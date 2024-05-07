using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities {
    public class UIMultipleChoice : MonoBehaviour {
        [BoxGroup("Setup"), SerializeField] private int _startOptionIndex;
        [BoxGroup("Setup"), SerializeField] private Option[] _options;
        
        [BoxGroup("Transition"), SerializeField] private float _transitionTime = 0.2f;
        [BoxGroup("Transition"), SerializeField] private Color _enableOptionColor;
        [BoxGroup("Transition"), SerializeField] private Color _disableOptionColor;

        private Option _chosenOption;

        public Option ChosenOption => _chosenOption;
        
        [System.Serializable]
        public class Option {
            public Image tickImage;
            public Button tickButton;
            public string name;
        }

        private void OnValidate() {
            if (_startOptionIndex < 0) {
                _startOptionIndex = 0;
            } else if (_startOptionIndex >= _options.Length) {
                _startOptionIndex = _options.Length - 1;
            }
        }

        private void Start() {
            Choose(_options[_startOptionIndex]);

            foreach (Option option in _options) {
                option.tickButton.onClick.AddListener(() => Choose(option));
            }
        }

        private void Choose(Option option) {
            if (_chosenOption != null) {
                _chosenOption.tickImage.DOColor(_disableOptionColor, _transitionTime);
            }

            _chosenOption = option;

            if (_chosenOption != null) {
                _chosenOption.tickImage.DOColor(_enableOptionColor, _transitionTime);
            }
        }
    }
}
