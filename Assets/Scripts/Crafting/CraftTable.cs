using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float imageFillRate = 1f;
    [SerializeField] private float maxFillValue = 100f;
    [SerializeField] private float currentFillValue = 0f;
    private bool canDecrease = false;

    void Update()
    {
        if (canDecrease)
        {
            if (fillImage.fillAmount > 0)
            {
                currentFillValue -= (imageFillRate * 3);
                UpdateCircleSpriteFillAmounth();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canDecrease = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentFillValue += imageFillRate;
            UpdateCircleSpriteFillAmounth();

            if (currentFillValue == maxFillValue)
            {
                // CRAFT THE ORDER

                currentFillValue = 0;
                UpdateCircleSpriteFillAmounth();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canDecrease = true;
        }
    }

    public void UpdateCircleSpriteFillAmounth()
    {
        fillImage.fillAmount = currentFillValue / maxFillValue;
    }
}
