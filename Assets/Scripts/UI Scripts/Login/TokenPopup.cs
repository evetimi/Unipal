using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Menu;
using UI.Menu.PanelIDs;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Logins {
    public class TokenPopup : PopupObject<string> {
        [SerializeField] private GameObject _errorMessage;
        [SerializeField] private TMP_InputField _tokenField;
        [SerializeField] private TMP_Text[] _tokenTexts;
        [SerializeField] private Transform _caretBlink;
        [SerializeField] private Image _caretBlinkImage;
        [SerializeField] private float _caretBlinkRate = 0.87f;

        private float _caretBlinkTimer;

        private void Awake() {
            _errorMessage.SetActive(false);
            _caretBlinkImage.gameObject.SetActive(false);

            _tokenField.characterLimit = _tokenTexts.Length;
            _tokenField.onValueChanged.AddListener(OnValueChanged);
            _tokenField.onSelect.AddListener(OnSelect);
            _tokenField.onEndEdit.AddListener(OnEndEdit);

            for (int i = 0; i < _tokenTexts.Length; i++) {
                _tokenTexts[i].text = "";
            }

            _caretBlinkTimer = _caretBlinkRate;
        }

        private void Update() {
            _caretBlinkTimer -= Time.deltaTime;
            if (_caretBlinkTimer <= 0) {
                _caretBlinkTimer = _caretBlinkRate;
                _caretBlinkImage.enabled = !_caretBlinkImage.enabled;
            }
        }

        private void OnValueChanged(string text) {
            int length = text.Length;
            if (length > _tokenTexts.Length)
                length = _tokenTexts.Length;

            for (int i = 0; i < length; i++) {
                _tokenTexts[i].text = text[i].ToString();
            }

            for (int i = length; i < _tokenTexts.Length; i++) {
                _tokenTexts[i].text = "";
            }

            if (length < _tokenTexts.Length) {
                Vector2 lastPosition = _tokenTexts[length].transform.position;
                _caretBlink.position = lastPosition;
                _caretBlinkImage.gameObject.SetActive(true);
            } else {
                _caretBlinkImage.gameObject.SetActive(false);
            }
        }

        private void OnSelect(string _) {
            _caretBlinkImage.gameObject.SetActive(true);
        }

        private void OnEndEdit(string _) {
            _caretBlinkImage.gameObject.SetActive(false);
        }

        public override string GetData() {
            return _tokenField.text;
        }

        public void SetErrorMessage(bool enabled) {
            _errorMessage.SetActive(enabled);
        }
    }
}
