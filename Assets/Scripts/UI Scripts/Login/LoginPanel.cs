using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unipal.Controller.Login;
using UnityEngine;

namespace UI.Logins {
    public class LoginPanel : MenuPanel {
        [SerializeField] private TMP_InputField _emailInput;
        [SerializeField] private TMP_InputField _passwordInput;

        public async Task<LoginStatus> Login() {
            return await LoginController.Instance.Login(_emailInput.text, _passwordInput.text);
            // return CredentialStatus.Success;
        }
    }
}
