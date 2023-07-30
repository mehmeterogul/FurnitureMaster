using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OrderList")]
public class OrderListSO : ScriptableObject
{
    public List<OrderSO> level1OrderList = new List<OrderSO>();
    public List<OrderSO> level2OrderList = new List<OrderSO>();
    public List<OrderSO> level3OrderList = new List<OrderSO>();
    public List<OrderSO> level4OrderList = new List<OrderSO>();
}
