using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup {
    public abstract class PopupObject<T> : MonoBehaviour {
        [SerializeField] private bool _closeOnConfirm = true;
        [SerializeField] private float _boardHeight = 875f;

        public bool CloseOnConfirm => _closeOnConfirm;
        public float BoardHeight => _boardHeight;

        /// <summary>
        /// The data that can be gotten as a result after clicking Confirm button
        /// </summary>
        /// <returns>Result of the data</returns>
        public abstract T GetData();
    }
}
