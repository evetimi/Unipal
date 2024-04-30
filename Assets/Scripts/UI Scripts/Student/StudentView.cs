using System.Collections;
using System.Collections.Generic;
using UI.Menu;
using UnityEngine;

namespace UI.Students {
    public class StudentView : MenuPanel {
        [SerializeField] private StudentMenuContainer _studentMenuContainer;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            if (enabled) {
                _studentMenuContainer.ChangeMainMenu();
            }
        }
    }
}
