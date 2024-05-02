using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UI.Menu;
using UI.Menu.PanelIDs;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class MainMenuController : MonoBehaviourSingleton<MainMenuController>
{
    [TabGroup("Setup"), SerializeField] private MainMenuContainer _mainMenuContainer;
    [TabGroup("Setup"), SerializeField] private Image _backgroundImage;

    [TabGroup("Background Circle"), SerializeField] private Transform _backgroundCircle;
    [TabGroup("Background Circle"), SerializeField] private float _backgroundCircleTransitionTime = 1f;
    [TabGroup("Background Circle"), SerializeField] private Vector2[] _defaultCircleLocalPositions;
    [TabGroup("Background Circle"), SerializeField] private Vector2[] _defaultCircleLocalScales;

    public MainMenuContainer MainMenuContainer => _mainMenuContainer;

    private void Start() {
        _mainMenuContainer.ChangePanel(MainMenuPanelID.VerifyScreen);
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
