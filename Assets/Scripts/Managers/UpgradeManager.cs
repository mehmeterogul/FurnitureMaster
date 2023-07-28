using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private bool _isPickaxeUnlocked;
    private bool _isPickaxeUpgraded;
    private bool _isSawmillUnlocked;
    private bool _isFurnitureUnlocked;
    private bool _isGoldFurnitureUnlocked;

    public void UpgradeAxe()
    {
        Debug.Log("Upgrade Axe function called");
    }

    public void UnlockPickaxe()
    {
        Debug.Log("Unlock Pickaxe function called");
    }

    public void UpgradePickaxe()
    {
        Debug.Log("Upgrade Pickaxe function called");
    }

    public void UnlockSawmill()
    {
        Debug.Log("Unlock Sawmill function called");
    }

    public void UnlockFurniture()
    {
        Debug.Log("Unlock Furniture function called");
    }

    public void UnlockGoldFurniture()
    {
        Debug.Log("Unlock GoldFurniture function called");
    }
}
