using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup {
    public class ConfirmPopupObject : PopupObject<bool> {
        public override bool GetData() {
            return true;
        }
    }
}
