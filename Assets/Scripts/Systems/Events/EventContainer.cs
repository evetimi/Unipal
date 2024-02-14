using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventContainer : MonoBehaviour
{
    [BoxGroup("Event Data"), SerializeField] private List<EventData> _eventDatas;

    [BoxGroup("Prefabs"), SerializeField, RequiredIn(PrefabKind.PrefabAsset)] private EventContent _contentPrefab;
    [BoxGroup("Prefabs"), SerializeField, RequiredIn(PrefabKind.PrefabAsset)] private Transform _dotPrefab;

    [BoxGroup("Content Image"), SerializeField] private ChapterSlider _slider;
    [BoxGroup("Content Image"), SerializeField] private Transform _dotsContainer;
    [BoxGroup("Content Image"), SerializeField] private Transform _activeDot;

    [BoxGroup("Effect"), SerializeField] private float _transitionDuration = 0.2f;
    [BoxGroup("Effect"), SerializeField] private float _autoSlidesDelay = 10f;

    private void Start() {
        StartCoroutine(GenerateInformationBasedOnEventDatas());
        StartCoroutine(AutoSlides());
    }

    private IEnumerator GenerateInformationBasedOnEventDatas() {
        if (_eventDatas != null && _eventDatas.Count > 0) {
            foreach (var eventData in _eventDatas) {
                var newEvent = Instantiate(_contentPrefab, _slider.transform);
                newEvent.Picture.sprite = eventData.picture;

                Instantiate(_dotPrefab, _dotsContainer);
            }

            yield return null;
            yield return null;

            _activeDot.position = _dotsContainer.GetChild(0).position;
        }
    }

    private void OnEnable() {
        _slider.OnSlideTo.AddListener(ContentSlidesTo);
    }

    private void OnDisable() {
        _slider.OnSlideTo.RemoveListener(ContentSlidesTo);
    }

    private IEnumerator AutoSlides() {
        while (true) {
            yield return new WaitForSeconds(_autoSlidesDelay);

            if (_slider.CurrentIndex < _slider.ChildCount - 1) {
                _slider.SlideRight();
            } else {
                _slider.SlideTo(0);
            }
        }
    }

    private void ContentSlidesTo(GameObject obj) {
        int siblingIndex = obj.transform.GetSiblingIndex();
        Vector2 position = _dotsContainer.GetChild(siblingIndex).position;

        position = _activeDot.parent.InverseTransformPoint(position);

        _activeDot.DOKill();
        _activeDot.DOLocalMove(position, _transitionDuration).SetEase(Ease.OutSine);
    }
}
