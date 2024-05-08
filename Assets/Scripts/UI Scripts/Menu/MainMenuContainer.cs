using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UI.Menu.PanelIDs;
using UnityEngine;

namespace UI.Menu {
    public class MainMenuContainer : MenuContainer<MainMenuPanelID> {
        // [SerializeField] private Transform _parent;
        // [SerializeField, RequiredIn(PrefabKind.PrefabAsset)] private StudentMenuContainer _studentMenuPrefab;

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

        public void ChangeStudentMenu() {
            // if (_currentMenu == null || !_currentMenu.TryGetComponent<StudentMenuContainer>(out var studentMenuContainer)) {
            //     if (_currentMenu) {
            //         Destroy(_currentMenu);
            //     }
            //     studentMenuContainer = Instantiate(_studentMenuPrefab, _parent);
            //     _currentMenu = studentMenuContainer.gameObject;
            // }

            // studentMenuContainer.ChangeMainMenu();

            GameObject newMenu = ChangePanel(MainMenuPanelID.StudentMenu).gameObject;

            if (newMenu != _currentMenu) {
                Destroy(_currentMenu);
                _currentMenu = newMenu;
            }
        }

        public void ChangeTeacherMenu() {
            GameObject newMenu = ChangePanel(MainMenuPanelID.TeacherMenu).gameObject;

            if (newMenu != _currentMenu) {
                Destroy(_currentMenu);
                _currentMenu = newMenu;
            }
        }

        public void ChangeAdminMenu() {
            GameObject newMenu = ChangePanel(MainMenuPanelID.AdminMenu).gameObject;

            if (newMenu != _currentMenu) {
                Destroy(_currentMenu);
                _currentMenu = newMenu;
            }
        }
    }
}
