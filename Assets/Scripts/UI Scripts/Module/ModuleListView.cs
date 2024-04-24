using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unipal.Model.Modules;
using UnityEngine;
using Utilities;

namespace UI.Modules {
    public class ModuleListView : MenuPanel {
        [BoxGroup("Module List View"), SerializeField] private ObjectPooling _moduleContainerPool;
        [BoxGroup("Module List View"), SerializeField] private ModuleComponent _moduleComponentPrefab;

        private List<ModuleComponent> _moduleComponentList;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            ValidateModuleList();
        }

        public void ValidateModuleList() {
            // get the list of attended modules of the user through the Module Controller
            Module[] modules = new Module[] {
                new Module("1", "System", "OK!"),
                new Module("1", "Computer Science", "CS!"),
                new Module("1", "UI/UX", "User Interface."),
                new Module("1", "C# Programming", "Nice!"),
                new Module("1", "Idk?", "Huh?")
            };

            _moduleComponentList ??= new List<ModuleComponent>(modules.Length);
            _moduleComponentList.Clear();

            _moduleContainerPool.DisableAll();
            _moduleContainerPool.NewObjects(modules.Length, _moduleComponentPrefab, (i, newComponent) => {
                newComponent.SetModule(modules[i]);
                newComponent.RegisterSelectAction(SelectModule);
                _moduleComponentList.Add(newComponent);
            });
        }

        public void Search(string text) {
            if (string.IsNullOrEmpty(text)) {
                foreach (var component in _moduleComponentList) {
                    component.gameObject.SetActive(true);
                }
            }

            foreach (var component in _moduleComponentList) {
                component.gameObject.SetActive(component.MatchSearch(text));
            }
        }

        private void SelectModule(Module module) {
            if (!MainMenuController.Instance) {
                return;
            }

            MainMenuController.Instance.ChangePanel(MainMenuPanelID.Module);

            // Get access to ModuleView and instantiate the module informations
        }
    }
}
