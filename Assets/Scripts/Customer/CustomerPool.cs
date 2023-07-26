using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPool : MonoBehaviour
{
    [SerializeField] private List<Transform> _customerList;

    private int _nextCustomerIndex;

    public Transform GetNextCustomer()
    {
        if (_nextCustomerIndex >= _customerList.Count)
            _nextCustomerIndex = 0;

        int currentCustomerIndex = _nextCustomerIndex;
        _nextCustomerIndex++;

        return _customerList[currentCustomerIndex];
    }
}
