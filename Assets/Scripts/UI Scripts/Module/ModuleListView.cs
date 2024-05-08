using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unipal.Model.Modules;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace UI.Modules {
    public class ModuleListView : MenuPanel {
        [BoxGroup("Module List View"), SerializeField] private ObjectPooling _moduleContainerPool;
        [BoxGroup("Module List View"), SerializeField] private ModuleComponent _moduleComponentPrefab;
        [BoxGroup("Module List View"), SerializeField] private ModuleView _moduleView;
        [BoxGroup("Module List View"), SerializeField] private UnityEvent _onModuleClick;

        private List<ModuleComponent> _moduleComponentList;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            ValidateModuleList();
        }

        public void ValidateModuleList() {
            // get the list of attended modules of the user through the Module Controller
            Module[] modules = new Module[] {
                new Module("5CI022", "Databases", "Databases"),
                new Module("5CS019", "Object-Oriented Design and Programming", "Object-Oriented Design and Programming"),
                new Module("5CS020", "Human - Computer Interaction", "Human - Computer Interaction"),
                new Module("5CS021", "Numerical Methods and Concurrency", "Numerical Methods and Concurrency"),
                new Module("5CS024", "Collaborative Development", "Collaborative Development")
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

            // Get access to ModuleView and instantiate the module informations
            _moduleView.SetModule(module);

            // Call this event so that the Unity event will handle this to change to module view
            _onModuleClick?.Invoke();
        }
    }
}
