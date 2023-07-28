using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequiredMaterialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _requiredMaterialAmount;
    [SerializeField] private Image _requiredMaterialIcon;

    public void SetIcon(Image icon)
    {
        _requiredMaterialIcon = icon;
    }

    public void SetText(string text)
    {
        _requiredMaterialAmount.text = text;
    }
}
