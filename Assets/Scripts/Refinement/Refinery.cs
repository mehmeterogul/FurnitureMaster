using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class Refinery : MonoBehaviour
{
    public float ConversionSpeed;
    public int ConversionPower;
    public List<Item> input_items;
    public Item output_item;

    private PlayerController _player;
    private Inventory _inv;
    private Game_Manager _manager;

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
        foreach(Item item in input_items){
            if (!_inv.CheckAnyLeft(item))
                return;

            _inv.DecreaseItem(item, ConversionPower);        
        }
        _inv.IncreaseItem(output_item, ConversionPower);

    }

}
