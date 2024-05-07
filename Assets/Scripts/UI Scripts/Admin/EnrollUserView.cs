using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UI.Menu;
using Unipal.Controller.User;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.Admins {
    public class EnrollUserView : MenuPanel {
        [FoldoutGroup("Setup"), SerializeField] private EnrollUserMenuContainer _enrollMenuContainer;
        [FoldoutGroup("Setup"), SerializeField] private TMP_Text _faculty;
        [FoldoutGroup("Setup"), SerializeField] private TMP_Text _course;
        [FoldoutGroup("Setup"), SerializeField] private TMP_Text _name;
        [FoldoutGroup("Setup"), SerializeField] private TMP_Text _surname;
        [FoldoutGroup("Setup"), SerializeField] private TMP_Text _email;
        [FoldoutGroup("Setup"), SerializeField] private UIMultipleChoice _gender;
        [FoldoutGroup("Setup"), SerializeField] private TMP_Text _phoneNumber;
        [FoldoutGroup("Setup"), SerializeField] private TMP_Text _dob;
        [FoldoutGroup("Setup"), SerializeField] private bool _isSubmitTeacher;

        [BoxGroup("Transition"), SerializeField] private Graphic[] _backgroundGraphics;
        [BoxGroup("Transition"), SerializeField] private Graphic[] _itemGraphics;
        [BoxGroup("Transition"), SerializeField] private Color _color1 = Color.white;
        [BoxGroup("Transition"), SerializeField] private Color _color2 = Color.white;
        [BoxGroup("Transition"), SerializeField] private float _transitionTime = 0.2f;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            _enrollMenuContainer.ChangeEnrollUser();
        }

        public void Submit() {
            // TODO: Submit
            // AdminController.Instance.EnrollUser(UserType.Student, _name.text, _surname.text, _email.text, )
            _enrollMenuContainer.ChangeEnrollSuccess();
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
            } else {
                foreach (var item in _backgroundGraphics) {
                    item.DOColor(_color2, _transitionTime);
                }
                foreach (var item in _itemGraphics) {
                    item.DOColor(_color1, _transitionTime);
                }
            }
        }
    }
}
