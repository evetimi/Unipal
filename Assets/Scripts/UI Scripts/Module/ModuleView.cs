using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unipal.Model.Modules;
using UnityEngine;
using Utilities;

namespace UI.Modules {
    /// <summary>
    /// ModuleView will include multiple smaller pages at one, so this panel will contain all of those pages and will use this class to navigate through all instead of using the MainMenuController one.
    /// </summary>
    public class ModuleView : MenuPanel {
        [TabGroup("Module View Setup"), SerializeField] private float _transitionWaitTime = 2f;
        [TabGroup("Module View Setup"), SerializeField, ListDrawerSettings(DraggableItems = false)] private List<Panel> _panels;

        [System.Serializable]
        private class Panel {
            [HorizontalGroup, HideLabel, ReadOnly] public ModulePanelID panelId;
            [HorizontalGroup, HideLabel] public MenuPanel panel;
        }

        private Panel _currentActive;

        protected override void OnValidate() {
            base.OnValidate();

            ModulePanelID[] modulePanelIDs = EnumUtility.GetEnumArray<ModulePanelID>();
            for (int i = 0; i < modulePanelIDs.Length; i++) {
                if (i >= _panels.Count) {
                    Panel panel = new() {
                        panelId = modulePanelIDs[i]
                    };
                    _panels.Add(panel);
                } else {
                    _panels[i].panelId = modulePanelIDs[i];
                }
            }

            while (_panels.Count > modulePanelIDs.Length) {
                _panels.RemoveAt(_panels.Count - 1);
            }
        }

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);
            ChangePanel(ModulePanelID.Info);
        }

        public void ChangePanel(ModulePanelID panelId) {
            int index = (int)panelId;
            if (index < 0 || index >= _panels.Count) {
                return;
            }

            StartCoroutine(ChangePanel(_panels[index]));
        }

        private IEnumerator ChangePanel(Panel target) {
            if (_currentActive != null) {
                _currentActive.panel.SetEnabled(false);
            }

            yield return new WaitForSeconds(_transitionWaitTime);

            _currentActive = target;
            target.panel.SetEnabled(true);
        }

        public void ChangeInfoPanel() {
            ChangePanel(ModulePanelID.Info);
        }

        public void ChangeResourcePanel() {
            ChangePanel(ModulePanelID.Resources);
        }
    }
}
