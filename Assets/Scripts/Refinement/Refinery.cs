using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class Refinery : MonoBehaviour
{
    public float ConversionSpeed;
    public Item input_item;
    public Item output_item;

    public PlayerController _player;
    public Inventory _inv;
    public Game_Manager _manager;

    void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _manager = Game_Manager.Instance;
        _player = _manager.Player_Ref;
        _inv = _manager.Inventory_Ref;
    }
    
    
    public void Refine()
    {
        if (!_inv.CheckAnyLeft(input_item))
            return;

        _inv.DecreaseResourceItem(input_item,1);
            
    }

}
