using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject block;
    public int maxResourceInRegion =3;
    public float SpawnPeriod = 8f;
    public float RandomMovementOffsetValue = 1f;
    public float RandomRotationOffsetValue = 180f;
    public int current_level = 1;
    public bool unlocked = false;

    private float spawnDistance = 3f;

    private List<ResourcePrefabSO> _resourcePrefabs;
    private List<Transform> _spawnTransforms;
    private List<SpawnPoint> _spawnPoints;
    private PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
        if (unlocked)
            UnlockArea();
        
    }

    public void UnlockArea()
    {
        Destroy(block);
        InvokeRepeating(nameof(GenerateRandomResource), SpawnPeriod, SpawnPeriod);
    }

    private void InitializeComponents()
    {
        _player = Game_Manager.Instance.Player_Ref;

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
    public void ClearResource(SpawnPoint point)
    {
        point.empty = true;
        Destroy(point.spawnedObject);
    }
    private bool CheckPlayer(Vector3 pointPos)
    {
        Vector3 playerPos = _player.transform.position;

        // Check if the player is inside the spawn point's collider
        float distanceToPlayer = Vector3.Distance(playerPos, pointPos);
        if (distanceToPlayer <= spawnDistance)
        {
            // If the player is inside the collider, don't spawn a resource here
            return true;
        }
        return false;
    }
    public void GenerateRandomResource()
    {
        if (_resourcePrefabs.Count == 0 || _spawnPoints.Count == 0)
        {
            Debug.LogWarning("No resource prefabs or spawn points found!");
            return;
        }

        // Don't spawn any resources if all spawn points are full
        if (CheckEmptySpace())
            return;

        List<ResourcePrefabSO> eligibleResources = _resourcePrefabs.FindAll(resource => resource.AvailableLevel <= current_level);

        // Choose a random resource prefab from the list
        ResourcePrefabSO randomResource = eligibleResources[Random.Range(0, eligibleResources.Count)];
        // Choose a random empty spawn point
        List<SpawnPoint> emptySpawnPoints = _spawnPoints.FindAll(spawnPoint => spawnPoint.empty);

        if (emptySpawnPoints.Count ==  maxResourceInRegion)
        {
            // Don't spawn any resources if there are no empty spawn points
            return;
        }

        SpawnPoint randomSpawnPoint = emptySpawnPoints[Random.Range(0, emptySpawnPoints.Count)];
        //if player inside area don't spawn
        if (CheckPlayer(randomSpawnPoint.pos))
        {
            return;
        }

        randomSpawnPoint.empty = false; // Mark the spawn point as non-empty

        //random offset
        Vector3 randomPositionOffset = new Vector3(Random.Range(-RandomMovementOffsetValue, RandomMovementOffsetValue),0, Random.Range(-RandomMovementOffsetValue, RandomMovementOffsetValue));
        Quaternion randomRotationOffset = Quaternion.Euler(0, Random.Range(0f,RandomRotationOffsetValue),0);

        // Apply the offsets to the spawn point position and rotation
        Vector3 spawnPosition = randomSpawnPoint.pos + randomPositionOffset;
        Quaternion spawnRotation = randomRotationOffset;


        // Instantiate the resource prefab at the chosen spawn point position
        GameObject spawnedPrefab = Instantiate(randomResource.Prefab, spawnPosition, spawnRotation);

        // Play scaling up animation
        Transform spawnedPrefabTransform = spawnedPrefab.transform;
        spawnedPrefabTransform.localScale = Vector3.one * 0.1f;
        spawnedPrefabTransform.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutSine);

        //Set Adjustments
        randomSpawnPoint.spawnedObject = spawnedPrefab;
        randomSpawnPoint.owner = this;
        Abstract_Resource abstractResource = spawnedPrefab.GetComponent<Abstract_Resource>();
        if (abstractResource != null)
        {
            abstractResource.SetSpawnPoint(randomSpawnPoint);
        }

    }

    private bool CheckEmptySpace()
    {
        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            if (spawnPoint.empty)
            {
                return false;
            }
        }
        return true;
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
