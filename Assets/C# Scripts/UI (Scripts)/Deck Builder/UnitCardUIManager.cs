using UnityEngine;


public class UnitCardUIManager : MonoBehaviour
{
    public static UnitCardUIManager Instance { get; private set; }


    [SerializeField] private Transform cardListHolder;
    [SerializeField] private Transform deckListHolder;

    [SerializeField] private UnitCardUIController deckListEntryPrefab;

    private UnitCardUIController[] cardListUI, deckListUI;
    private int totalCardCount;


    private void Awake()
    {
        Instance = this;

        totalCardCount = UnitTypeManager.UnitTypes.Length;

        cardListUI = new UnitCardUIController[totalCardCount];
        deckListUI = new UnitCardUIController[totalCardCount];

        // Setup Card UI and data
        for (int i = 0; i < totalCardCount; i++)
        {
            UnitInfo info = UnitTypeManager.UnitTypes[i].Info;
            int unitTypeId = UnitTypeManager.UnitTypes[i].Id;

            UnitCardUIController listEntry = Instantiate(deckListEntryPrefab, cardListHolder);
            cardListUI[i] = listEntry;

            UnitCardUIController deckEntry = Instantiate(deckListEntryPrefab, deckListHolder);
            deckListUI[i] = deckEntry;

            listEntry.Init();
            listEntry.UpdateCardUI(info, unitTypeId);

            deckEntry.Init();
            deckEntry.UpdateCardUI(info, unitTypeId);
            deckEntry.SetActive(false);
        }

        _ = DeckBuilder.LoadDeck_Async();
    }

    public void UpdateTargetUnitTypeUI(int unitTypeId, int newCount)
    {
        cardListUI[unitTypeId].UpdateCardCopiesText(newCount);
        deckListUI[unitTypeId].UpdateCardCopiesText(newCount);
    }
    public void ToggleDeckUnitUI(int unitTypeId, bool state)
    {
        deckListUI[unitTypeId].SetActive(state);
    }
}