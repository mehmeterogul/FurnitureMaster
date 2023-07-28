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
        List<OrderSO> orderList = GetOrderList();
        OrderSO randomOrder = orderList[Random.Range(0, orderList.Count)];
        return randomOrder;
    }

    private List<OrderSO> GetOrderList()
    {
        List <OrderSO> list = new List<OrderSO>();

        int level = Game_Manager.Instance.CurrentLevel_Ref;
        switch(level)
        {
            default:
            case 1:
                list = _orderList.level1OrderList;
                break;
            case 2:
                list = _orderList.level2OrderList;
                break;
            case 3:
                list = _orderList.level3OrderList;
                break;
        }

        return list;
    }

    public void SetOrder(OrderSO order)
    {
        _orderIcon.sprite = order.OrderIcon;
        _currentOrder = order;
        _orderCanvas.SetActive(true);
    }

    public void DeliverOrder()
    {
        OrderSO currentOrder = _craftTable.GetCurrentOrder();
        Inventory inventory = FindObjectOfType<Inventory>();
        inventory.IncreaseMoney(currentOrder.OrderPayment);
        _orderCanvas.SetActive(false);
        _orderIcon.sprite = null;
        _hasOrderTaken = false;
        _customerSpawner.DiscardFirstCustomer();
        _craftTable.ClearCraftedOrder();
        Game_Manager.Instance.Player_Ref.HideCraftedObject();
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
