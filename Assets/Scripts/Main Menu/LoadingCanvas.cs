using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCanvas : MonoBehaviour
{
    [SerializeField] private BlurEffect _blurEffect;
    [SerializeField] private Animator _loadingIcon;

    public void OpenLoading() {
        _blurEffect.OnBlur();
        _loadingIcon.SetBool("enable", true);
    }

    public void CloseLoading() {
        _blurEffect.OffBlur();
        _loadingIcon.SetBool("enable", false);
    }
}
