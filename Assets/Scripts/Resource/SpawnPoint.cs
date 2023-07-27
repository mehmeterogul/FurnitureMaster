using UnityEngine;
using UnityEngine.UIElements;

public class SpawnPoint
{
    public bool empty=true;
    public Vector3 pos;
    public GameObject spawnedObject;
    public ResourceSpawner owner;

    public SpawnPoint(Vector3 pos)
    {
        this.pos = pos;
    }
}
