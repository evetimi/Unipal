using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UI.Menu;
using Unipal.Controller.User;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using Utilities;

namespace UI.Admins {
    public class EnrollUserView : MenuPanel {
        [FoldoutGroup("Setup"), SerializeField] private EnrollUserMenuContainer _enrollMenuContainer;
        [FoldoutGroup("Setup"), SerializeField] private TMP_Text _faculty;
        [FoldoutGroup("Setup"), SerializeField] private TMP_Text _course;
        [FoldoutGroup("Setup"), SerializeField] private TMP_InputField _name;
        [FoldoutGroup("Setup"), SerializeField] private TMP_InputField _surname;
        [FoldoutGroup("Setup"), SerializeField] private TMP_InputField _email;
        [FoldoutGroup("Setup"), SerializeField] private UIMultipleChoice _gender;
        [FoldoutGroup("Setup"), SerializeField] private TMP_InputField _phoneNumber;
        [FoldoutGroup("Setup"), SerializeField] private UISimpleDateInput _dob;
        [FoldoutGroup("Setup"), SerializeField] private TMP_Text _emailConfirm;
        [FoldoutGroup("Setup"), SerializeField] private TMP_Text _token;
        [FoldoutGroup("Setup"), SerializeField] private bool _isSubmitTeacher;

        [BoxGroup("Transition"), SerializeField] private TMP_Text _submitText;
        [BoxGroup("Transition"), SerializeField] private Graphic[] _backgroundGraphics;
        [BoxGroup("Transition"), SerializeField] private Graphic[] _itemGraphics;
        [BoxGroup("Transition"), SerializeField] private Color _color1 = Color.white;
        [BoxGroup("Transition"), SerializeField] private Color _color2 = Color.white;
        [BoxGroup("Transition"), SerializeField] private float _transitionTime = 0.2f;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            _enrollMenuContainer.ChangeEnrollUser();
        }

        public async void Submit() {
            // TODO: Submit
            string dob = _dob.Year + "-" + _dob.Month + "-" + _dob.Day;
            var result = await AdminController.Instance.EnrollUser(_isSubmitTeacher ? UserType.Teacher : UserType.Student, _name.text, _surname.text, _gender.ChosenOption.name, _email.text, "", _phoneNumber.text, dob);

            if (result != null) {
                string token = "";
                for (int i = 0; i < result.body.token.Length; i++) {
                    token += result.body.token[i] + "-";
                }
                token = token[..^1];
                _token.text = token;
                _emailConfirm.text = _email.text;
                _enrollMenuContainer.ChangeEnrollSuccess();
            }
        }

        public void ToggleStudentTeacher(bool enabled) {
            _isSubmitTeacher = enabled;
            if (!_isSubmitTeacher) {
                foreach (var item in _backgroundGraphics) {
                    item.DOColor(_color1, _transitionTime);
                }
                foreach (var item in _itemGraphics) {
                    item.DOColor(_color2, _transitionTime);
                }

                _submitText.text = "Submit Student";
            } else {
                foreach (var item in _backgroundGraphics) {
                    item.DOColor(_color2, _transitionTime);
                }
                foreach (var item in _itemGraphics) {
                    item.DOColor(_color1, _transitionTime);
                }

                _submitText.text = "Submit Teacher";
            }
        }
    }
}
