using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using TMPro;
using Unipal.Model.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Modules {
    public class ModuleComponent : MonoBehaviour, ISearchable {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Image _teacherPfp;

        private Action<Module> OnSelectModule;

        private string _searchableText;
        private Module _relatedModule;
        public Module RelatedModule => _relatedModule;

        public void SetModule(Module module) {
            _relatedModule = module;
            _titleText.text = module.Name;
            _searchableText = module.Name + " " + module.Description;

            _searchableText = _searchableText.ToLower();
        }

        public void RegisterSelectAction(Action<Module> action) {
            OnSelectModule = action;
        }

        public bool MatchSearch(string searchText) {
            searchText = searchText.ToLower();

            int currentIndex = 0;
            int searchTextLength = searchText.Length;
            foreach (char c in _searchableText) {
                if (currentIndex >= searchTextLength) {
                    break;
                }

                if (c == searchText[currentIndex]) {
                    currentIndex++;
                }
            }

            return currentIndex >= searchTextLength;
        }

        /// <summary>
        /// This method is called from Unity's Button
        /// </summary>
        public void ButtonClick() {
            OnSelectModule?.Invoke(_relatedModule);
        }
    }
}
