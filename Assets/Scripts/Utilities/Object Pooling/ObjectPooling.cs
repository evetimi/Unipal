using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

namespace Utilities {
    public class ObjectPooling : MonoBehaviour {

        private Queue<Transform> _disabledPoolComponents = new();

        // private void Awake() {
        //     // Check if there are children at instantiate
        //     foreach (Transform child in transform) {
        //         if (!child.TryGetComponent<ObjectPoolComponent>(out var poolComponent)) {
        //             poolComponent = child.AddComponent<ObjectPoolComponent>();
        //         }
        //         poolComponent.OnDisableCallback += OnObjectPoolDisable;

        //         if (child.gameObject.activeSelf) {
        //             child.gameObject.SetActive(false);
        //         } else {
        //             _disabledPoolComponents.Enqueue(child);
        //         }
        //     }
        // }

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
            while (newObjectIndex < amount) {

                // check existing child if there is any disabled component that is null
                Transform existingChild = null;
                while (_disabledPoolComponents.Count > 0 && existingChild == null) {
                    existingChild = _disabledPoolComponents.Dequeue();
                }

                if (existingChild != null) {
                    if (existingChild.gameObject.activeSelf && existingChild.TryGetComponent<T>(out var component)) {
                        existingChild.gameObject.SetActive(true);
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
                    if (!newObject.TryGetComponent<ObjectPoolComponent>(out var poolComponent)) {
                        poolComponent = newObject.AddComponent<ObjectPoolComponent>();
                    }
                    poolComponent.OnDisableCallback += OnObjectPoolDisable;

                    newObject.gameObject.SetActive(true);
                    onObjectEnabled?.Invoke(newObjectIndex, newObject);
                    newObjectIndex++;
                }
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

        private void OnObjectPoolDisable(Transform objTransform) {
            _disabledPoolComponents.Enqueue(objTransform);
        }
    }
}
