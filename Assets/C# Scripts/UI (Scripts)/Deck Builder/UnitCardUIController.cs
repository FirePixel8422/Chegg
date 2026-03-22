using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UnitCardUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI cardCopiesText;

    private int unitTypeId;


    public void Init()
    {
        cardCopiesText.text = "0";
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public void UpdateCardUI(UnitInfo unitInfo, int unitTypeId)
    {
        titleText.text = unitInfo.Name;
        titleText.color = unitInfo.Color;

        descriptionText.text = unitInfo.Description;
        icon.sprite = unitInfo.Icon;

        this.unitTypeId = unitTypeId;
    }

    public void UpdateCardCount(int addedCount)
    {
        if (DeckManager.IsFull && addedCount > 0) return;

        DeckManager.UpdateCard(unitTypeId, addedCount);
    }
    public void UpdateCardCopiesText(int newCount)
    {
        cardCopiesText.text = newCount.ToString();
    }
}
