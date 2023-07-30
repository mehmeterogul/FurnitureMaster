using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour
{
    [Header("Trigger Settings")]
    [SerializeField] private Image _fillImage;
    [SerializeField] private float _imageFillRate = 1f;
    [SerializeField] private float _maxFillValue = 100f;
    [SerializeField] private float _currentFillValue = 0f;
    private bool _canDecrease = false;
    private bool _canTrigger = false;
    private bool _canCraft = false;
    private bool _hasOrderCrafted = false;

    [Header("Order Image")]
    [SerializeField] private Image _orderImage;
    private OrderSO _currentOrder;

    [Header("Order Required Material Settings")]
    [SerializeField] private Transform _requiredMaterialsUIParent;
    [SerializeField] private Transform _requiredMaterialUIPrefab;

    Inventory _inventory;

    [SerializeField] private AudioClip _craftCompleteSound;

    private void Start()
    {
        _orderImage.gameObject.SetActive(false);
        _inventory = Game_Manager.Instance.Inventory_Ref;
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
        _currentOrder = order;
        _canCraft = true;
        _hasOrderCrafted = false;
        _orderImage.gameObject.SetActive(true);
        _orderImage.sprite = order.OrderIcon;

        UpdateRequiredOrderMaterials();
    }

    private void UpdateRequiredOrderMaterials()
    {
        ClearRequiredMaterialVisuals();

        List<RequiredResourcesDictionary> requiredResourceDictionary = _currentOrder.requiredResourceDictionary;
        foreach (var item in requiredResourceDictionary)
        {
            Transform requiredMaterialUITransform = Instantiate(_requiredMaterialUIPrefab, _requiredMaterialsUIParent);
            RequiredMaterialUI requiredMaterialUI = requiredMaterialUITransform.GetComponent<RequiredMaterialUI>();
            requiredMaterialUI.SetText(item.amount.ToString());
            Image resourceIcon = _inventory.GetIcon(item.resource);
            requiredMaterialUI.SetIcon(resourceIcon);
        }
    }

    private void ClearRequiredMaterialVisuals()
    {
        foreach (Transform child in _requiredMaterialsUIParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsResourceEnough())
            return;

        _canDecrease = false;
        _canTrigger = true;
    }

    private bool IsResourceEnough()
    {
        if (!_currentOrder)
            return false;

        List<RequiredResourcesDictionary> requiredResourceDictionary = _currentOrder.requiredResourceDictionary;

        foreach (var item in requiredResourceDictionary)
        {
            int resourceAmount = _inventory.GetAmount(item.resource);
            bool isMaterialsEnough = (resourceAmount - item.amount) >= 0;
            if (!isMaterialsEnough)
                return false;
        }

        return true;
    }

    private void DecreaseResources()
    {
        List<RequiredResourcesDictionary> requiredResourceDictionary = _currentOrder.requiredResourceDictionary;

        foreach (var item in requiredResourceDictionary)
        {
            _inventory.DecreaseItem(item.resource, item.amount);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_canTrigger && _canCraft)
        {
            _currentFillValue += _imageFillRate;
            UpdateCircleSpriteFillAmounth();

            if (_currentFillValue == _maxFillValue)
            {
                _orderImage.gameObject.SetActive(false);
                _canCraft = false;

                // CRAFT ORDER HERE
                _hasOrderCrafted = true;
                Game_Manager.Instance.Player_Ref.HoldCraftedObject(_currentOrder.OrderPrefab);
                DecreaseResources();
                ClearRequiredMaterialVisuals();
                AudioManager.Instance.PlaySound(_craftCompleteSound);

                _canTrigger = false;
                _currentFillValue = 0;
                UpdateCircleSpriteFillAmounth();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _canTrigger = true;
        _canDecrease = true;
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

    public OrderSO GetCurrentOrder()
    {
        return _currentOrder;
    }
}
