using System.Collections;
using System.Collections.Generic;
using UI.Menu;
using UnityEngine;

namespace UI.Admins {
    public class AdminView : MenuPanel {
        [SerializeField] private AdminMenuContainer _amindMenuContainer;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            if (enabled) {
                _amindMenuContainer.ChangeMainMenu();
            }
        }
    }
}
