using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    private List<ResourcePrefabSO> _resourcePrefabs;
    private List<Transform> _spawnTransforms;
    // Start is called before the first frame update
    void Start()
    {
        _resourcePrefabs = Game_Manager.Instance.ResourceManager_Ref.ResourcePrefabs;

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
