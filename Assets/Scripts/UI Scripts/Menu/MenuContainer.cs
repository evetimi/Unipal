using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;

namespace UI.Menu {
    public abstract class MenuContainer<T> : MonoBehaviour where T : Enum {
        [FoldoutGroup("Menu Container"), SerializeField] private float _transitionWaitTime = 0.2f;
        [FoldoutGroup("Menu Container"), SerializeField, ListDrawerSettings(DraggableItems = false)] private List<Panel> _panels;

        [System.Serializable]
        private class Panel {
            [HorizontalGroup, HideLabel, ReadOnly] public T panelId;
            [HorizontalGroup, HideLabel] public MenuPanel panel;
            // [HorizontalGroup, LabelWidth(50f)] public bool isPrefab;
            // [ShowIf(nameof(isPrefab)), InfoBox("panel will be treated as prefab, if the panel's parent is not this parent, it will be instantiated to the new one and re-assign new panel. If the panel's parent is this parent, simply transition to this panel without instantiating new one.")]
            // public Transform parent;
        }

        private Panel _currentActive;

        protected virtual void OnValidate() {
            T[] panelIDs = EnumUtility.GetEnumArray<T>();
            for (int i = 0; i < panelIDs.Length; i++) {
                if (i >= _panels.Count) {
                    Panel panel = new() {
                        panelId = panelIDs[i]
                    };
                    _panels.Add(panel);
                } else {
                    _panels[i].panelId = panelIDs[i];
                }
            }

            while (_panels.Count > panelIDs.Length) {
                _panels.RemoveAt(_panels.Count - 1);
            }
        }

        public void ChangePanel(T panelId) {
            int index = Convert.ToInt32(panelId);
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
    }
}
