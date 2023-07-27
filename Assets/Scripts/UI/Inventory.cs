using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Inventory : MonoBehaviour
{    
    public List<ItemUI> Items;

    private PlayerController _player;
    private Game_Manager _manager;

    private void Start()
    {
        InitializeComponents();
    }

    void Update()
    {
        
    }
    private void InitializeComponents()
    {
        _manager = Game_Manager.Instance;
        _player = _manager.Player_Ref;
    }
    public void IncreaseItem(Item item ,int amount) 
    {
        // Check if the item already exists in the list
        ItemUI existingItemUI = Items.Find(itemUI => itemUI.Properties == item);
        if (existingItemUI != null)
        {
            // If the item exists, increase its amount
            existingItemUI.Increase(amount);
        }

    }
    public void DecreaseItem(Item item, int amount)
    {
        // Check if the item already exists in the list
        ItemUI existingItemUI = Items.Find(itemUI => itemUI.Properties == item);
        if (existingItemUI != null)
        {
            // If the item exists, increase its amount
            existingItemUI.Decrease(amount);
        }
    }

    public int GetAmount(Item item)
    {
        ItemUI existingItemUI = Items.Find(itemUI => itemUI.Properties == item);
        if (existingItemUI != null)
            return existingItemUI.amount;
        else
            return 0;
    }
    public bool CheckAnyLeft(Item item)
    {
        if (GetAmount(item) > 0)
            return true;
        else
            return false;
    }

}
