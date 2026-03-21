using UnityEngine;


public class UnitCardUIManager : MonoBehaviour
{
    public static UnitCardUIManager Instance { get; private set; }


    private UnitCardUIHandler[] uiHandlers;
    private int currentPage;

    private int totalCardCount;
    private int cardPerPageCount;
    private int pageCount;


    private void Awake()
    {
        Instance = this;
        uiHandlers = GetComponentsInChildren<UnitCardUIHandler>(true);

        cardPerPageCount = uiHandlers.Length;
        for (int i = 0; i < cardPerPageCount; i++)
        {
            uiHandlers[i].Init();
        }

        totalCardCount = UnitTypeManager.UnitTypes.Length;
        pageCount = Mathf.CeilToInt((float)totalCardCount / cardPerPageCount);

        UpdateUnitCardsUI(0);
    }

    public void UpdateUnitCardsUI(int addedScroll)
    {
        currentPage = Mathf.Clamp(currentPage + addedScroll, 0, pageCount - 1);

        int unitStartIndex = currentPage * cardPerPageCount;
        int cPageCardCount = Mathf.Min(cardPerPageCount, totalCardCount - unitStartIndex);

        // Enable and update card slots that get used by current page.
        for (int i = 0; i < cardPerPageCount; i++)
        {
            bool active = i < cPageCardCount;
            uiHandlers[i].SetActiveState(active);

            if (active)
            {
                UnitInfo info = UnitTypeManager.UnitTypes[unitStartIndex + i].Info;
                uiHandlers[i].UpdateCardUI(info);
            }
        }
    }
}