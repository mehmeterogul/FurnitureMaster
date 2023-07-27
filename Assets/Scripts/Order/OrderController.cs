using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    private CustomerSpawner _customerSpawner;
    private CraftTable _craftTable;
    [SerializeField] private OrderSO _currentOrder;
    [SerializeField] private Image _orderIcon;
    [SerializeField] private GameObject _orderCanvas;
    private bool _hasOrderTaken;

    private void Awake()
    {
        _customerSpawner = FindObjectOfType<CustomerSpawner>();
        if (!_customerSpawner)
            Debug.LogError("Assign CustomerSpawner object!");

        _craftTable = FindObjectOfType<CraftTable>();
        if (!_craftTable)
            Debug.LogError("Assign CraftTable object!");
    }

    public void ShowNextOrder()
    {
        _hasOrderTaken = true;
        SetOrder(_currentOrder);
    }

    public void SetOrder(OrderSO order)
    {
        _orderCanvas.SetActive(true);
        // _currentOrder = order;
        _orderIcon.sprite = order.OrderIcon;
    }

    public void CheckCanDeliver()
    {
        // Check order ready

        // if so
        _orderCanvas.SetActive(false);
        _hasOrderTaken = false;
        _customerSpawner.DiscardFirstCustomer();
        _craftTable.ClearCraftedOrder();
    }

    public bool HasOrderTaken()
    {
        return _hasOrderTaken;
    }

    public OrderSO GetCurrentOrder()
    {
        return _currentOrder;
    }

    public void CloseCanvas()
    {
        _orderCanvas.SetActive(false);
    }
}
