using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInButton : MonoBehaviour
{
    [SerializeField] private bool _isSignInButton;

    public void Accept() {
        // TODO: Look for LoginController
        if (_isSignInButton) {
            MainMenuController.Instance.ChangePanel(MainMenuPanelID.LogIn);
        } else {
            MainMenuController.Instance.ChangePanel(MainMenuPanelID.MainMenu);
        }
    }
}
