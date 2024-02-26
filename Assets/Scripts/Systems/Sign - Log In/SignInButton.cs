using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SignInButton : MonoBehaviour
{
    [SerializeField] private SignInPanel _signInPanel;
    [SerializeField] private TMP_Text _usernameText;
    [SerializeField] private TMP_Text _passwordText;
    [SerializeField] private TMP_Text _passwordConfirmText;

    public void Accept() {
        // TODO: Look for LoginController
        _signInPanel.ClickSignInButton();
    }
}
