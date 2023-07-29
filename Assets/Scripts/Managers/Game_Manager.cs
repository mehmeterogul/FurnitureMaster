using UnityEngine;
using System.Collections.Generic;

public class Game_Manager:MonoBehaviour
{
    public PlayerController Player_Ref;
    public Inventory Inventory_Ref;
    public int CurrentLevel_Ref;

    public ResourceManager ResourceManager_Ref;

    public CollectedResourceSpawner CollectedResourceSpawner_Ref;

    private static Game_Manager _instance;

    public static Game_Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game manager is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        CurrentLevel_Ref = 1;
    }

    public int GetLevel()
    {
        return CurrentLevel_Ref;
    }


    // DEBUG
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            List<ItemUI> itemUIList = Inventory_Ref.GetItemList();
            foreach (ItemUI itemUI in itemUIList)
            {
                Inventory_Ref.IncreaseItem(itemUI.Properties, 100);
            }
            Inventory_Ref.IncreaseMoney(100);
        }
    }
    // DEBUG
}