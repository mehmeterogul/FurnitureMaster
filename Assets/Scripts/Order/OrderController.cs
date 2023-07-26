using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    [SerializeField] private OrderSO _currentOrder;
    [SerializeField] private Image _orderIcon;
    private bool _hasOrderTaken;

    public void SetOrder()
    {

    }

    /*
    public void SetOrder(OrderSO order)
    {
        _currentOrder = order;
        _orderIcon.sprite = order.OrderIcon;
    }
    */

    public void CheckCanDeliver()
    {
        // Check order ready
    }

    public bool HasOrderTaken()
    {
        return _hasOrderTaken;
    }
}
