using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

namespace Unipal.API {
    /// <summary>
    /// The class which is used to create connection and provide functionality to send request to the server.
    /// </summary>
    public static class UnipalClient {
        public static HttpClient client;
        public static Action<object> OnBeforeSendingRequest;
        public static Action<UnipalMessage> OnAfterSendingRequest;

        /// <summary>
        /// To validate the connection to the server.
        /// </summary>
        /// <returns>TRUE if the connection is successful.</returns>
        public static bool ValidateHttpClient() {
            if (client == null) {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://13.60.19.3/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );
            }

            return true;
        }
        
        /// <summary>
        /// To send POST request to the server API, the method will wait until the server response before returning the result.
        /// </summary>
        /// <typeparam name="SendType">The object type to send request to the API.</typeparam>
        /// <typeparam name="ReceiveType">The object type to receive response from the API.</typeparam>
        /// <param name="apiUrl">The API path to send request. This DOES NOT include the IP address.</param>
        /// <param name="requestObj">The object to send request.</param>
        /// <returns>The response UnipalMessage from the server that will be converted to an object of ReceiveType.</returns>
        public static async Task<UnipalMessage<ReceiveType>> DoPostRequestAsync<SendType, ReceiveType>(string apiUrl, ApiSendObject<SendType> requestObj) {
            if (!ValidateHttpClient()) {
                return null;
            }

            OnBeforeSendingRequest?.Invoke(requestObj);

            UnipalMessage<ReceiveType> msg = new();

            try {
                HttpResponseMessage response = await client.PostAsJsonAsync(
                    $"api/{apiUrl}", requestObj
                );

                string responseString = await response.Content.ReadAsStringAsync();

                msg.receiveMessageSuccess = true;
                msg.failedMessage = "";
                msg.receivedMessage = JsonUtility.FromJson<ApiReceiveObject<ReceiveType>>(responseString);
            } catch (Exception e) {
                string responseString = e.InnerException.Message;
                msg.receiveMessageSuccess = false;
                msg.failedMessage = responseString;
                msg.receivedMessage = null;
            }

            OnAfterSendingRequest?.Invoke(msg);
            
            return msg;
        }
    }
}