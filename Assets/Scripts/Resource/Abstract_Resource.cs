using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Resource : MonoBehaviour
{
    public int Health = 10;
    public int Amount = 10;

    public PlayerController _player;
    public Game_Manager manager;
    public Coroutine gatherCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        manager = Game_Manager.Instance;
        _player = manager.Player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Abstracts Methods
    public abstract void GatherResource();

    //Common Methods
    
    private void OnTriggerEnter(Collider other)
    {
        _player.GatherResource(this);
    }

    
    private void OnTriggerStay(Collider other)
    {
        GatherResource();
    }
    
    private void OnCollisionExit(Collision collision)
    {
        _player.LeaveGathering();
    }

    public void TakeHit(int damage)
    {     
        Health -= damage;
        Debug.Log("hp:" + Health.ToString());
        CheckDestroy();
    }
    public void CheckDestroy()
    {
        if (Health <= 0)
            DestroyMe();
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
