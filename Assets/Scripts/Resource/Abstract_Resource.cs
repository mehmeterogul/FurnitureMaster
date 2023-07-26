using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Resource : MonoBehaviour
{
    public int Health = 10;
    public int Amount = 10;

    protected PlayerController _player;
    protected Game_Manager manager;
    protected Coroutine gatherCoroutine;

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
        Debug.Log(_player.CutPower.ToString());
        _player.GatherResource(this);
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Ontrigger");
        GatherResource();
    }
    */
    private void OnCollisionExit(Collision collision)
    {
        _player.LeaveGathering();
    }

    public void TakeHit(int amount)
    {     
        Health -=amount;
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
