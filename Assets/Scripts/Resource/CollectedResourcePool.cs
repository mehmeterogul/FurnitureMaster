using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedResourcePool : MonoBehaviour
{
    [SerializeField] private List<Transform> _woodList;
    [SerializeField] private List<Transform> _ironOreList;
    [SerializeField] private List<Transform> _goldOreList;

    private int _nextWoodIndex;
    private int _nextIronOreIndex;
    private int _nextGoldOreIndex;

    public Transform GetNextWood()
    {
        if (_nextWoodIndex >= _woodList.Count)
            _nextWoodIndex = 0;

        int currentCustomerIndex = _nextWoodIndex;
        _nextWoodIndex++;

        return _woodList[currentCustomerIndex];
    }

    public Transform GetNextIronOre()
    {
        if (_nextIronOreIndex >= _ironOreList.Count)
            _nextIronOreIndex = 0;

        int currentCustomerIndex = _nextIronOreIndex;
        _nextIronOreIndex++;

        return _ironOreList[currentCustomerIndex];
    }

    public Transform GetNextGoldOre()
    {
        if (_nextGoldOreIndex >= _goldOreList.Count)
            _nextGoldOreIndex = 0;

        int currentCustomerIndex = _nextGoldOreIndex;
        _nextGoldOreIndex++;

        return _goldOreList[currentCustomerIndex];
    }
}
