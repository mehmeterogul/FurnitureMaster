using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTrigger : MonoBehaviour
{
    [SerializeField] private OrderController _orderController;
    [SerializeField] private CraftTable _craftTable;

    private void Awake()
    {
        if (!_orderController)
            Debug.LogError("Assign order controller object!");

        _craftTable = FindObjectOfType<CraftTable>();
        if (!_craftTable)
            Debug.LogError("Assign CraftTable object!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (!_orderController.HasOrderTaken())
                return;

            if (_craftTable.HasOrderCrafted())
            {
                _orderController.CheckCanDeliver();
            }
            else
            {
                OrderSO currentOrder = _orderController.GetCurrentOrder();
                _craftTable.ShowOrderOnCraftTable(currentOrder);
            }
        }
    }
}
