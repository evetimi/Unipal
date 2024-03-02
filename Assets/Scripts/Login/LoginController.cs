using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : MonoBehaviourSingleton<LoginController>
{
    [SerializeField] private string emailVerificationAPI = "emailVerification.php";
    [SerializeField] private string signupAPI = "signup.php";
    [SerializeField] private string loginAPI = "login.php";

    public async void VerifyEmail(string email) {
        EmailVerifyObject emailVerifyObject = new EmailVerifyObject() {
            email = email
        };

        var uri = await UnipalClient.DoPostRequestAsync(emailVerificationAPI, emailVerifyObject);
        Debug.Log(uri);
    }

    public async void Signup(string email, string password, string confirmPassword) {
        // TODO: confirmPassword
        
        LoginObject loginObj = new() {
            email = email,
            password = password
        };

        var uri = await UnipalClient.DoPostRequestAsync(signupAPI, loginObj);
        Debug.Log(uri);
    }

    public async void Login(string email, string password) {
        LoginObject loginObj = new() {
            email = email,
            password = password
        };

        MainMenuController.Instance.ChangePanel(MainMenuPanelID.MainMenu);

        var uri = await UnipalClient.DoPostRequestAsync(loginAPI, loginObj);
        Debug.Log(uri);

        // Status obj = JsonUtility.FromJson<Status>(uri);

        // Debug.Log($"After Json: email = {obj.body}, password = {password}");
    }
}

public struct EmailVerifyObject {
    public string email;
}

public struct LoginObject {
    public string email;
    public string password;
}

public struct Status {
    public string status;
    public string body;
}