using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private float _imageFillRate = 1f;
    [SerializeField] private float _maxFillValue = 100f;
    [SerializeField] private float _currentFillValue = 0f;
    private bool _canDecrease = false;
    private bool _canTrigger = false;
    private bool _canCraft = false;
    private bool _hasOrderCrafted = false;
    [SerializeField] private Image _orderImage;

    private void Start()
    {
        _orderImage.gameObject.SetActive(false);
    }

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

    public void ShowOrderOnCraftTable(OrderSO order)
    {
        _canCraft = true;
        _hasOrderCrafted = false;
        _orderImage.gameObject.SetActive(true);
        _orderImage.sprite = order.OrderIcon;
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
        if (other.gameObject.CompareTag("Player") && _canTrigger && _canCraft)
        {
            _currentFillValue += _imageFillRate;
            UpdateCircleSpriteFillAmounth();

            if (_currentFillValue == _maxFillValue)
            {
                _orderImage.gameObject.SetActive(false);
                _canCraft = false;
                _hasOrderCrafted = true;
                // CRAFT ORDER HERE

                _canTrigger = false;
                _currentFillValue = 0;
                UpdateCircleSpriteFillAmounth();
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

    public bool HasOrderCrafted()
    {
        return _hasOrderCrafted;
    }

    public void ClearCraftedOrder()
    {
        _hasOrderCrafted = false;
    }
}
