using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    private CustomerSpawner _customerSpawner;
    [SerializeField] private OrderSO _currentOrder;
    [SerializeField] private Image _orderIcon;
    [SerializeField] private GameObject _orderCanvas;
    private bool _hasOrderTaken;

    private void Awake()
    {
        _customerSpawner = FindObjectOfType<CustomerSpawner>();
        if (!_customerSpawner)
            Debug.LogError("Assign CustomerSpawner object!");
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

        // if can
        _orderCanvas.SetActive(false);
        _hasOrderTaken = false;
        _customerSpawner.DiscardFirstCustomer();
    }

    public bool HasOrderTaken()
    {
        return _hasOrderTaken;
    }

    public OrderSO GetCurrentOrder()
    {
        return _currentOrder;
    }
}
