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
    public Animator anim;
    public Status status;
    public GameObject pickaxe;
    public GameObject axe;

    private Vector3 _playerVelocity;
    private CharacterController _controller;

    [SerializeField] private FloatingJoystick _joystick;


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
    private void InitializeComponents()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        status = new Status();
        status.Movement_Stat = Status.MovementStatus.Idle;
        status.Gather_Stat = Status.GatherStatus.NotGathering;

    }
    private void Move()
    {
        Vector3 move = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        _controller.Move(move * Time.deltaTime * PlayerSpeed);

        if (move != Vector3.zero)
        {
            StartMove();
            transform.forward = move.normalized;
        }
        else
        {
            StopMove();
        }
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
    public void GatherResource(Abstract_Resource resource)
    {
        if (status.Gather_Stat == Status.GatherStatus.NotGathering)
        {
            anim.SetBool("isGathering", true);
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
        anim.SetBool("isGathering", false);
    }
    private void StartMove()
    {
        status.Movement_Stat = Status.MovementStatus.Walking;
        anim.SetBool("isMoving", true);
    }
    private void StopMove()
    {
        status.Movement_Stat = Status.MovementStatus.Idle;
        anim.SetBool("isMoving", false);
    }
    public void SwitchTool(Abstract_Resource resource){

        if (resource is Abstract_Tree)
            SwitchAxe();
        else
            SwitchPickAxe();
    }

    public void SwitchAxe()
    {
        axe.SetActive(true);
        pickaxe.SetActive(false);

    }
    public void SwitchPickAxe()
    {
        axe.SetActive(false);
        pickaxe.SetActive(true);

    }
    public void Disarm()
    {
        axe.SetActive(false);
        pickaxe.SetActive(false);

    }
    public void CraftStart()
    {
        anim.SetBool("isCrafting", true);
    }
    public void CraftExit()
    {
        anim.SetBool("isCrafting", false);
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
