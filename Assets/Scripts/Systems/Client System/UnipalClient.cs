using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class which is used to create connection and provide functionality to send request to the server.
/// </summary>
public static class UnipalClient {
    public static HttpClient client;

    /// <summary>
    /// To validate the connection to the server.
    /// </summary>
    /// <returns>TRUE if the connection is successful.</returns>
    public static bool ValidateHttpClient() {
        if (client == null) {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://13.53.155.166/");
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
    /// <typeparam name="T">The object type to send request to the API.</typeparam>
    /// <param name="apiUrl">The API path to send request. This DOES NOT include the IP address.</param>
    /// <param name="requestObj">The object to send request.</param>
    /// <returns>The response message from the server, this will be the JSON format.</returns>
    public static async Task<string> DoPostRequestAsync<T>(string apiUrl, T requestObj) {
        if (!ValidateHttpClient()) {
            return null;
        }

        HttpResponseMessage response = await client.PostAsJsonAsync(
            $"api/{apiUrl}", requestObj
        );

        var responseString = await response.Content.ReadAsStringAsync();
        
        return responseString;
    }
}