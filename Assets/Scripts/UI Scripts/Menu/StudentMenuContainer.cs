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

        public void ChangeMainMenu() {
            ChangePanel(StudentPanelID.MainMenu);
            _indicator.DOMove(_homeLocation.position, 0.2f);
        }

        public void ChangeProfile() {
            ChangePanel(StudentPanelID.Profile);
            _indicator.DOMove(_profileLocation.position, 0.2f);
        }

        public void ChangeModuleList() {
            ChangePanel(StudentPanelID.ModuleList);
            _indicator.DOMove(_bookLocation.position, 0.2f);
        }

        public void ChangeModule() {
            ChangePanel(StudentPanelID.Module);
            _indicator.DOMove(_bookLocation.position, 0.2f);
        }

        public void ChangeCalendar() {
            ChangePanel(StudentPanelID.Calendar);
        }
    }
}
