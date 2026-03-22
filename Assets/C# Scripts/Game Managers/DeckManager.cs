using System.Collections.Generic;
using System.Threading.Tasks;


public static class DeckManager
{
#pragma warning disable UDR0001
    private static Dictionary<int, DeckEntry> deckJson;

    public static bool IsFull => currentCardCount == MAX_CARDS_PER_DECK;
    private static int currentCardCount;

    private const string DECK_SAVE_FILE_PATH = "Decks/New Deck";
    private const int MAX_CARDS_PER_DECK = 15;
#pragma warning restore UDR0001


    public static void UpdateCard(int unitTypeId, int addedCount)
    {
        int newUnitCopyCount;

        if (deckJson.TryGetValue(unitTypeId, out DeckEntry entry))
        {
            int prevCount = entry.Count;
            newUnitCopyCount = prevCount + addedCount;

            entry.Count = newUnitCopyCount;

            // Add changed card count to currentCardCount
            currentCardCount += newUnitCopyCount - prevCount;

            if (newUnitCopyCount == 0)
            {
                deckJson.Remove(unitTypeId);
                UnitCardUIManager.Instance.ToggleDeckUnitUI(unitTypeId, false);
            }
            else
            {
                deckJson[unitTypeId] = entry;
            }
        }
        else
        {
            if (addedCount < 0) return;

            newUnitCopyCount = addedCount;

            deckJson.Add(unitTypeId, new DeckEntry(unitTypeId, addedCount));
            currentCardCount += addedCount;

            UnitCardUIManager.Instance.ToggleDeckUnitUI(unitTypeId, true);
        }

        UnitCardUIManager.Instance.UpdateTargetUnitTypeUI(unitTypeId, newUnitCopyCount);
        _ = SaveDeck_Async();
    }

    
    /// <summary>
    /// Load potential saved deck savefile into data.
    /// </summary>
    public static async Task LoadDeck_Async()
    {
        (bool succes, ArrayWrapper<DeckEntry> data) = await FileManager.LoadInfoAsync<ArrayWrapper<DeckEntry>>(DECK_SAVE_FILE_PATH);

        if (succes == false || data.Value.IsNullOrEmpty())
        {
            deckJson = new Dictionary<int, DeckEntry>();
            return;
        }

        int totalEntries = data.Value.Length;
        deckJson = new Dictionary<int, DeckEntry>(totalEntries);
        currentCardCount = 0;

        for (int i = 0; i < totalEntries; i++)
        {
            int unitTypeId = data[i].Id;

            deckJson.Add(unitTypeId, data[i]);
            currentCardCount += data[i].Count;

            UnitCardUIManager.Instance.UpdateTargetUnitTypeUI(unitTypeId, data[i].Count);
            UnitCardUIManager.Instance.ToggleDeckUnitUI(unitTypeId, true);
        }
    }
    /// <summary>
    /// Save deckdata to savefile
    /// </summary>
    private static async Task SaveDeck_Async()
    {
        int totalEntries = deckJson.Count;
        if (totalEntries == 0) return;

        ArrayWrapper<DeckEntry> deckEntries = new ArrayWrapper<DeckEntry>(totalEntries);

        int i = 0;
        foreach (var kvp in deckJson)
        {
            deckEntries[i] = kvp.Value;
            i += 1;
        }

        await FileManager.SaveInfoAsync(deckEntries, DECK_SAVE_FILE_PATH);
    }
}

[System.Serializable]
public struct DeckEntry
{
    public int Id;
    public int Count;

    public DeckEntry(int id, int count)
    {
        Id = id;
        Count = count;
    }
}
