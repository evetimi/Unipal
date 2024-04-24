using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utilities {
    public class ObjectPooling : MonoBehaviour {

        /// <summary>
        /// Get the first child (in index) that's enabled in hiararchy
        /// </summary>
        public Transform FirstEnabledChild {
            get {
                for (int i = 0; i < transform.childCount; i++) {
                    Transform child =transform.GetChild(i);
                    if (child.gameObject.activeSelf) {
                        return child;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Get the last child (in index) that's enabled in hiararchy
        /// </summary>
        public Transform LastEnabledChild {
            get {
                for (int i = transform.childCount - 1; i >= 0; i--) {
                    Transform child =transform.GetChild(i);
                    if (child.gameObject.activeSelf) {
                        return child;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Enable or create new object to be the child of this pool
        /// </summary>
        /// <typeparam name="T">Component type that can attached to GameObject</typeparam>
        /// <param name="amount">Amount of the child</param>
        /// <param name="newPrefab">Instantiate new prefab if the current child amount cannot satisfied the provided amount</param>
        /// <param name="onObjectEnabled">Callback when the object is enabled or instantiated with reference to that object and its respective index in the total created amount</param>
        public void NewObjects<T>(int amount, T newPrefab, Action<int, T> onObjectEnabled) where T : Component {
            int newObjectIndex = 0;
            int currentChildIndex = 0;
            while (newObjectIndex < amount) {
                if (currentChildIndex < transform.childCount) {
                    // get existing child
                    Transform child = transform.GetChild(currentChildIndex);
                    if (child.gameObject.activeSelf && child.TryGetComponent<T>(out var component)) {
                        child.gameObject.SetActive(true);
                        onObjectEnabled?.Invoke(newObjectIndex, component);
                        newObjectIndex++;
                    }
                } else {
                    // create new
                    if (newPrefab == null) {
                        // CANNOT create new if this is null, break the loop
                        break;
                    }

                    T newObject = Instantiate(newPrefab, transform);
                    newObject.gameObject.SetActive(true);
                    onObjectEnabled?.Invoke(newObjectIndex, newObject);
                    newObjectIndex++;
                }

                currentChildIndex++;
            }
        }

        public void DisableAll() {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }
        }

        public void DestroyAll() {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
        }
    }
}
