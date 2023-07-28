using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private float _imageFillRate = 1f;
    [SerializeField] private float _maxFillValue = 100f;
    [SerializeField] private float _currentFillValue = 0f;
    protected bool _canDecrease = false;
    protected bool _canTrigger = false;
    public UnityEvent OnFillComplete;

    void Update()
    {
        if (_canDecrease)
        {
            if (_fillImage.fillAmount > 0)
            {
                _currentFillValue -= (_imageFillRate * 3);
                UpdateCircleSpriteFillAmounth();
            }
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        _canDecrease = false;
        _canTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_canTrigger)
            return;

        _currentFillValue += _imageFillRate;
        UpdateCircleSpriteFillAmounth();

        if (_currentFillValue >= _maxFillValue)
        {
            _currentFillValue = 0;
            UpdateCircleSpriteFillAmounth();

            OnFillComplete?.Invoke();
            OnComplete();
        }
    }

    public virtual void OnComplete()
    {

    }

    public virtual void OnTriggerExit(Collider other)
    {
        _canDecrease = true;
        _canTrigger = true;
    }

    public void UpdateCircleSpriteFillAmounth()
    {
        _fillImage.fillAmount = _currentFillValue / _maxFillValue;
    }
}
