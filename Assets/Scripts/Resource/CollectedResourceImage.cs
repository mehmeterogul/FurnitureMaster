using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CollectedResourceImage : MonoBehaviour
{
    [SerializeField] private CollectedResourceSpawner _collectedResourceSpawner;
    private Vector3 _targetPos;
    private bool _isActive;
    private ItemUI _itemUI;

    private void Update()
    {
        if(_isActive)
        {
            _targetPos = _itemUI.transform.position;
            Vector3 newTargetPos = _collectedResourceSpawner.GetTargetWorldPosition(_targetPos);

            if (Vector2.Distance(transform.position, newTargetPos) > 1f)
            {
                transform.position = Vector3.Lerp(transform.position, newTargetPos, Time.deltaTime * 5f);
            }
            else
            {
                _isActive = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void SetStartAndTargetPosition(Vector3 spawnPosition, ItemUI itemUI)
    {
        _isActive = true;
        _itemUI = itemUI;
        transform.position = spawnPosition;
    }
}
