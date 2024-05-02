using System.Collections;
using System.Collections.Generic;
using UI.Menu;
using UI.Menu.PanelIDs;
using UI.Popup;
using UnityEngine;

namespace UI.Logins {
    public class LoginView : MenuPanel {
        [SerializeField] private LoginMenuContainer _loginMenuContainer;
        [SerializeField] private SignInPanel _signInPanel;
        [SerializeField] private LoginPanel _loginPanel;
        [SerializeField] private TokenPopup _tokenPopupPrefab;

        private bool _verified;
        private bool _tokenVerified;
        private TokenPopup _createdTokenPopup;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            if (enabled) {
                _loginMenuContainer.ChangeLogin();
            }
        }

        private async void VerifyEmail() {
            // _verified = await _signInPanel.VerifyEmail();
            _verified = true;
            if (_verified) {
                _createdTokenPopup = PopupView.Instance.Open(_tokenPopupPrefab, null, VerifyToken) as TokenPopup;
            }
        }

        private async void VerifyToken(string token) {
            // _tokenVerified = await _signInPanel.VerifyToken(token);
            _tokenVerified = false;
            if (_tokenVerified) {
                PopupView.Instance.Close();
            } else {
                _createdTokenPopup.SetErrorMessage(true);
            }
        }

        private async void SignUp() {
            bool signup = await _signInPanel.SignUp();
            if (signup) {
                BackToEmail();
                _loginMenuContainer.ChangePanel(LoginPanelID.Login);
            }
        }

        public void BackToEmail() {
            // Get back to Email

            _verified = false;
            _signInPanel.BackToEmail();
        }

        public void ClickSignInButton() {
            // if (!_verified) {
            //     VerifyEmail();
            // } else {
            //     // TODO: Check verification
            //     SignUp();
            // }
            if (!_verified || !_tokenVerified) {
                if (!_verified) {
                    VerifyEmail();
                }
            } else {
                SignUp();
            }
        }

        public async void ClickLoginButton() {
            // bool login = await _loginPanel.Login();
            bool login = true;

            if (login) {
                // TODO: go to main menu
                MainMenuController.Instance.MainMenuContainer.ChangeStudentMenu();
            }
        }
    }
}
