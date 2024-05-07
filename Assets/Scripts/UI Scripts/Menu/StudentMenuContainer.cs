using System.Collections;
using System.Collections.Generic;
using UI.Menu.PanelIDs;
using UnityEngine;

namespace UI.Menu {
    public class StudentMenuContainer : MenuContainer<StudentPanelID> {
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
    }
}
