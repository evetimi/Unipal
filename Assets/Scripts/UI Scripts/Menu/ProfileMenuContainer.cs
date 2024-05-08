using System.Collections;
using System.Collections.Generic;
using UI.Menu.PanelIDs;
using UnityEngine;

namespace UI.Menu {
    public class ProfileMenuContainer : MenuContainer<ProfilePanelID> {
        public void ChangeProfile() {
            ChangePanel(ProfilePanelID.Profile);
        }

        public void ChangeChangePassword() {
            ChangePanel(ProfilePanelID.ChangePassword);
        }
    }
}
