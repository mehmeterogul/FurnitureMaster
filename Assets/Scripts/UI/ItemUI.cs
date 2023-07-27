using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI: MonoBehaviour
{
    public Image IconImage;
    public TextMeshProUGUI AmountText;
    public Item Properties;
    public int amount;

    private Color _darkGray;
    private bool _isDormant;

    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
        amount = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitializeComponents()
    {
        _isDormant = true;
        _darkGray = new Color32(65,65,65,255);
        IconImage.sprite = Properties.Icon;
        IconImage.color = _darkGray;
        AmountText.SetText(string.Empty);
    }
    public void Increase(int income)
    {
        if (_isDormant)
        {
            IconImage.color = Color.white;
            _isDormant = false;
        }
        amount+= income;
        AmountText.SetText(amount.ToString());
        
    }
    public void Decrease(int income)
    {
        
        amount -= income;
        AmountText.SetText(amount.ToString());
        
    }
}
