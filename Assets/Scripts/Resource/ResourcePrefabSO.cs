using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ResourcePrefabSO")]
public class ResourcePrefabSO : ScriptableObject
{
    public GameObject Prefab;
    public int AvailableLevel;
}
