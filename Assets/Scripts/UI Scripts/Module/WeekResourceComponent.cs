using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using Unipal.Model.LinkResources;
using UnityEngine;
using Utilities;

namespace UI.Modules {
    public class WeekResourceComponent : MonoBehaviour {
        [FoldoutGroup("For Validating"), SerializeField] private RectTransform _titleRect;
        [FoldoutGroup("For Validating"), SerializeField] private RectTransform _separatorRect;
        [FoldoutGroup("For Validating"), SerializeField] private RectTransform _contentRect;

        [BoxGroup("Setup"), SerializeField] private TMP_Text _titleText;
        [BoxGroup("Setup"), SerializeField] private string _titlePrefix = "Week ";
        [BoxGroup("Setup"), SerializeField] private float _titleHeight;
        [BoxGroup("Setup"), SerializeField] private ObjectPooling _resourceContainerPool;
        [BoxGroup("Setup"), SerializeField] private ResourceComponent _resourceComponentPrefab;

        [BoxGroup("Collapse Setup"), SerializeField] private Transform _collapseButtonTransform;
        [BoxGroup("Collapse Setup"), SerializeField] private Vector3 _collapsedRotate = new Vector3(0f, 0f, -180f);
        [BoxGroup("Collapse Setup"), SerializeField] private Vector3 _expandedRotate = new Vector3(0f, 0f, -90f);
        [BoxGroup("Collapse Setup"), SerializeField] private float _animDuration = 0.2f;

        [BoxGroup("Runtime"), ShowInInspector, ReadOnly] private int _weekIndex;

        private RectTransform _rectTransform;
        private List<ResourceComponent> _resourceComponentList;
        public bool IsCollapsed { get; private set; }

        private void OnValidate() {
            if (_titleRect) _titleRect.sizeDelta = new Vector2(0f, _titleHeight);
            if (_separatorRect) _separatorRect.anchoredPosition = new Vector2(0f, -_titleHeight);
            if (_contentRect) _contentRect.anchoredPosition = new Vector2(0f, -_titleHeight);
            
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _titleHeight);
        }

        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetWeek(int weekIndex) {
            _weekIndex = weekIndex;
            ValidateResourceList();
        }

        public void ValidateResourceList() {
            _titleText.text = $"{_titlePrefix}{_weekIndex}";

            // get the list of attended modules of the user through the Module Controller
            Resource[] resources = new Resource[] {
                new Resource("1", "5CI022 Sample Structure Part 1 portfolio.docx", "5CI022 Sample Structure Part 1 portfolio.docx", "Link"),
                new Resource("1", "5CI022 Assessement Topics 2023-2024 SEM1.docx", "5CI022 Assessement Topics 2023-2024 SEM1.docx", "Link"),
                new Resource("1", "5CI022 Coursework UPDATED STUDENT VERSION 2023-2024.docx", "5CI022 Coursework UPDATED STUDENT VERSION 2023-2024.docx", "Link")
            };

            _resourceComponentList ??= new List<ResourceComponent>(resources.Length);
            _resourceComponentList.Clear();

            _resourceContainerPool.DisableAll();
            _resourceContainerPool.NewObjects(resources.Length, _resourceComponentPrefab, (i, newComponent) => {
                newComponent.SetResource(resources[i]);
                _resourceComponentList.Add(newComponent);
            });
        }

        private void OnEnable() {
            Expand();
        }

        public void CollapseButtonClick() {
            if (IsCollapsed) {
                Expand();
            } else {
                Collapse();
            }
        }

        public void Expand() {
            _resourceContainerPool.gameObject.SetActive(true);

            // Need to wait for one frame for the container to setup its preferred size.
            IEnumerator enumerator() {
                yield return null;
                Vector2 targetSizeDelta = new Vector2(_rectTransform.sizeDelta.x, _titleHeight + _resourceContainerPool.GetComponent<RectTransform>().sizeDelta.y);

                _collapseButtonTransform.DOKill();
                _rectTransform.DOKill();
                _collapseButtonTransform.DORotate(_expandedRotate, _animDuration).SetEase(Ease.OutSine);
                _rectTransform.DOSizeDelta(targetSizeDelta, _animDuration).SetEase(Ease.OutSine);
            }

            StartCoroutine(enumerator());

            IsCollapsed = false;
        }

        public void Collapse() {
            _resourceContainerPool.gameObject.SetActive(false);
            Vector2 targetSizeDelta = new Vector2(_rectTransform.sizeDelta.x, _titleHeight);

            _collapseButtonTransform.DOKill();
            _rectTransform.DOKill();
            _collapseButtonTransform.DORotate(_collapsedRotate, _animDuration).SetEase(Ease.OutSine);
            _rectTransform.DOSizeDelta(targetSizeDelta, _animDuration).SetEase(Ease.OutSine);

            IsCollapsed = true;
        }
    }
}
