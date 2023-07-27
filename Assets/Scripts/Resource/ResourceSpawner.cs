using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ResourceSpawner : MonoBehaviour
{
    public List<ResourcePrefabSO> _resourcePrefabs;
    public List<Transform> _spawnTransforms;
    public List<SpawnPoint> _spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();

        InvokeRepeating(nameof(GenerateRandomResource), 1f, 1f);
    }
    private void InitializeComponents()
    {
        _spawnTransforms = GetDirectChildTransforms();
        _resourcePrefabs = Game_Manager.Instance.ResourceManager_Ref.ResourcePrefabs;

        _spawnPoints = new List<SpawnPoint>();
        foreach (Transform spawnTransform in _spawnTransforms)
        {
            // Create a new SpawnPoint instance for each transform and add it to the list
            _spawnPoints.Add(new SpawnPoint(spawnTransform.position));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateRandomResource()
    {
        if (_resourcePrefabs.Count == 0 || _spawnPoints.Count == 0)
        {
            Debug.LogWarning("No resource prefabs or spawn points found!");
            return;
        }

        // Check if all spawn points are full
        bool allSpawnPointsFull = true;
        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            if (spawnPoint.empty)
            {
                allSpawnPointsFull = false;
                break;
            }
        }

        if (allSpawnPointsFull)
        {
            // Don't spawn any resources if all spawn points are full
            return;
        }

        // Choose a random resource prefab from the list
        ResourcePrefabSO randomResource = _resourcePrefabs[Random.Range(0, _resourcePrefabs.Count)];

        // Choose a random empty spawn point
        List<SpawnPoint> emptySpawnPoints = _spawnPoints.FindAll(spawnPoint => spawnPoint.empty);
        if (emptySpawnPoints.Count == 0)
        {
            // Don't spawn any resources if there are no empty spawn points
            return;
        }

        SpawnPoint randomSpawnPoint = emptySpawnPoints[Random.Range(0, emptySpawnPoints.Count)];
        randomSpawnPoint.empty = false; // Mark the spawn point as non-empty

        // Instantiate the resource prefab at the chosen spawn point position
        Instantiate(randomResource.Prefab, randomSpawnPoint.pos, Quaternion.identity);
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
