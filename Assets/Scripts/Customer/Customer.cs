using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Customer : MonoBehaviour
{
    private Vector3 _targetPosition;
    private bool _canMove;
    public Action OnCustomerArrived;
    private bool _isNextCustomer = false;

    // Update is called once per frame
    void Update()
    {
        if(!_canMove)
            return;

        Vector3 moveDirection = (_targetPosition - transform.position).normalized;

        float rotationSpeed = 25f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

        float _moveSpeed = 5f;
        float stoppingDistance = 0.1f;
        if (Vector3.Distance(transform.position, _targetPosition) >= stoppingDistance)
        {
            transform.position += moveDirection * _moveSpeed * Time.deltaTime;
        }
        else
        {
            _canMove = false;
            if(_isNextCustomer)
                OnCustomerArrived?.Invoke();
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _canMove = true;
        _targetPosition = targetPosition;
    }

    public IEnumerator HideCoroutine()
    {
        _isNextCustomer = false;
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }

    public void SetIsNextCustomer(bool isNextCustomer)
    {
        _isNextCustomer = isNextCustomer;
    }
}
