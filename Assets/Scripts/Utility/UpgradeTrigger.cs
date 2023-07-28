using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UpgradeTrigger : Trigger
{
    [Header("Settings")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private bool willDestroy;

    [SerializeField] private RequiredResourcesDictionary _requiredResource = new RequiredResourcesDictionary();
    Inventory _inventory;

    private void Start()
    {
        _inventory = Game_Manager.Instance.Inventory_Ref;
        _icon = _inventory.GetIcon(_requiredResource.resource);
        _price.text = _requiredResource.amount.ToString();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (!IsResourceEnough())
            return;

        base.OnTriggerEnter(other);
    }

    private bool IsResourceEnough()
    {
        int resourceAmount = _inventory.GetMoneyAmount();
        bool isMaterialsEnough = (resourceAmount - _requiredResource.amount) >= 0;
        if (!isMaterialsEnough)
            return false;

        return true;
    }

    public override void OnComplete()
    {
        _inventory.DecreaseMoney(_requiredResource.amount);
        _canTrigger = false;

        if (willDestroy)
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
