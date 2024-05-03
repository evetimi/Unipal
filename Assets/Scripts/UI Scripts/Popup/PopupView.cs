using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup {
    public class PopupView : UIService<PopupView> {
        [SerializeField] private GameObject _container;
        [SerializeField] private RectTransform _board;
        [SerializeField] private RectTransform _content;

        private bool _closeOnConfirm;
        private PopupObject _currentPopupObject;
        private Action<object> _onCancelCalback;
        private Action<object> _onConfirmCallback;

        public PopupObject<T> Open<T>(PopupObject<T> popupObject, Action<T> onCancelCalback, Action<T> onConfirmCallback) {
            base.Open();

            PopupObject<T> createdObject = Instantiate(popupObject, _content);
            _currentPopupObject = createdObject;
            _container.SetActive(true);

            if (onCancelCalback != null) {
                _onCancelCalback = obj => onCancelCalback((T)obj);
            } else {
                _onCancelCalback = null;
            }

            if (onConfirmCallback != null) {
                _onConfirmCallback = obj => onConfirmCallback((T)obj);
            } else {
                _onConfirmCallback = null;
            }

            Vector2 size = _board.sizeDelta;
            size.y = popupObject.BoardHeight;
            _board.sizeDelta = size;

            _closeOnConfirm = popupObject.CloseOnConfirm;

            return createdObject;
        }

        public override void Close() {
            base.Close();

            _container.SetActive(false);
            Destroy(_currentPopupObject);
        }

        public void CancelButtonClick() {
            _onCancelCalback?.Invoke(_currentPopupObject.GetDataObject());
            Close();
        }

        public void ConfirmButtonClick() {
            _onConfirmCallback?.Invoke(_currentPopupObject.GetDataObject());
            if (_closeOnConfirm) {
                Close();
            }
        }
    }
}
