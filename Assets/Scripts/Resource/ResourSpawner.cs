using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourSpawner : MonoBehaviour
{
    public List<GameObject> ResourcePrefabs;
    private List<Transform> _spawnTransforms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateRandomResource()
    {

    }
    private List<Transform> GetDirectChildTransforms()
    {
        _spawnTransforms.Clear(); // Clear the list before populating it

        // Add all direct child transforms to the list
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            _spawnTransforms.Add(childTransform);
        }

        return _spawnTransforms;
    }
}
