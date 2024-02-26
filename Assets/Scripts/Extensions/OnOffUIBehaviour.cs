using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnOffUIBehaviour : UIBehaviour
{
    private bool IsBoolTransition => transitionType == TransitionType.Bool;

    [FoldoutGroup("On Off UI"), SerializeField] protected Animator _onOffAnim;
    [FoldoutGroup("On Off UI"), SerializeField] protected TransitionType transitionType;
    [FoldoutGroup("On Off UI"), SerializeField, ShowIf(nameof(IsBoolTransition))] protected string _boolName = "enabled";
    [FoldoutGroup("On Off UI"), SerializeField, HideIf(nameof(IsBoolTransition))] protected string _onName = "enable";
    [FoldoutGroup("On Off UI"), SerializeField, HideIf(nameof(IsBoolTransition))] protected string _offName = "disable";

    protected enum TransitionType {
        Bool,
        Trigger
    }

    private void BoolTransition(bool enabled) {
        _onOffAnim.SetBool(_boolName, enabled);
    }

    private void TriggerTransition(bool enabled) {
        if (enabled) {
            _onOffAnim.SetTrigger(_onName);
        } else {
            _onOffAnim.SetTrigger(_offName);
        }
    }

    public virtual void SetEnabled(bool enabled) {
        switch (transitionType) {
            case TransitionType.Bool: { BoolTransition(enabled); break; }
            case TransitionType.Trigger: { TriggerTransition(enabled); break; }
        }
    }
}
