using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : MonoBehaviourSingleton<LoginController>
{
    [SerializeField] private string loginAPI = "login.php";

    public async void Login(string email, string password) {
        LoginObject loginObj = new() {
            email = email,
            password = password
        };

        var uri = await UnipalClient.DoPostRequestAsync(loginAPI, loginObj);
        Debug.Log(uri);

        LoginObject obj = JsonUtility.FromJson<LoginObject>(uri);

        Debug.Log($"After Json: email = {email}, password = {password}");
    }
}

public struct LoginObject {
    public string email;
    public string password;
}