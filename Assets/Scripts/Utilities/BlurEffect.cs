using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurEffect : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private string _blurPropertyName = "_Size";
    [SerializeField] private float _minValue = 1f;
    [SerializeField] private float _maxValue = 2f;
    [SerializeField] private float _animateTime = 0.2f;

    private Material _material;
    private Tweener _tweener;
    
    private void Start() {
        _material = _image.material;
    }

    [Button]
    public void Verify() {
        _material = _image.material;

        if (!_material.HasProperty(_blurPropertyName)) {
            Debug.Log("NO!");
            return;
        }

        Debug.Log("YES!");
    }

    [Button]
    public void OnBlur() {
        if (!_material.HasProperty(_blurPropertyName)) {
            Debug.LogError($"Don't have required property in the Material: \"{_blurPropertyName}\"!");
            return;
        }

        _tweener.Kill();
        _tweener = DOVirtual.Float(_material.GetFloat(_blurPropertyName), _maxValue, _animateTime, (value) => {
            _material.SetFloat(_blurPropertyName, value);
        });
    }

    [Button]
    public void OffBlur() {
        if (!_material.HasProperty(_blurPropertyName)) {
            Debug.LogError($"Don't have required property in the Material: \"{_blurPropertyName}\"!");
            return;
        }

        _tweener.Kill();
        _tweener = DOVirtual.Float(_material.GetFloat(_blurPropertyName), _minValue, _animateTime, (value) => {
            _material.SetFloat(_blurPropertyName, value);
        });
    }
}
