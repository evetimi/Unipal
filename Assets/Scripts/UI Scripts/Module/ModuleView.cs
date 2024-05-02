using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UI.Menu;
using Unipal.Model.Modules;
using UnityEngine;

namespace UI.Modules {
    /// <summary>
    /// ModuleView will include multiple smaller pages at one, so this panel will contain all of those pages and will use this class to navigate through all instead of using the MainMenuController one.
    /// </summary>
    public class ModuleView : MenuPanel {
        [SerializeField] private ModuleMenuContainer _moduleMenuContainer;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private string _idNameSeparator = " - ";

        private Module _activeModule;

        public Module ActiveModule => _activeModule;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);
            _moduleMenuContainer.ChangeInfo();
        }

        public void SetModule(Module module) {
            if (module == null) {
                return;
            }

            _activeModule = module;
            _titleText.text = module.Id + _idNameSeparator + module.Name;
        }
    }
}
