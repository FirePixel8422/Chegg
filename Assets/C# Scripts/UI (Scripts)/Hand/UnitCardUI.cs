using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UnitCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image icon;

    public int UnitTypeId { get; private set; }



    public void UpdateCardUI(UnitInfo unitInfo, int unitTypeId)
    {
        titleText.text = unitInfo.Name;
        titleText.color = unitInfo.Color;

        descriptionText.text = unitInfo.Description;
        icon.sprite = unitInfo.Icon;

        UnitTypeId = unitTypeId;
    }
}
