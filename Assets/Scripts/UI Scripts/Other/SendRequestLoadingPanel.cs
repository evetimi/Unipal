using System;
using System.Collections;
using System.Collections.Generic;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Others {
    public class SendRequestLoadingPanel : MenuPanel {
        [SerializeField] private ConfirmPopupObject _requestErrorPopupObject;

        protected override void Awake() {
            base.Awake();
            UnipalClient.OnBeforeSendingRequest += OnBeforeSendingRequest;
            UnipalClient.OnAfterSendingRequest += OnAfterSendingRequest;
        }

        private void OnBeforeSendingRequest(object _) {
            SetEnabled(true);
        }

        private void OnAfterSendingRequest(UnipalMessage msg) {
            SetEnabled(false);

            CheckRequestStatus(msg);
        }

        private void CheckRequestStatus(UnipalMessage msg) {
            if (!msg.receiveMessageSuccess) {
                PopupView.Instance.Close();
                PopupView.Instance.Open(_requestErrorPopupObject, null, null);
            }
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            UnipalClient.OnBeforeSendingRequest -= OnBeforeSendingRequest;
            UnipalClient.OnAfterSendingRequest -= OnAfterSendingRequest;
        }
    }
}
