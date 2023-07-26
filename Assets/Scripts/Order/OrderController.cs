using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    [SerializeField] private OrderSO _currentOrder;
    [SerializeField] private Image _orderIcon;

    public void SetOrder(OrderSO order)
    {
        _currentOrder = order;
        _orderIcon.sprite = order.OrderIcon;
    }

    public void CheckOrderCraftable()
    {
        // Check resources
        // If resources enough, then call DiscardFirstCustomer in CustomerSpawner script
        // if not, do nothing
    }
}
