using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedResourcePool : MonoBehaviour
{
    [SerializeField] private List<Transform> _woodList;
    [SerializeField] private List<Transform> _ironOreList;
    [SerializeField] private List<Transform> _goldOreList;
    [SerializeField] private List<Transform> _plankList;
    [SerializeField] private List<Transform> _ironIngotList;
    [SerializeField] private List<Transform> _goldIngotList;

    private int _nextWoodIndex;
    private int _nextIronOreIndex;
    private int _nextGoldOreIndex;
    private int _nextPlankIndex;
    private int _nextIronIngotIndex;
    private int _nextGoldIngotIndex;

    public Transform GetNextWood()
    {
        if (_nextWoodIndex >= _woodList.Count)
            _nextWoodIndex = 0;

        int currentIndex = _nextWoodIndex;
        _nextWoodIndex++;

        return _woodList[currentIndex];
    }

    public Transform GetNextIronOre()
    {
        if (_nextIronOreIndex >= _ironOreList.Count)
            _nextIronOreIndex = 0;

        int currentIndex = _nextIronOreIndex;
        _nextIronOreIndex++;

        return _ironOreList[currentIndex];
    }

    public Transform GetNextGoldOre()
    {
        if (_nextGoldOreIndex >= _goldOreList.Count)
            _nextGoldOreIndex = 0;

        int currentIndex = _nextGoldOreIndex;
        _nextGoldOreIndex++;

        return _goldOreList[currentIndex];
    }

    public Transform GetNextPlank()
    {
        if (_nextPlankIndex >= _plankList.Count)
            _nextPlankIndex = 0;

        int currentIndex = _nextPlankIndex;
        _nextPlankIndex++;

        return _plankList[currentIndex];
    }

    public Transform GetNextIronIngot()
    {
        if (_nextIronIngotIndex >= _ironIngotList.Count)
            _nextIronIngotIndex = 0;

        int currentIndex = _nextIronIngotIndex;
        _nextIronIngotIndex++;

        return _ironIngotList[currentIndex];
    }

    public Transform GetNextGoldIngot()
    {
        if (_nextGoldIngotIndex >= _goldIngotList.Count)
            _nextGoldIngotIndex = 0;

        int currentIndex = _nextGoldIngotIndex;
        _nextGoldIngotIndex++;

        return _goldIngotList[currentIndex];
    }
}
