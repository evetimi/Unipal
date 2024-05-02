using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SignInPanel : MenuPanel
{
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private TMP_InputField _passwordConfirmInput;

    public async Task<bool> VerifyEmail() {
        bool verify = await LoginController.Instance.VerifyEmail(_emailInput.text);

        if (verify) {
            _emailInput.interactable = false;
            _onOffAnim.SetBool("signin", true);
        }

        return verify;
    }

    public async Task<bool> VerifyToken(string token) {
        bool tokenVerify = await LoginController.Instance.VerifyToken(_emailInput.text, token);

        // Do anim if verify success

        if (tokenVerify) {
            _emailInput.interactable = false;
            _onOffAnim.SetBool("signin", true);
        }

        return tokenVerify;
    }

    public async Task<bool> SignUp() {
        bool signUp = await LoginController.Instance.Signup(_emailInput.text, _passwordInput.text, _passwordConfirmInput.text);
        return signUp;
    }

    public void BackToEmail() {
        // Get back to Email
        _emailInput.interactable = true;
        _onOffAnim.SetBool("signin", false);
    }
}
