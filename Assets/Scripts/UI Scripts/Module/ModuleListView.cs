using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Modules;
using UnityEngine;
using Utilities;

namespace UI.Modules {
    public class ModuleListView : OnOffUIBehaviour {
        [SerializeField] private ObjectPooling _moduleContainerPool;
        [SerializeField] private ModuleComponent _moduleComponentPrefab;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            ValidateModuleList();
        }

        public void ValidateModuleList() {
            // get the list of attended modules of the user through the Module Controller
            Module[] modules;
        }
    }
}
