using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unipal.Model.LinkResources;
using UnityEngine;
using Utilities;

namespace UI.Modules {
    public class ModuleResourceView : MenuPanel {
        [BoxGroup("Module Resource View"), SerializeField] private ObjectPooling _resourceContainerPool;
        [BoxGroup("Module Resource View"), SerializeField] private WeekResourceComponent _weekResourceComponentPrefab;

        private List<WeekResourceComponent> _weekResourceComponentList;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            ValidateResourceList();
        }

        public void ValidateResourceList() {
            // get the list of attended modules of the user through the Module Controller
            int amount = 10;

            _weekResourceComponentList ??= new List<WeekResourceComponent>(amount);
            _weekResourceComponentList.Clear();

            _resourceContainerPool.DisableAll();
            _resourceContainerPool.NewObjects(amount, _weekResourceComponentPrefab, (i, newComponent) => {
                newComponent.SetWeek(i);
                _weekResourceComponentList.Add(newComponent);
            });
        }
    }
}
