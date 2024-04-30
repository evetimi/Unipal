using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UI.Menu.PanelIDs;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class MainMenuController : MonoBehaviourSingleton<MainMenuController>
{
    [TabGroup("Setup"), SerializeField] private float _transitionWaitTime = 2f;
    [TabGroup("Setup"), SerializeField] private Image _backgroundImage;
    [TabGroup("Setup"), SerializeField, ListDrawerSettings(DraggableItems = false)] private List<Panel> _panels;

    [TabGroup("Background Circle"), SerializeField] private Transform _backgroundCircle;
    [TabGroup("Background Circle"), SerializeField] private float _backgroundCircleTransitionTime = 1f;
    [TabGroup("Background Circle"), SerializeField] private Vector2[] _defaultCircleLocalPositions;
    [TabGroup("Background Circle"), SerializeField] private Vector2[] _defaultCircleLocalScales;

    [System.Serializable]
    private class Panel {
        [HorizontalGroup, HideLabel, ReadOnly] public MainMenuPanelID panelId;
        [HorizontalGroup, HideLabel] public MenuPanel panel;
    }

    private Panel _currentActive;

    private void OnValidate() {
        MainMenuPanelID[] mainMenuPanelIDs = EnumUtility.GetEnumArray<MainMenuPanelID>();
        for (int i = 0; i < mainMenuPanelIDs.Length; i++) {
            if (i >= _panels.Count) {
                Panel panel = new() {
                    panelId = mainMenuPanelIDs[i]
                };
                _panels.Add(panel);
            } else {
                _panels[i].panelId = mainMenuPanelIDs[i];
            }
        }

        while (_panels.Count > mainMenuPanelIDs.Length) {
            _panels.RemoveAt(_panels.Count - 1);
        }
    }

    private void Start() {
        ChangePanel(MainMenuPanelID.VerifyScreen);
    }

    public void ChangePanel(MainMenuPanelID panelId) {
        int index = (int)panelId;
        if (index < 0 || index >= _panels.Count) {
            return;
        }

        StartCoroutine(ChangePanel(_panels[index]));
    }

    private IEnumerator ChangePanel(Panel target) {
        if (_currentActive != null) {
            _currentActive.panel.SetEnabled(false);
        }

        yield return new WaitForSeconds(_transitionWaitTime);

        _currentActive = target;
        target.panel.SetEnabled(true);
    }

    public void ChangeBackgroundColor(Color color) {
        _backgroundImage.DOColor(color, _backgroundCircleTransitionTime).SetEase(Ease.InOutSine);
    }

    public void MoveBackgroundCircle(Vector2 position) {
        _backgroundCircle.DOMove(position, _backgroundCircleTransitionTime).SetEase(Ease.InOutSine);
    }

    public void MoveLocalBackgroundCircle(params Vector2[] eachCircleLocalPosition) {
        for (int i = 0; i < _backgroundCircle.childCount && i < eachCircleLocalPosition.Length; i++) {
            _backgroundCircle.GetChild(i).DOLocalMove(eachCircleLocalPosition[i], _backgroundCircleTransitionTime).SetEase(Ease.InOutSine);
        }
    }

    public void SetLocalBackgroundDefaultPosition() {
        MoveLocalBackgroundCircle(_defaultCircleLocalPositions);
    }

    public void ScaleLocalBackgroundCircle(params Vector2[] eachCircleLocalScale) {
        for (int i = 0; i < _backgroundCircle.childCount && i < eachCircleLocalScale.Length; i++) {
            _backgroundCircle.GetChild(i).DOScale(eachCircleLocalScale[i], _backgroundCircleTransitionTime).SetEase(Ease.InOutSine);
        }
    }

    public void SetLocalBackgroundDefaultScale() {
        ScaleLocalBackgroundCircle(_defaultCircleLocalScales);
    }
}
