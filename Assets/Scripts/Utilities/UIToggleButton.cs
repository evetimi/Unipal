using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.UI.Selectable;



#if UNITY_EDITOR
using UnityEditor.UI;
using UnityEditor;
#endif

namespace Utilities {
    public class UIToggleButton : Selectable {
        [Header("Toggle")]
        [Tooltip("Check this to toggle on the button on start, uncheck to toggle off")] public bool startToggleOn;
        
        public Image displayImage;
        public Transition displayImageTransition = Transition.ColorTint;
        public Sprite visualSpriteOn;
        public Sprite visualSpriteOff;
        public Color visualColorOn = Color.white;
        public Color visualColorOff = Color.black;

        public Image sliderImage;
        public Transition sliderTransition = Transition.ColorTint;
        public Sprite sliderSpriteOn;
        public Sprite sliderSpriteOff;
        public Color sliderColorOn = Color.white;
        public Color sliderColorOff = Color.black;
        //AFF7FF
        public GameObject toggle;
        public float toggleDuration = 0.2f;
        [Tooltip("Relative to the parent object, which is this one")] public float toggleOnPosition;
        [Tooltip("Relative to the parent object, which is this one")] public float toggleOffPosition;
        public UnityEvent onToggleOn;
        public UnityEvent onToggleOff;
        public UnityEvent<bool> onToggle;

        public bool isToggled { get; private set; }

        //protected override void Reset() {
        //    base.Reset();
        //    TryGetOnOffPosition();
            
        //    if (transform.parent != null) {
        //        displayImage = transform.parent.GetComponent<Image>();
        //    }

        //    sliderImage = GetComponent<Image>();

        //    if (transform.childCount > 0) {
        //        toggle = transform.GetChild(0).gameObject;
        //    }
        //}

        protected override void Start() {
            isToggled = startToggleOn;
            StartToggle(startToggleOn);
        }

        private void StartToggle(bool enabled) {
            ToggleSliderImage(enabled, false);

            if (TryGetChildToggle()) {
                float position = transform.position.x + (enabled ? toggleOnPosition : toggleOffPosition);
                toggle.transform.position = new Vector3(position, transform.position.y, transform.position.z);
            }

            ToggleDisplayImage(enabled, false);
        }

        private bool TryGetChildToggle() {
            if (toggle == null && transform.childCount > 0) {
                toggle = transform.GetChild(0).gameObject;
            }

            return toggle != null;
        }

        private bool TryGetOnOffPosition() {
            if (!TryGetChildToggle()) {
                return false;
            }

            toggleOffPosition = toggle.transform.localPosition.x;
            toggleOnPosition = -toggleOffPosition;

            return true;
        }

        public override void OnPointerUp(PointerEventData eventData) {
            base.OnPointerUp(eventData);

            ToggleButton();
        }

        public void ToggleButton() {
            isToggled = !isToggled;

            Toggling(isToggled);

            if (isToggled) {
                onToggleOn?.Invoke();
            } else {
                onToggleOff?.Invoke();
            }

            onToggle?.Invoke(isToggled);
        }

        private void Toggling(bool enabled) {
            ToggleSliderImage(enabled);

            if (TryGetChildToggle()) {
                float position = transform.position.x + (enabled ? toggleOnPosition : toggleOffPosition);
                toggle.transform.DOMoveX(position, toggleDuration);
            }

            ToggleDisplayImage(enabled);
        }

        private void ToggleDisplayImage(bool toggle, bool doTransition = true) {
            if (displayImage == null || displayImageTransition == Transition.None) {
                return;
            }

            switch (displayImageTransition) {
                case Transition.ColorTint: {
                    if (doTransition)
                        displayImage.DOColor(toggle ? visualColorOn : visualColorOff, toggleDuration);
                    else
                        displayImage.color = toggle ? visualColorOn : visualColorOff;
                    break;
                }

                case Transition.SpriteSwap: {
                    displayImage.sprite = toggle ? visualSpriteOn : visualSpriteOff;
                    break;
                }
            }
        }

        private void ToggleSliderImage(bool toggle, bool doTransition = true) {
            if (sliderImage == null || sliderTransition == Transition.None) {
                return;
            }

            switch (sliderTransition) {
                case Transition.ColorTint: {
                    if (doTransition)
                        sliderImage.DOColor(toggle ? sliderColorOn : sliderColorOff, toggleDuration);
                    else
                        sliderImage.color = toggle ? sliderColorOn : sliderColorOff;
                    break;
                }

                case Transition.SpriteSwap: {
                    sliderImage.sprite = toggle ? sliderSpriteOn : sliderSpriteOff;
                    break;
                }
            }
        }

        private void OnDrawGizmosSelected() {
            if (TryGetChildToggle()) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(new Vector3(transform.position.x + toggleOffPosition, transform.position.y, transform.position.z), 10f);
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(new Vector3(transform.position.x + toggleOnPosition, transform.position.y, transform.position.z), 10f);
            }
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(UIToggleButton))]
    public class UIToggleButtonEditor : SelectableEditor {
        public override void OnInspectorGUI() {
            // Draw the default inspector
            //DrawDefaultInspector();
            base.OnInspectorGUI();

            //DrawDefaultInspector();

            // Cast the target to UIToggleButton
            UIToggleButton toggleButton = (UIToggleButton)target;

            // Draw additional fields
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Toggle Settings", EditorStyles.boldLabel);
            toggleButton.startToggleOn = EditorGUILayout.Toggle("Start Toggle On", toggleButton.startToggleOn);
            
            toggleButton.displayImage = (Image)EditorGUILayout.ObjectField("Display Image", toggleButton.displayImage, typeof(Image), true);
            toggleButton.displayImageTransition = (Transition)EditorGUILayout.EnumPopup("Visual Transition", toggleButton.displayImageTransition);
            // Check the value of visualTransition and adjust the inspector accordingly
            switch (toggleButton.displayImageTransition) {
                case Transition.ColorTint:
                    DrawColorFields("Visual Color", ref toggleButton.visualColorOn, ref toggleButton.visualColorOff);
                    break;
                case Transition.SpriteSwap:
                    DrawSpriteFields("Visual Sprite", ref toggleButton.visualSpriteOn, ref toggleButton.visualSpriteOff);
                    break;
                    // Add more cases for other types if needed
            }

            toggleButton.sliderImage = (Image)EditorGUILayout.ObjectField("Slider Image", toggleButton.sliderImage, typeof(Image), true);
            toggleButton.sliderTransition = (Transition)EditorGUILayout.EnumPopup("Slider Transition", toggleButton.sliderTransition);
            // Check the value of visualTransition and adjust the inspector accordingly
            switch (toggleButton.sliderTransition) {
                case Transition.ColorTint:
                    DrawColorFields("Slider Color", ref toggleButton.sliderColorOn, ref toggleButton.sliderColorOff);
                    break;
                case Transition.SpriteSwap:
                    DrawSpriteFields("Slider Sprite", ref toggleButton.sliderSpriteOn, ref toggleButton.sliderSpriteOff);
                    break;
                    // Add more cases for other types if needed
            }

            toggleButton.toggle = (GameObject)EditorGUILayout.ObjectField("Toggle Object", toggleButton.toggle, typeof(GameObject), true);
            toggleButton.toggleDuration = EditorGUILayout.FloatField("Toggle Duration", toggleButton.toggleDuration);
            toggleButton.toggleOnPosition = EditorGUILayout.FloatField("Toggle On Position", toggleButton.toggleOnPosition);
            toggleButton.toggleOffPosition = EditorGUILayout.FloatField("Toggle Off Position", toggleButton.toggleOffPosition);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Toggle Events", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onToggleOn"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onToggleOff"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onToggle"));

            // Apply changes to serializedObject
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawColorFields(string label, ref Color colorOn, ref Color colorOff) {
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            colorOn = EditorGUILayout.ColorField("On", colorOn);
            colorOff = EditorGUILayout.ColorField("Off", colorOff);
            EditorGUI.indentLevel--;
        }

        private void DrawSpriteFields(string label, ref Sprite spriteOn, ref Sprite spriteOff) {
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            spriteOn = (Sprite)EditorGUILayout.ObjectField("On", spriteOn, typeof(Sprite), false);
            spriteOff = (Sprite)EditorGUILayout.ObjectField("Off", spriteOff, typeof(Sprite), false);
            EditorGUI.indentLevel--;
        }
    }
    #endif
}