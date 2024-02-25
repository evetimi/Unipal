using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : MonoBehaviourSingleton<LoginController>
{
    [SerializeField] private string loginAPI = "login.php";

    public async void Login(string username, string password) {
        LoginObject loginObj = new() {
            username = username,
            password = password
        };

        var uri = await UnipalClient.DoPostRequestAsync(loginAPI, loginObj);
        Debug.Log(uri);

        LoginObject obj = JsonUtility.FromJson<LoginObject>(uri);

        Debug.Log($"After Json: username = {username}, password = {password}");
    }
}

public struct LoginObject {
    public string username;
    public string password;
}