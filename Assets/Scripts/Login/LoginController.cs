using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : MonoBehaviour
{
    public static LoginController Instance;

    private void Awake() {
        Instance = this;
    }

    public async void Login(string username, string password) {
        LoginObject loginObj = new(username, password);

        var uri = await UnipalClient.DoPostRequestAsync("asd", loginObj);
        Debug.Log(uri);
    }
}

public class LoginObject {
    public string username;
    public string password;

    public LoginObject(string username, string password) {
        this.username = username;
        this.password = password;
    }
}