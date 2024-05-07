using System.Collections;
using System.Collections.Generic;
using UI.Menu.PanelIDs;
using UnityEngine;

namespace UI.Menu {
    public class AdminMenuContainer : MenuContainer<AdminPanelID> {
        public void ChangeMainMenu() {
            ChangePanel(AdminPanelID.MainMenu);
        }

        public void ChangeProfile() {
            ChangePanel(AdminPanelID.Profile);
        }

        public void ChangeEnrollUser() {
            ChangePanel(AdminPanelID.EnrollUser);
        }
    }
}
