using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Order")]
public class OrderSO : ScriptableObject
{
    public string OrderName;
    public Sprite OrderIcon;
    public List<RequiredResourcesDictionary> requiredResourceDictionary;
    public int OrderPayment;
    public Transform OrderPrefab;
}
