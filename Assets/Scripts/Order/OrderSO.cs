using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Order")]
public class OrderSO : ScriptableObject
{
    public string OrderName;
    public Sprite OrderIcon;
    public List<Abstract_Resource> requiredResourceList;
    public int OrderPayment;
}
