using System.Collections;
using System.Collections.Generic;
using UI.Menu;
using UI.Menu.PanelIDs;
using UI.Popup;
using Unipal.Controller.Login;
using Unipal.Controller.User;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.Logins {
    public class LoginView : MenuPanel {
        [SerializeField] private LoginMenuContainer _loginMenuContainer;
        [SerializeField] private SignInPanel _signInPanel;
        [SerializeField] private LoginPanel _loginPanel;
        [SerializeField] private TokenPopup _tokenPopupPrefab;
        [SerializeField] private ConfirmPopupObject _confirmPopupPrefab;

        private bool _verified;
        private bool _tokenVerified;
        private TokenPopup _createdTokenPopup;

        private void Start() {
            _loginMenuContainer.OnAfterTransition += OnPanelChanged;
        }

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            if (enabled) {
                _loginMenuContainer.ChangeLogin();
            }
        }

        private void OnPanelChanged(LoginPanelID loginPanelID) {
            _signInPanel.ResetCreditials();
            _loginPanel.ResetCreditials();
        }

        private async void VerifyEmail() {
            CredentialStatus verify = await _signInPanel.VerifyEmail();
            
            if (verify == CredentialStatus.Success) {
                _createdTokenPopup = PopupView.Instance.Open(_tokenPopupPrefab, CancelToken, VerifyToken) as TokenPopup;
            }

            _verified = verify == CredentialStatus.Success;
        }

        private void CancelToken(string _) {
            BackToEmail();
        }

        private async void VerifyToken(string token) {
            CredentialStatus tokenVerify = await _signInPanel.VerifyToken(token);
            
            if (tokenVerify == CredentialStatus.Success) {
                PopupView.Instance.Close();
            } else if (tokenVerify == CredentialStatus.Fail) {
                _createdTokenPopup.SetErrorMessage(true);
            }

            _tokenVerified = tokenVerify == CredentialStatus.Success;
        }

        private async void SignUp() {
            SignupStatus signup = await _signInPanel.SignUp();
            if (signup.status == CredentialStatus.Success) {
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
            LoginStatus login = await _loginPanel.Login();

            if (login.status == CredentialStatus.Success) {
                // TODO: check the user type, go to main menu of that user

                if (login.userType == UserType.Student) {
                    MainMenuController.Instance.MainMenuContainer.ChangeStudentMenu();
                } else {
                    MainMenuController.Instance.MainMenuContainer.ChangeAdminMenu();
                }

                _loginPanel.ResetCreditials();
            } else {
                PopupView.Instance.Open(_confirmPopupPrefab, null, null);
            }
        }
    }
}
