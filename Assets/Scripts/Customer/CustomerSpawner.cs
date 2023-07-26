using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private CustomerPool _customerPool;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Transform> _leavePointList;
    [SerializeField] private List<Transform> _queuePointList;
    private int _maxCustomerCount;
    private List<Transform> _customersOnQueue = new List<Transform>();

    private void Start()
    {
        _maxCustomerCount = _queuePointList.Count;

        SpawnCustomer();
        Invoke("SpawnCustomer", 1.5f);
        Invoke("SpawnCustomer", 2.8f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DiscardFirstCustomer();
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
        Customer firstCustomer = _customersOnQueue[0].GetComponent<Customer>();
        int leavePositionIndex = Random.Range(0, _leavePointList.Count);
        firstCustomer.SetTargetPosition(_leavePointList[leavePositionIndex].position);
        StartCoroutine(firstCustomer.HideCoroutine());
        SortCustomerQueue();
        // Invoke("SpawnCustomer", 2.8f);
    }

    private void SortCustomerQueue()
    {
        
    }

    private Vector3 GetTargetPosition()
    {
        int currentCustomerCount = GetCustomerCountOnQueue() - 1;
        Vector3 targetPos = Vector3.zero;

        switch(currentCustomerCount)
        {
            default:
            case 0:
                targetPos = _queuePointList[0].position;
                break;
            case 1:
                targetPos = _queuePointList[1].position;
                break;
            case 2:
                targetPos = _queuePointList[2].position;
                break;

        }

        return targetPos;
    }

    private int GetCustomerCountOnQueue()
    {
        return _customersOnQueue.Count;
    }
}
