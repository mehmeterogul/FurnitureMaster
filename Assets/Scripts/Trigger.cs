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
    private bool _canDecrease = false;
    private bool _canTrigger = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _canDecrease = false;
            _canTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _canTrigger)
        {
            _currentFillValue += _imageFillRate;
            UpdateCircleSpriteFillAmounth();

            if (_currentFillValue == _maxFillValue)
            {
                _canTrigger = false;
                _currentFillValue = 0;
                UpdateCircleSpriteFillAmounth();

                OnFillComplete?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _canTrigger = true;
            _canDecrease = true;
        }
    }

    public void UpdateCircleSpriteFillAmounth()
    {
        _fillImage.fillAmount = _currentFillValue / _maxFillValue;
    }

    public void Testing()
    {
        Debug.Log("Test");
    }
}
