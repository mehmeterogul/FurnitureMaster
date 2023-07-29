using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Resource : MonoBehaviour
{
    public int Health = 10;
    public int Amount = 10;

    public PlayerController _player;
    public Inventory _inv;
    public Game_Manager _manager;
    public Coroutine gatherCoroutine;
    public Item output_item;
    public bool _playerLeft;

    protected SpawnPoint _spawnPoint;

    private CollectedResourceSpawner collectedResourceSpawner;

    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
    }

    public void GatherResource()
    {
        if (gatherCoroutine == null)
        {
            gatherCoroutine = StartCoroutine(GatherResourceCoroutine());
        }
    }
    public abstract IEnumerator GatherResourceCoroutine();

    //Common Methods
    public void SetSpawnPoint(SpawnPoint spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }
    private void InitializeComponents()
    {
        _manager = Game_Manager.Instance;
        _player = _manager.Player_Ref;
        _inv = _manager.Inventory_Ref;
        collectedResourceSpawner = _manager.CollectedResourceSpawner_Ref;
    }
    private void OnTriggerEnter(Collider other)
    {
        _playerLeft = false;
        _player.SwitchTool(this);
    }

    private void OnTriggerStay(Collider other)
    {
        _playerLeft = false;
        GatherResource();
    }
    private void OnTriggerExit(Collider other)
    {
        _playerLeft = true;
        _player.LeaveGathering();
    }

    public void TakeHit(int damage)
    {
        _player.GatherResource(this);
        Health -= damage;
        _inv.IncreaseItem(output_item,damage);
        CheckDestroy();
        collectedResourceSpawner.SpawnResourceImage(output_item, transform.position);
        transform.DOShakeScale(0.2f, 0.2f);
    }
    public void CheckDestroy()
    {
        if (Health <= 0)
            DestroyMe();
    }
    public void DestroyMe()
    {
        _player.LeaveGathering();
        _spawnPoint.owner.ClearResource(_spawnPoint);
    }
}
