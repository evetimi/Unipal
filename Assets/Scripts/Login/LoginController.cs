using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// The controller for the login and signup functionality of the application, this method will help the other script to validate and send request to the server.
/// </summary>
public class LoginController : MonoBehaviourSingleton<LoginController>
{
    [SerializeField] private string emailVerificationAPI = "emailVerification.php";
    [SerializeField] private string signupAPI = "signup.php";
    [SerializeField] private string loginAPI = "login.php";

    /// <summary>
    /// To verify if the email is signed up in the server.
    /// </summary>
    /// <param name="email">The email to send request</param>
    /// <returns>TRUE if the verification success</returns>
    public async Task<bool> VerifyEmail(string email) {
        EmailVerifyObject emailVerifyObject = new EmailVerifyObject() {
            email = email
        };

        var uri = await UnipalClient.DoPostRequestAsync(emailVerificationAPI, emailVerifyObject);
        Debug.Log(uri);

        // TODO: Check if the email is good to go: it has to be in the system but not registered yet

        return true;
    }

    /// <summary>
    /// Send sign up request to the server, this method will validate the email and password be sending request, if the validation failed, the request will not be made.
    /// </summary>
    /// <param name="email">Email to sign up</param>
    /// <param name="password">Password to sign up</param>
    /// <param name="confirmPassword">To confirm the password if it is matched password or not</param>
    public async void Signup(string email, string password, string confirmPassword) {
        // TODO: confirmPassword
        
        LoginObject loginObj = new() {
            email = email,
            password = password
        };

        var uri = await UnipalClient.DoPostRequestAsync(signupAPI, loginObj);
        Debug.Log(uri);
    }

    /// <summary>
    /// Send login request to the server, will send the user to the Home page if the login is successful.
    /// </summary>
    /// <param name="email">Email to login</param>
    /// <param name="password">Password to login</param>
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


/*
{
    "email": "7as8d9as"
}
*/