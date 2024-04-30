using System.Collections;
using System.Collections.Generic;
using UI.Menu;
using UI.Menu.PanelIDs;
using UnityEngine;

namespace UI.Logins {
    public class LoginView : MenuPanel {
        [SerializeField] private LoginMenuContainer _loginMenuContainer;
        [SerializeField] private SignInPanel _signInPanel;

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

        private void BackToEmail() {
            // Get back to Email

            _verified = false;
            _signInPanel.BackToEmail();
        }

        public void ClickSignInButton() {
            if (!_verified || !_tokenVerified) {
                if (!_verified) {
                    VerifyEmail();
                } else {
                    VerifyToken();
                }
            } else {
                // TODO: Check verification
                BackToEmail();
                _loginMenuContainer.ChangePanel(LoginPanelID.Login);
            }
        }
    }
}
