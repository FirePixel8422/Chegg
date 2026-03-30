using TMPro;
using UnityEngine;


public class DeckBuilderUnitCardUI : UnitCardUI
{
    [SerializeField] private TextMeshProUGUI cardCopiesText;


    public void Init()
    {
        cardCopiesText.text = "0";
    }

    public void UpdateCardCount(int addedCount)
    {
        if (DeckBuilder.IsFull && addedCount > 0) return;

        DeckBuilder.UpdateCard(UnitTypeId, addedCount);
    }
    public void UpdateCardCopiesText(int newCount)
    {
        cardCopiesText.text = newCount.ToString();
    }
}
