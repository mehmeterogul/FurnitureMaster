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

    private CollectedResourceSpawner _collectedResourceSpawner;

    void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _manager = Game_Manager.Instance;
        _player = _manager.Player_Ref;
        _inv = _manager.Inventory_Ref;
        _collectedResourceSpawner = _manager.CollectedResourceSpawner_Ref;
    }


    public void Refine()
    {
        // Step 1: Check if all items are available
        foreach (Item item in input_items)
        {
            if (!_inv.CheckAnyLeft(item))
            {
                // If any item is not available, return without making any changes
                return;
            }
        }

        // Step 2: Decrease quantities of all available items
        foreach (Item item in input_items)
        {
            _inv.DecreaseItem(item, ConversionPower);
        }

        // Step 3: Increase the output item quantity
        _inv.IncreaseItem(output_item, ConversionPower);
        _collectedResourceSpawner.SpawnResourceImage(output_item, transform.position);
    }

    public bool CanRefine()
    {
        foreach (Item item in input_items)
        {
            if (!_inv.CheckAnyLeft(item))
            {
                // If any item is not available, return without making any changes
                return false;
            }
        }

        return true;
    }
}
