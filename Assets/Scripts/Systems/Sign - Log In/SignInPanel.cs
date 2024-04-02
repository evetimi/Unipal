using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SignInPanel : MenuPanel
{
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private TMP_InputField _passwordConfirmInput;
    private bool _verified;

    public async void VerifyEmail() {
        bool verify = await LoginController.Instance.VerifyEmail(_emailInput.text);

        // Do anim if verify success

        if (verify) {
            _verified = true;
            _emailInput.interactable = false;
            _onOffAnim.SetBool("signin", true);
        }
    }

    public void BackToEmail() {
        // Get back to Email

        _verified = false;
        _emailInput.interactable = true;
        _onOffAnim.SetBool("signin", false);
    }

    public void ClickSignInButton() {
        if (!_verified) {
            VerifyEmail();
        } else {
            // TODO: Check verification
            BackToEmail();
            MainMenuController.Instance.ChangePanel(MainMenuPanelID.LogIn);
        }
    }
}