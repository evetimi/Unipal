using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Menu;
using UI.Menu.PanelIDs;
using Unipal.Controller.User;
using UnityEngine;

namespace UI.Students {
    public class StudentView : MenuPanel {
        [SerializeField] private StudentMenuContainer _studentMenuContainer;
        [SerializeField] private TMP_Text _greetingsText;

        private void Start() {
            _studentMenuContainer.OnAfterTransition += OnChildPanelChanged;
        }

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            if (enabled) {
                _studentMenuContainer.ChangeMainMenu();
            }
        }

        private void OnChildPanelChanged(StudentPanelID studentPanelID) {
            if (studentPanelID == StudentPanelID.MainMenu) {
                _greetingsText.text = StudentController.Instance.CurrentLoggedAccount.Name;
            }
        }
    }
}
