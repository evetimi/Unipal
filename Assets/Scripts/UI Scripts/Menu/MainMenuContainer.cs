using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UI.Menu.PanelIDs;
using UnityEngine;

namespace UI.Menu {
    public class MainMenuContainer : MenuContainer<MainMenuPanelID> {
        [SerializeField] private Transform _parent;
        [SerializeField, RequiredIn(PrefabKind.PrefabAsset)] private StudentMenuContainer _studentMenuPrefab;

        private GameObject _currentMenu;

        public void ChangeVerifyScreen() {
            ChangePanel(MainMenuPanelID.VerifyScreen);
        }

        public void ChangeSignUp() {
            ChangePanel(MainMenuPanelID.SignUp);
        }

        public void ChangeLogIn() {
            ChangePanel(MainMenuPanelID.LogIn);
        }

        public void ChangeStudentMainMenu() {
            if (_currentMenu == null || !_currentMenu.TryGetComponent<StudentMenuContainer>(out var studentMenuContainer)) {
                if (_currentMenu) {
                    Destroy(_currentMenu);
                }
                studentMenuContainer = Instantiate(_studentMenuPrefab, _parent);
                _currentMenu = studentMenuContainer.gameObject;
            }

            studentMenuContainer.ChangeMainMenu();
        }

        public void ChangeTeacherMainMenu() {
            // ChangePanel(MainMenuPanelID.TeacherMainMenu);
        }

        public void ChangeAdminMainMenu() {
            // ChangePanel(MainMenuPanelID.AdminMainMenu);
        }
    }
}
