using System.Threading.Tasks;
using Sirenix.OdinInspector;
using TMPro;
using Unipal.Controller.Login;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Logins {
    public class SignInPanel : MenuPanel {
        [BoxGroup("Fields"), SerializeField] private TMP_InputField _emailInput;
        [BoxGroup("Fields"), SerializeField] private TMP_InputField _passwordInput;
        [BoxGroup("Fields"), SerializeField] private TMP_InputField _passwordConfirmInput;

        [BoxGroup("Validation"), SerializeField] private GameObject _emailFailed;
        [BoxGroup("Validation"), SerializeField] private MenuPanel _buttonsAndValidateTexts;
        [BoxGroup("Validation"), SerializeField] private Image _passwordValidateImage;
        [BoxGroup("Validation"), SerializeField] private Image _confirmPasswordValidateImage;
        [BoxGroup("Validation"), SerializeField] private Sprite _tickSprite;
        [BoxGroup("Validation"), SerializeField] private Sprite _crossSprite;
        [BoxGroup("Validation"), SerializeField] private TMP_Text _3char;
        [BoxGroup("Validation"), SerializeField] private TMP_Text _1spec;
        [BoxGroup("Validation"), SerializeField] private TMP_Text _1number;
        [BoxGroup("Validation"), SerializeField] private TMP_Text _1uppercase;
        [BoxGroup("Validation"), SerializeField] private Color _validateColor = Color.gray;
        [BoxGroup("Validation"), SerializeField] private Color _invalidColor = Color.black;

        private bool _passwordValidated;
        private bool _confirmPasswordValidated;

        protected override void Awake() {
            base.Awake();
            _passwordInput.onValueChanged.AddListener(PasswordValueChanged);
            _passwordConfirmInput.onValueChanged.AddListener(ConfirmPasswordValueChanged);
            SetImage(_passwordValidateImage, _crossSprite);
            SetImage(_confirmPasswordValidateImage, _crossSprite);
        }

        #region Validate Password Methods
        private void SetImage(Image image, Sprite sprite) {
            if (image != null) {
                image.sprite = sprite;
            }
        }

        private void PasswordValidate(string password, string confirmPassword) {
            int charCount = password.Length;
            int specCount = 0;
            int numberCount = 0;
            int uppercaseCount = 0;

            for (int i = 0; i < charCount; i++) {
                char c = password[i];
                if (char.IsDigit(c)) {
                    numberCount++;
                } else if (char.IsUpper(c)) {
                    uppercaseCount++;
                } else if (!char.IsLetter(c)) {
                    specCount++;
                }
            }

            int ValidateText(TMP_Text t, bool validated) {
                t.color = validated ? _validateColor : _invalidColor;
                return validated ? 1 : 0;
            }

            int validated = 0;
            validated += ValidateText(_3char, charCount >= 3);
            validated += ValidateText(_1spec, specCount >= 1);
            validated += ValidateText(_1number, numberCount >= 1);
            validated += ValidateText(_1uppercase, uppercaseCount >= 1);

            if (validated >= 4) {
                SetImage(_passwordValidateImage, _tickSprite);
                _passwordValidated = true;
            } else {
                SetImage(_passwordValidateImage, _crossSprite);
                _passwordValidated = false;
            }

            if (!string.IsNullOrEmpty(confirmPassword) && confirmPassword.Equals(_passwordInput.text)) {
                SetImage(_confirmPasswordValidateImage, _tickSprite);
                _confirmPasswordValidated = true;
            } else {
                SetImage(_confirmPasswordValidateImage, _crossSprite);
                _confirmPasswordValidated = false;
            }

            if (_passwordValidated && _confirmPasswordValidated) {
                _buttonsAndValidateTexts.SetEnabled(false);
            } else {
                _buttonsAndValidateTexts.SetEnabled(true);
            }
        }

        private void PasswordValueChanged(string password) {
            PasswordValidate(password, _passwordConfirmInput.text);
        }

        private void ConfirmPasswordValueChanged(string confirmPassword) {
            PasswordValidate(_passwordInput.text, confirmPassword);
        }
        #endregion

        public async Task<CredentialStatus> VerifyEmail() {
            CredentialStatus verify = await LoginController.Instance.VerifyEmail(_emailInput.text);

            if (verify == CredentialStatus.Success) {
                _emailInput.interactable = false;
                _emailFailed.SetActive(false);
            } else if (verify == CredentialStatus.Fail) {
                _emailFailed.SetActive(true);
            }

            return verify;
        }

        public async Task<CredentialStatus> VerifyToken(string token) {
            CredentialStatus tokenVerify = await LoginController.Instance.VerifyToken(_emailInput.text, token);

            if (tokenVerify == CredentialStatus.Success) {
                _emailInput.interactable = false;
                _onOffAnim.SetBool("signin", true);
                _buttonsAndValidateTexts.SetEnabled(true);
            }

            return tokenVerify;
        }

        public async Task<SignupStatus> SignUp() {
            SignupStatus signUp = await LoginController.Instance.Signup(_emailInput.text, _passwordInput.text, _passwordConfirmInput.text);
            return signUp;
        }

        public void BackToEmail() {
            // Get back to Email
            _emailInput.interactable = true;
            _onOffAnim.SetBool("signin", false);
            _buttonsAndValidateTexts.SetEnabled(false);
            _passwordInput.text = "";
            _passwordConfirmInput.text = "";
        }

        public void ResetCreditials() {
            _emailInput.text = "";
            _passwordInput.text = "";
            _passwordConfirmInput.text = "";
        }
    }
}
