using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    //Singleton
    public static Inventory Instance { get; private set; }

    public List<ItemUI> ResourceItems;
    public List<ItemUI> MaterialItems;

    // Start is called before the first frame update
    void Start()
    {
      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
