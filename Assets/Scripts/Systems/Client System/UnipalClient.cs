using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnipalClient {
    public static HttpClient client;

    public static bool ValidateHttpClient() {
        if (client == null) {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://16.171.193.220/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }

        return true;
    }

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