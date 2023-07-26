using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTrigger : MonoBehaviour
{
    [SerializeField] private OrderController _orderController;

    private void Awake()
    {
        if (!_orderController)
            Debug.LogError("Assign order controller object!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _orderController.CheckOrderCraftable();
        }
    }
}
