using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CollectedResourceSpawner : MonoBehaviour
{
    [SerializeField] private CollectedResourcePool _collectedResourcePool;
    private List<ItemUI> itemList = new List<ItemUI>();
    private Camera _mainCamera;

    private void Start()
    {
        itemList = Game_Manager.Instance.Inventory_Ref.GetItemList();
        _mainCamera = Camera.main;
    }

    public void SpawnResourceImage(Item item, Vector3 spawnPosition)
    {
        Transform spawnedResource;

        
        int itemIndex = 0;
        switch (item.Item_name)
        {
            default:
            case "Wood":
                spawnedResource = _collectedResourcePool.GetNextWood();
                itemIndex = 0;
                break;
            case "IronOre":
                spawnedResource = _collectedResourcePool.GetNextIronOre();
                itemIndex = 1;
                break;
            case "GoldOre":
                spawnedResource = _collectedResourcePool.GetNextGoldOre();
                itemIndex = 2;
                break;
            case "Plank":
                spawnedResource = _collectedResourcePool.GetNextPlank();
                itemIndex = 3;
                break;
            case "IronIngot":
                spawnedResource = _collectedResourcePool.GetNextIronIngot();
                itemIndex = 4;
                break;
            case "GoldIngot":
                spawnedResource = _collectedResourcePool.GetNextGoldIngot();
                itemIndex = 5;
                break;
        }

        ItemUI itemUI = itemList[itemIndex];
        spawnedResource.gameObject.SetActive(true);
        
        CollectedResourceImage collectedResourceImage = spawnedResource.GetComponent<CollectedResourceImage>();
        collectedResourceImage.SetStartAndTargetPosition(spawnPosition, itemUI);
    }

    public Vector3 GetTargetWorldPosition(Vector3 position)
    {
        Vector3 uiPosition = position;
        uiPosition.z = (position - _mainCamera.transform.position).z;
        Vector3 newPosition = _mainCamera.ScreenToWorldPoint(uiPosition);
        return newPosition;
    }
}