using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LoginPanel : MenuPanel
{
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _passwordInput;

    public async Task<CredentialStatus> Login() {
        return await LoginController.Instance.Login(_emailInput.text, _passwordInput.text);
    }
}
