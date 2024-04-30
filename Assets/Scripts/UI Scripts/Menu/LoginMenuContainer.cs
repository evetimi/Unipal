using System.Collections;
using System.Collections.Generic;
using UI.Menu.PanelIDs;
using UnityEngine;

namespace UI.Menu {
    public class LoginMenuContainer : MenuContainer<LoginPanelID> {
        public void ChangeSignUp() {
            ChangePanel(LoginPanelID.SignUp);
        }

        public void ChangeLogin() {
            ChangePanel(LoginPanelID.Login);
        }
    }
}
