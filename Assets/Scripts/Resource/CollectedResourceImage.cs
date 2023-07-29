using System;
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
    private float _activeTime;

    private void Update()
    {
        if(_isActive)
        {
            _targetPos = _itemUI.transform.position;
            Vector3 newTargetPos = _collectedResourceSpawner.GetTargetWorldPosition(_targetPos);

            _activeTime += Time.deltaTime;

            float speed = 5f;
            if (Vector2.Distance(transform.position, newTargetPos) > 1f)
            {
                float totalAnimationTime = 0.7f;
                if (_activeTime >= totalAnimationTime)
                    speed = 10f;

                transform.position = Vector3.Lerp(transform.position, newTargetPos, Time.deltaTime * speed);
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
        _activeTime = 0f;
    }
}
