using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UpgradeTrigger : Trigger
{
    [Header("Settings")]
    [SerializeField] private Image _moneyIcon;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private RequiredMaterialUI _requiredMaterialUI;
    [SerializeField] private bool willDestroy;

    [Header("Required Resource Settings")]
    [SerializeField] private RequiredResourcesDictionary _requiredMoney = new RequiredResourcesDictionary();
    [SerializeField] private RequiredResourcesDictionary _requiredResource = new RequiredResourcesDictionary();
    Inventory _inventory;

    [SerializeField] private bool _needExtraResource;

    private void Start()
    {
        _inventory = Game_Manager.Instance.Inventory_Ref;
        _price.text = _requiredMoney.amount.ToString();

        if(_needExtraResource)
        {
            _requiredMaterialUI.gameObject.SetActive(true);
            _requiredMaterialUI.SetText(_requiredResource.amount.ToString());
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (!IsResourceEnough())
            return;

        base.OnTriggerEnter(other);
    }

    private bool IsResourceEnough()
    {
        int moneyAmount = _inventory.GetMoneyAmount();
        bool isMoneyEnough = (moneyAmount - _requiredMoney.amount) >= 0;
        if (!isMoneyEnough)
            return false;

        if(_needExtraResource)
        {
            int resourceAmount = _inventory.GetAmount(_requiredResource.resource);
            bool isMaterialEnough = (resourceAmount - _requiredResource.amount) >= 0;
            if (!isMaterialEnough)
                return false;
        }

        return true;
    }

    public override void OnComplete()
    {
        _inventory.DecreaseMoney(_requiredMoney.amount);
        _inventory.DecreaseItem(_requiredResource.resource, _requiredResource.amount);
        _canTrigger = false;

        if (willDestroy)
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
