using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private float _imageFillRate = 1f;
    [SerializeField] private float _maxFillValue = 100f;
    [SerializeField] private float _currentFillValue = 0f;
    protected bool _canDecrease = false;
    protected bool _canTrigger = false;
    public UnityEvent OnFillComplete;

    [SerializeField] private AudioClip _clip;
   
    void Update()
    {
        if (_canDecrease)
        {
            if (_fillImage.fillAmount > 0)
            {
                _currentFillValue -= (_imageFillRate * 3);
                UpdateCircleSpriteFillAmounth();
            }
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        _canDecrease = false;
        _canTrigger = true;
        EnterCraftingAnim();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_canTrigger)
            return;

        _currentFillValue += _imageFillRate;
        UpdateCircleSpriteFillAmounth();

        if (_currentFillValue >= _maxFillValue)
        {
            _currentFillValue = 0;
            UpdateCircleSpriteFillAmounth();

            OnFillComplete?.Invoke();
            OnComplete();
            AudioManager.Instance.PlaySound(_clip);
        }
    }

    public virtual void OnComplete()
    {

    }

    protected void EnterCraftingAnim()
    {
        Game_Manager.Instance.Player_Ref.CraftStart();
    }
    protected void ExitCraftingAnim()
    {
        Game_Manager.Instance.Player_Ref.CraftExit();
    }

    public virtual void OnTriggerExit(Collider other)
    {
        _canDecrease = true;
        _canTrigger = true;
        ExitCraftingAnim();
    }

    public void UpdateCircleSpriteFillAmounth()
    {
        _fillImage.fillAmount = _currentFillValue / _maxFillValue;
    }
}
