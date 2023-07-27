using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed = 2f;
    public float CutPeriod = 1f;
    public float DigPeriod = 1f;
    public int CutPower = 1;
    public int DigPower = 1;

    public Status status;

    private Vector3 _playerVelocity;
    private CharacterController _controller;


    // TEMP
    [SerializeField] private Transform _holdPosition;
    private Transform _heldObject;
    // TEMP


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
        Vector3 move = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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
    public void GatherResource(Abstract_Resource resource)
    {
        if (status.Gather_Stat == Status.GatherStatus.NotGathering)
        {
            if (resource is Abstract_Tree)
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
    public void LeaveGathering()
    {
        status.Gather_Stat = Status.GatherStatus.NotGathering;
    }


    // TEMP
    public void HoldCraftedObject(Transform craftedObjectPrefab)
    {
        _heldObject = Instantiate(craftedObjectPrefab, _holdPosition);
    }

    public void HideCraftedObject()
    {
        Destroy(_heldObject.gameObject);
    }
    // TEMP
}
