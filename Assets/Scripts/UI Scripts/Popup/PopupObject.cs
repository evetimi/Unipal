using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup {
    public abstract class PopupObject : MonoBehaviour {
        [BoxGroup("Popup Properties"), SerializeField] private bool _closeOnConfirm = true;
        [BoxGroup("Popup Properties"), SerializeField] private float _boardHeight = 875f;
        [BoxGroup("Popup Properties"), SerializeField, InfoBox("Having both back button and confirm button disabled might lead to un-closable popup", nameof(CloseWarningCheck), InfoMessageType = InfoMessageType.Warning)]
        private bool _hasBackButton = true;
        [BoxGroup("Popup Properties"), SerializeField] private bool _hasConfirmButton = true;
        [BoxGroup("Popup Properties"), SerializeField, ShowIf(nameof(_hasConfirmButton))] private string _confirmButtonText = "Confirm";

        public bool CloseOnConfirm => _closeOnConfirm;
        public float BoardHeight => _boardHeight;
        public bool HasBackButton => _hasBackButton;
        public bool HasConfirmButton => _hasConfirmButton;
        public string ConfirmButtonText => _confirmButtonText;

        /// <summary>
        /// The data that can be gotten as a result after clicking Confirm button
        /// </summary>
        /// <returns>Result of the data</returns>
        public abstract object GetDataObject();

        private bool CloseWarningCheck() {
            return !_hasBackButton && !_hasConfirmButton;
        }
    }

    public abstract class PopupObject<T> : PopupObject {
        /// <summary>
        /// The data that can be gotten as a result after clicking Confirm button
        /// </summary>
        /// <returns>Result of the data</returns>
        public abstract T GetData();

        public override object GetDataObject() {
            return GetData();
        }
    }
}
