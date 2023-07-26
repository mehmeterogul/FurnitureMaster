using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Resource : MonoBehaviour
{
    public int Health = 5;
    public int Amount = 5;

    private PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
       

    }
}
