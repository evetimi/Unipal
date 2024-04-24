using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unipal.Model.LinkResources;
using UnityEngine;

namespace UI.Modules {
    public class ResourceComponent : MonoBehaviour {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _urlText;

        private Resource _containResource;

        public void SetResource(Resource resource) {
            if (resource == null) {
                return;
            }
            
            _containResource = resource;

            if (_titleText) _titleText.text = _containResource.Name;
            if (_descriptionText) _descriptionText.text = _containResource.Description;
            if (_urlText) _urlText.text = _containResource.Url;
        }
    }
}
