using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogInButton : MonoBehaviour
{
    [SerializeField] private bool _isSignInButton;
    [SerializeField] private TMP_Text _usernameText;
    [SerializeField] private TMP_Text _passwordText;

    public void Accept() {
        // TODO: Look for LoginController
        if (_isSignInButton) {
            MainMenuController.Instance.ChangePanel(MainMenuPanelID.LogIn);

        } else {
            // MainMenuController.Instance.ChangePanel(MainMenuPanelID.MainMenu);
            LoginController.Instance.Login(_usernameText.text, _passwordText.text);
        }
    }
}
