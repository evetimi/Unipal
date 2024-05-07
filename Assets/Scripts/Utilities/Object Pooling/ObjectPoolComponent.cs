using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utilities {
    public class ObjectPoolComponent : MonoBehaviour {
        public Action<Transform> OnDisableCallback;

        private void OnDisable() {
            OnDisableCallback?.Invoke(transform);
        }
    }
}
