using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Inventory : MonoBehaviour
{    
    public List<ItemUI> ResourceItems;
    public List<ItemUI> MaterialItems;

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
    public void IncreaseResourceItem(Item item ,int amount) 
    {
        // Check if the item already exists in the list
        ItemUI existingItemUI = ResourceItems.Find(itemUI => itemUI.Properties == item);
        if (existingItemUI != null)
        {
            // If the item exists, increase its amount
            existingItemUI.Increase(amount);
        }

    }
    public void DecreaseResourceItem(Item item, int amount)
    {
        // Check if the item already exists in the list
        ItemUI existingItemUI = ResourceItems.Find(itemUI => itemUI.Properties == item);
        if (existingItemUI != null)
        {
            // If the item exists, increase its amount
            existingItemUI.Decrease(amount);
        }
    }
    public void IncreaseMaterialItem(Item item, int amount)
    {
        // Check if the item already exists in the list
        ItemUI existingItemUI = MaterialItems.Find(itemUI => itemUI.Properties == item);
        if (existingItemUI != null)
        {
            // If the item exists, increase its amount
            existingItemUI.Increase(amount);
        }

    }
    public void DecreaseMaterialItem(Item item, int amount)
    {
        // Check if the item already exists in the list
        ItemUI existingItemUI = MaterialItems.Find(itemUI => itemUI.Properties == item);
        if (existingItemUI != null)
        {
            // If the item exists, increase its amount
            existingItemUI.Increase(amount);
        }

    }

    public int GetAmount(Item item)
    {
        ItemUI existingItemUI = MaterialItems.Find(itemUI => itemUI.Properties == item);
        return existingItemUI.amount;
    }


}
