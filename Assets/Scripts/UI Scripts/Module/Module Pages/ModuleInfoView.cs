using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unipal.Model.Modules;
using UnityEngine;

namespace UI.Modules {
    public class ModuleInfoView : MenuPanel {
        [SerializeField] private ModuleView _moduleView;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            SetupModule();
        }

        private void SetupModule() {
            Module module = _moduleView.ActiveModule;
        }
    }
}
