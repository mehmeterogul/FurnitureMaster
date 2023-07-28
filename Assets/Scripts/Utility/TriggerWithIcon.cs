using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class TriggerWithIcon : Trigger
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _price;
}
