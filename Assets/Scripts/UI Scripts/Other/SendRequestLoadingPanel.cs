using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Others {
    public class SendRequestLoadingPanel : MenuPanel {
        protected override void Awake() {
            base.Awake();
            UnipalClient.OnBeforeSendingRequest += OnBeforeSendingRequest;
            UnipalClient.OnAfterSendingRequest += OnAfterSendingRequest;
        }

        private void OnBeforeSendingRequest(object _) {
            SetEnabled(true);
        }

        private void OnAfterSendingRequest(string _) {
            SetEnabled(false);
        }
    }
}
