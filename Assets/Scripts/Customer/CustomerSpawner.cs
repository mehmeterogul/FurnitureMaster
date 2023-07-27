using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    private OrderController _orderController;

    [SerializeField] private CustomerPool _customerPool;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Transform> _leavePointList;
    [SerializeField] private List<Transform> _queuePointList;
    private int _maxCustomerCount;
    [SerializeField] private List<Transform> _customersOnQueue = new List<Transform>();

    private void Start()
    {
        _orderController = FindObjectOfType<OrderController>();
        if (!_orderController)
        {
            Debug.LogError("Assign OrderController object!");
            return;
        }

        _maxCustomerCount = _queuePointList.Count;
        StartCoroutine(SpawnMaxCustomerCoroutine());
    }

    private IEnumerator SpawnMaxCustomerCoroutine()
    {
        SpawnCustomer();

        for (int i = 1; i < _maxCustomerCount; i++)
        {
            yield return new WaitForSeconds(1.5f);
            SpawnCustomer();
        }
    }

    private void SpawnCustomer()
    {
        int customerCount = GetCustomerCountOnQueue();
        if (customerCount >= _maxCustomerCount)
            return;

        Transform newCustomerTransform = _customerPool.GetNextCustomer();
        Customer customer = newCustomerTransform.GetComponent<Customer>();

        if(customerCount == 0)
        {
            customer.OnCustomerArrived += Customer_OnCustomerArrived;
        }

        newCustomerTransform.gameObject.SetActive(true);
        newCustomerTransform.transform.position = _spawnPoint.position;
        _customersOnQueue.Add(newCustomerTransform);

        customer.SetTargetPosition(GetTargetPosition());
    }

    public void DiscardFirstCustomer()
    {
        if (GetCustomerCountOnQueue() == 0)
            return;

        Transform firstCustomerTransform = _customersOnQueue[0];
        Customer firstCustomer = firstCustomerTransform.GetComponent<Customer>();
        int leavePositionIndex = Random.Range(0, _leavePointList.Count);
        firstCustomer.SetTargetPosition(_leavePointList[leavePositionIndex].position);
        StartCoroutine(firstCustomer.HideCoroutine());
        firstCustomer.OnCustomerArrived -= Customer_OnCustomerArrived;

        SortCustomerQueue();
    }

    private void SortCustomerQueue()
    {
        List<Transform> customerList = _customersOnQueue.ToList();
        _customersOnQueue.Clear();

        int customerIndex = 0;
        foreach (var customerTransform in customerList)
        {
            if (customerIndex == 0)
            {
                customerIndex++;
                continue;
            }
            _customersOnQueue.Add(customerTransform);

            Customer customer = customerTransform.GetComponent<Customer>();
            customer.SetTargetPosition(GetTargetPosition());
            if (customerIndex == 1)
            {
                customer.OnCustomerArrived += Customer_OnCustomerArrived;
            }
        }

        SpawnCustomer();
    }

    private Vector3 GetTargetPosition()
    {
        int currentCustomerCount = GetCustomerCountOnQueue() - 1;
        Vector3 targetPos = Vector3.zero;

        if(currentCustomerCount >= 0)
            targetPos = _queuePointList[currentCustomerCount].position;

        return targetPos;
    }

    private int GetCustomerCountOnQueue()
    {
        return _customersOnQueue.Count;
    }

    private void Customer_OnCustomerArrived()
    {
        _orderController.ShowNextOrder();
    }
}
