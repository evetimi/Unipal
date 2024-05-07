using System.Collections;
using System.Collections.Generic;
using UI.Menu.PanelIDs;
using UnityEngine;

namespace UI.Menu {
    public class EnrollUserMenuContainer : MenuContainer<EnrollUserPanelID> {
        public void ChangeEnrollUser() {
            ChangePanel(EnrollUserPanelID.EnrollUser);
        }

        public void ChangeEnrollSuccess() {
            ChangePanel(EnrollUserPanelID.EnrollSuccess);
        }
    }
}
