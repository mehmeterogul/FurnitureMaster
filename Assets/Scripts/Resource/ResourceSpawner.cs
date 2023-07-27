using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ResourceSpawner : MonoBehaviour
{
    public int maxResourceInRegion =3;
    public float SpawnPeriod = 8f;
    public float RandomMovementOffsetValue = 0.5f;
    public float RandomRotationOffsetValue = 180f;

    private List<ResourcePrefabSO> _resourcePrefabs;
    private List<Transform> _spawnTransforms;
    private List<SpawnPoint> _spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();

        InvokeRepeating(nameof(GenerateRandomResource), SpawnPeriod, SpawnPeriod);
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
        if (emptySpawnPoints.Count <=  maxResourceInRegion)
        {
            // Don't spawn any resources if there are no empty spawn points
            return;
        }

        SpawnPoint randomSpawnPoint = emptySpawnPoints[Random.Range(0, emptySpawnPoints.Count)];
        randomSpawnPoint.empty = false; // Mark the spawn point as non-empty

        //random offset
        Vector3 randomPositionOffset = new Vector3(Random.Range(-RandomMovementOffsetValue, RandomMovementOffsetValue),0, Random.Range(-RandomMovementOffsetValue, RandomMovementOffsetValue));
        Quaternion randomRotationOffset = Quaternion.Euler(0, Random.Range(0f,RandomRotationOffsetValue),0);

        // Apply the offsets to the spawn point position and rotation
        Vector3 spawnPosition = randomSpawnPoint.pos + randomPositionOffset;
        Quaternion spawnRotation = randomRotationOffset;


        // Instantiate the resource prefab at the chosen spawn point position
        Instantiate(randomResource.Prefab, randomSpawnPoint.pos,spawnRotation);
    }

    private List<Transform> GetDirectChildTransforms()
    {
        _spawnTransforms = new List<Transform>();

        // Add all direct child transforms to the list
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            _spawnTransforms.Add(childTransform);
        }

        return _spawnTransforms;
    }
}
