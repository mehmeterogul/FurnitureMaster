using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    private CustomerSpawner _customerSpawner;
    private CraftTable _craftTable;

    [SerializeField] private OrderListSO _orderList;

    private OrderSO _currentOrder;

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
        SetOrder(GetRandomOrder());
    }

    private OrderSO GetRandomOrder()
    {
        // Check upgrade status and bring list which is appropriate
        // List<OrderSO> orderList = _orderList.level1OrderList;
        // List<OrderSO> orderList = _orderList.level2OrderList;
        //List<OrderSO> orderList = _orderList.level3OrderList;

        List<OrderSO> orderList = _orderList.level1OrderList;
        OrderSO randomOrder = orderList[Random.Range(0, orderList.Count)];
        return randomOrder;
    }

    public void SetOrder(OrderSO order)
    {
        _orderIcon.sprite = order.OrderIcon;
        _currentOrder = order;
        _orderCanvas.SetActive(true);
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
