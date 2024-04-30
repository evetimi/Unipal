using System.Collections;
using System.Collections.Generic;
using UI.Menu;
using UI.Menu.PanelIDs;
using UnityEngine;

namespace UI.Logins {
    public class LoginView : MenuPanel {
        [SerializeField] private LoginMenuContainer _loginMenuContainer;
        [SerializeField] private SignInPanel _signInPanel;
        [SerializeField] private LoginPanel _loginPanel;

        private bool _verified;
        private bool _tokenVerified;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            if (enabled) {
                _loginMenuContainer.ChangeLogin();
            }
        }

        private async void VerifyEmail() {
            _verified = await _signInPanel.VerifyEmail();
        }

        private async void VerifyToken() {
            _tokenVerified = await _signInPanel.VerifyToken();
        }

        private async void SignUp() {
            bool signup = await _signInPanel.SignUp();
            if (signup) {

            }
        }

        public void BackToEmail() {
            // Get back to Email

            _verified = false;
            _signInPanel.BackToEmail();
        }

        public void ClickSignInButton() {
            if (!_verified) {
                VerifyEmail();
            } else {
                // TODO: Check verification
                SignUp();
            }
            // if (!_verified || !_tokenVerified) {
            //     if (!_verified) {
            //         VerifyEmail();
            //     } else {
            //         VerifyToken();
            //     }
            // } else {
            //     // TODO: Check verification
            //     BackToEmail();
            //     _loginMenuContainer.ChangePanel(LoginPanelID.Login);
            // }
        }

        public async void ClickLoginButton() {
            bool login = await _loginPanel.Login();

            if (login) {
                // TODO: go to main menu
            }
        }
    }
}
