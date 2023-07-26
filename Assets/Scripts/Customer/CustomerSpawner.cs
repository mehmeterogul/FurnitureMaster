using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private CustomerPool _customerPool;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Transform> _leavePointList;
    [SerializeField] private List<Transform> _queuePointList;
    private int _maxCustomerCount;
    [SerializeField] private List<Transform> _customersOnQueue = new List<Transform>();

    private void Start()
    {
        _maxCustomerCount = _queuePointList.Count;
        StartCoroutine(SpawnMaxCustomerCoroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DiscardFirstCustomer();
        }
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
        if(GetCustomerCountOnQueue() >= _maxCustomerCount)
            return;

        Transform newCustomerTransform = _customerPool.GetNextCustomer();
        Customer customer = newCustomerTransform.GetComponent<Customer>();

        newCustomerTransform.gameObject.SetActive(true);
        newCustomerTransform.transform.position = _spawnPoint.position;
        _customersOnQueue.Add(newCustomerTransform);

        customer.SetTargetPosition(GetTargetPosition());
    }

    private void DiscardFirstCustomer()
    {
        if (GetCustomerCountOnQueue() == 0)
            return;

        Transform firstCustomerTransform = _customersOnQueue[0];
        Customer firstCustomer = firstCustomerTransform.GetComponent<Customer>();
        int leavePositionIndex = Random.Range(0, _leavePointList.Count);
        firstCustomer.SetTargetPosition(_leavePointList[leavePositionIndex].position);
        StartCoroutine(firstCustomer.HideCoroutine());
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
}
