using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Refinement Prefabs")]
    [SerializeField] private Transform _sawmillPrefab;
    [SerializeField] private Transform _furniturePrefab;
    [SerializeField] private Transform _goldFurniturePrefab;

    [Header("Refinement Triggers")]
    [SerializeField] private Transform _sawwillTrigger;
    [SerializeField] private Transform _furnitureTrigger;
    [SerializeField] private Transform _goldFurnitureTrigger;

    private float xPositionArrangment = -3.62f;
    private PlayerController _player;
    private Game_Manager _manager;
    private void Start()
    {
        InitializeComponents();
    }
    private void InitializeComponents()
    {
        _manager = Game_Manager.Instance;
        _player = _manager.Player_Ref;
    }

    public void UpgradeAxe()
    {
        _player.CutPower = 2;
    }

    public void UnlockPickaxe()
    {
        _furnitureTrigger.gameObject.SetActive(true);
    }

    public void UpgradePickaxe()
    {
        _player.DigPower = 2;
        _goldFurnitureTrigger.gameObject.SetActive(true);
    }

    public void UnlockSawmill()
    {
        Vector3 instantiationPosition = new Vector3(_sawwillTrigger.position.x + xPositionArrangment, _sawmillPrefab.position.y, _sawwillTrigger.position.z);
        Instantiate(_sawmillPrefab, instantiationPosition, Quaternion.identity);
    }

    public void UnlockFurniture()
    {
        Vector3 instantiationPosition = new Vector3(_furnitureTrigger.position.x + xPositionArrangment, _furniturePrefab.position.y, _furnitureTrigger.position.z);
        Instantiate(_furniturePrefab, instantiationPosition, Quaternion.identity);
        _manager.CurrentLevel_Ref = 2;
    }

    public void UnlockGoldFurniture()
    {
        Vector3 instantiationPosition = new Vector3(_goldFurnitureTrigger.position.x + xPositionArrangment, _goldFurniturePrefab.position.y, _goldFurnitureTrigger.position.z);
        Instantiate(_goldFurniturePrefab, instantiationPosition, Quaternion.identity);
        _manager.CurrentLevel_Ref = 3;
    }
}
