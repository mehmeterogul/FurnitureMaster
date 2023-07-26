using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Resource : MonoBehaviour
{
    public int Health = 5;
    public int Amount = 5;

    public PlayerController _player;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        _player = manager.player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        GatherResource();

    }

    public abstract void GatherResource();

    public void TakeHit()
    {
        Health--;
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
