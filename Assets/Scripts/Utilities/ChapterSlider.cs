using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Sirenix.OdinInspector;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChapterSlider : MonoBehaviour
{
    [BoxGroup("Slide Effect")] public Transform middlePosition;
    [BoxGroup("Slide Effect")] public bool setPositionToMiddleOnEnable;
    [BoxGroup("Slide Effect")] public float slideDuration = 0.2f;
    //public AnimationCurve slideCurve;

    [BoxGroup("Arrows Effect")] public bool disableArrowsOnLastPage;
    [BoxGroup("Arrows Effect"), SerializeField] private bool canSlide = true;
    [BoxGroup("Arrows Effect")] public Animator arrowLeftAnim;
    [BoxGroup("Arrows Effect")] public Animator arrowRightAnim;

    [BoxGroup("Events"), Tooltip("Call immediately when the sliding starts")] public UnityEvent<GameObject> OnSlideLeave;
    [BoxGroup("Events"), Tooltip("Call immediately when the sliding starts")] public UnityEvent<GameObject> OnSlideTo;
    [BoxGroup("Events"), Tooltip("Call immediately when the sliding starts")] public UnityEvent OnReachRightLimit;
    [BoxGroup("Events"), Tooltip("Call immediately when the sliding starts")] public UnityEvent OnReachLeftLimit;

    public int ChildCount => transform.childCount;
    public int CurrentIndex { get; private set; }

    private void Awake() {
        CurrentIndex = 0;
    }

    public void SetCanSlide(bool enable) {
        canSlide = enable;
        SliderSetting();
    }

    public void SlideRight() {
        if (!canSlide || CurrentIndex + 1 >= ChildCount) {
            return;
        }

        OnSlideLeave?.Invoke(transform.GetChild(CurrentIndex).gameObject);

        SlideTo(CurrentIndex + 1);
    }

    public void SlideLeft() {
        if (!canSlide || middlePosition == null || CurrentIndex - 1 < 0) {
            return;
        }

        OnSlideLeave?.Invoke(transform.GetChild(CurrentIndex).gameObject);

        SlideTo(CurrentIndex - 1);
    }

    public void SlideTo(int childIndex) {
        if (!canSlide || middlePosition == null || childIndex < 0 || childIndex >= ChildCount) {
            return;
        }

        OnSlideTo?.Invoke(transform.GetChild(childIndex).gameObject);

        if (childIndex == 0) {
            OnReachLeftLimit?.Invoke();
        } else if (childIndex == ChildCount - 1) {
            OnReachRightLimit?.Invoke();
        }

        Vector3 length = transform.InverseTransformPoint(middlePosition.position) - transform.GetChild(childIndex).localPosition;

        transform.DOKill();
        transform.DOLocalMove(transform.localPosition + length, slideDuration).SetEase(Ease.InOutSine).OnComplete(() => {
            SliderSetting();
        });

        CurrentIndex = childIndex;
    }

    /// <summary>
    /// Immediately show the first child
    /// </summary>
    public void ResetMiddlePosition() {
        CurrentIndex = 0;
        transform.position = middlePosition.position;
    }

    /// <summary>
    /// Show the child at childIndex of this slider
    /// </summary>
    /// <param name="childIndex">Child index to show, if not able, nothing will happen</param>
    public void SetShowChild(int childIndex) {
        if (middlePosition == null || childIndex < 0 || childIndex >= ChildCount) {
            return;
        }
        
        CurrentIndex = childIndex;

        if (childIndex == 0) {
            OnReachLeftLimit?.Invoke();
        } else if (childIndex == ChildCount - 1) {
            OnReachRightLimit?.Invoke();
        }

        Vector3 length = middlePosition.position - transform.GetChild(childIndex).position;
        transform.position += length;

        SliderSetting();

        //if (gameObject.activeSelf && gameObject.activeInHierarchy) {
        //    Vector3 length = middlePosition.position - transform.GetChild(childIndex).position;
        //    transform.position += length;
        //}
    }

    private void SliderSetting() {
        bool left = false;
        bool right = false;

        if (canSlide) {
            if (CurrentIndex == 0) {
                left = false;
            } else {
                left = true;
            }

            if (CurrentIndex == ChildCount - 1) {
                right = false;
            } else {
                right = true;
            }
        }

        if (arrowLeftAnim != null) arrowLeftAnim.SetBool("enable", left);
        if (arrowRightAnim != null) arrowRightAnim.SetBool("enable", right);
    }

    private void TrySetCurrentPosition() {
        Vector3 childPosition = transform.GetChild(CurrentIndex).position;
        if (childPosition != middlePosition.position) {
            PrintChildAndMiddlePosition();
            transform.position += (middlePosition.position - childPosition);
        }
    }

    private void OnEnable() {
        if (setPositionToMiddleOnEnable) {
            ResetMiddlePosition();
        } else {
            //TrySetCurrentPosition();
        }

        SliderSetting();
    }

    //[ContextMenu("Set Middle Position")]
    //public void SetMiddlePosition() {
    //    middlePosition = transform.position;
    //}

    [ContextMenu("Print Child And Middle Position")]
    public void PrintChildAndMiddlePosition() {
        Debug.Log(transform.GetChild(CurrentIndex).position + ", " + middlePosition.position);
    }
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(ChapterSlider))]
//public class ChapterSliderEditor : Editor {
//    public override void OnInspectorGUI() {
//        DrawDefaultInspector();
//        ChapterSlider chapterSlider = (ChapterSlider)target;

//        if (GUILayout.Button("Set Middle Position")) {
//            chapterSlider.SetMiddlePosition();
//        }

//        serializedObject.Update();

//        serializedObject.ApplyModifiedProperties();
//    }
//}
//#endif