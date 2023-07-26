using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed = 2.0f;

    private Vector3 _playerVelocity;
    private CharacterController _controller;
    private Status status;


    private void Start()
    {
        InitializeComponents();
    }

    void Update()
    {
        Move();
    }
    void InitializeComponents()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        status = new Status();
        status.Movement_Stat = Status.MovementStatus.Idle;

    }
    void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _controller.Move(move * Time.deltaTime * PlayerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            status.Movement_Stat = Status.MovementStatus.Walking;
        }
        else
        {
            status.Movement_Stat = Status.MovementStatus.Idle;
        }
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
    void GatherResource(Abstract_Resource resource)
    {
        if (status.Gather_Stat == Status.GatherStatus.NotGathering)
        {
            if (resource is Tree)
                Cut(resource);
            else
                Dig(resource);

        }
    }
    public void Dig(Abstract_Resource resource)
    {
        status.Gather_Stat = Status.GatherStatus.Digging;
    }
    public void Cut(Abstract_Resource resource)
    {
        status.Gather_Stat = Status.GatherStatus.Cutting;
    }
}
