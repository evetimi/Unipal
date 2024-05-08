using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Menu;
using Unipal.Controller.User;
using Unipal.Model.User;
using UnityEngine;

namespace UI.Admins {
    public class ProfileView : MenuPanel {
        [SerializeField] private ProfileMenuContainer _profileMenuContainer;
        [SerializeField] private TMP_Text _id;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _gender;
        [SerializeField] private TMP_Text _dob;
        [SerializeField] private TMP_Text _email;
        [SerializeField] private TMP_Text _address;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            if (enabled) {
                _profileMenuContainer.ChangeProfile();
                SetupInformation();
            }
        }

        private void SetupInformation() {
            Student student = StudentController.Instance.CurrentLoggedAccount;
            if (student != null) {
                _id.text = "Student no: " + student.Id;
                _name.text = student.Name + " " + student.Surname;
                _gender.text = student.Gender;
                _dob.text = student.Dob.ToString("dd/mm/yyyy");
                _address.text = student.Address;
                _email.text = student.Email;
            }
        }

        public void LogoutButtonClick() {
            MainMenuController.Instance.Logout();
        }
    }
}
