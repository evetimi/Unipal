using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MenuPanel : OnOffUIBehaviour
{
    [BoxGroup("On Change Effect"), SerializeField] private Transform _backgroundCirclePosition;
    [BoxGroup("On Change Effect"), SerializeField] private Vector2[] _localBackgroundCirclePositions;
    [BoxGroup("On Change Effect"), SerializeField] private Vector2[] _localBackgroundCircleScales;

    public override void SetEnabled(bool enabled) {
        base.SetEnabled(enabled);

        if (_backgroundCirclePosition != null) {
            MainMenuController.Instance.MoveBackgroundCircle(_backgroundCirclePosition.position);
        }

        if (_localBackgroundCirclePositions != null && _localBackgroundCirclePositions.Length > 0) {
            MainMenuController.Instance.MoveLocalBackgroundCircle(_localBackgroundCirclePositions);
        } else {
            MainMenuController.Instance.SetLocalBackgroundDefaultPosition();
        }

        if (_localBackgroundCircleScales != null && _localBackgroundCircleScales.Length > 0) {
            MainMenuController.Instance.ScaleLocalBackgroundCircle(_localBackgroundCircleScales);
        } else {
            MainMenuController.Instance.SetLocalBackgroundDefaultScale();
        }
    }
}
