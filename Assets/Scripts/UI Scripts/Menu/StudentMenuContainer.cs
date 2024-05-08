using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UI.Menu.PanelIDs;
using UnityEngine;

namespace UI.Menu {
    public class StudentMenuContainer : MenuContainer<StudentPanelID> {
        [SerializeField] private Transform _indicator;
        [SerializeField] private Transform _bookLocation;
        [SerializeField] private Transform _homeLocation;
        [SerializeField] private Transform _profileLocation;

        private void Start() {
            OnAfterTransition += OnAfterChangePanel;
        }

        public void ChangeMainMenu() {
            ChangePanel(StudentPanelID.MainMenu);
        }

        public void ChangeProfile() {
            ChangePanel(StudentPanelID.Profile);
        }

        public void ChangeModuleList() {
            ChangePanel(StudentPanelID.ModuleList);
        }

        public void ChangeModule() {
            ChangePanel(StudentPanelID.Module);
        }

        public void ChangeCalendar() {
            ChangePanel(StudentPanelID.Calendar);
        }

        private void OnAfterChangePanel(StudentPanelID panelID) {
            switch (panelID) {
                case StudentPanelID.MainMenu: {
                    MoveIndicator(_homeLocation);
                    break;
                }
                case StudentPanelID.ModuleList: {
                    MoveIndicator(_bookLocation);
                    break;
                }
                case StudentPanelID.Module: {
                    MoveIndicator(_homeLocation);
                    break;
                }
                case StudentPanelID.Profile: {
                    MoveIndicator(_profileLocation);
                    break;
                }
            }
        }

        private void MoveIndicator(Transform location) {
            _indicator.DOMove(location.position, 0.2f);
        }
    }
}
