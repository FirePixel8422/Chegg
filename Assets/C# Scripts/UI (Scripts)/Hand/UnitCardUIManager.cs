using UnityEngine;


public class UnitCardUIManager : MonoBehaviour
{
    public static UnitCardUIManager Instance { get; private set; }


    [SerializeField] private Transform cardListHolder;
    [SerializeField] private Transform deckListHolder;

    [SerializeField] private DeckUnitCardUI deckListEntryPrefab;

    private DeckUnitCardUI[] cardListUI, deckListUI;
    private int totalCardCount;


    private void Awake()
    {
        Instance = this;

        totalCardCount = UnitTypeManager.UnitTypes.Length;

        cardListUI = new DeckUnitCardUI[totalCardCount];
        deckListUI = new DeckUnitCardUI[totalCardCount];

        // Setup Card UI and data
        for (int i = 0; i < totalCardCount; i++)
        {
            UnitInfo info = UnitTypeManager.UnitTypes[i].Info;
            int unitTypeId = UnitTypeManager.UnitTypes[i].Id;

            DeckUnitCardUI listEntry = Instantiate(deckListEntryPrefab, cardListHolder);
            cardListUI[i] = listEntry;

            DeckUnitCardUI deckEntry = Instantiate(deckListEntryPrefab, deckListHolder);
            deckListUI[i] = deckEntry;

            listEntry.Init();
            listEntry.UpdateCardUI(info, unitTypeId);

            deckEntry.Init();
            deckEntry.UpdateCardUI(info, unitTypeId);
            deckEntry.gameObject.SetActive(false);
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
        deckListUI[unitTypeId].gameObject.SetActive(state);
    }
}