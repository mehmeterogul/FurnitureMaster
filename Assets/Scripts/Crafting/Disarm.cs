using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disarm : MonoBehaviour
{
    private PlayerController _player;

    private void Start()
    {
        InitializeComponents();
    }
    private void InitializeComponents()
    {
        _player = Game_Manager.Instance.Player_Ref;
    }
    private void OnTriggerEnter(Collider other)
    {
        _player.Disarm();
    }
    private void OnTriggerExit(Collider other)
    {
        _player.SwitchAxe();
    }
}