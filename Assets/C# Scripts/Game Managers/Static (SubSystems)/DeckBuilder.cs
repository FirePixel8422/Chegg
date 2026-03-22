using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// Static class responsible for loading and tracking a savefile for you deck. Also responsible for updating card UI upon being sent card updates.
/// </summary>
public static class DeckBuilder
{
    private static Dictionary<int, DeckEntry> deckEntryDict;

    public static bool IsFull => currentCardCount == MAX_CARDS_PER_DECK;
    private static int currentCardCount;

    public const string DECK_SAVE_FILE_PATH = "Decks/New Deck.fpx";
    public const int MAX_CARDS_PER_DECK = 15;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        deckEntryDict = new Dictionary<int, DeckEntry>();
        currentCardCount = 0;

        _ = LoadDeck_Async();
    }

    public static void UpdateCard(int unitTypeId, int addedCount)
    {
        int newUnitCopyCount;

        if (deckEntryDict.TryGetValue(unitTypeId, out DeckEntry entry))
        {
            int prevCount = entry.Count;
            newUnitCopyCount = prevCount + addedCount;

            entry.Count = newUnitCopyCount;

            // Add changed card count to currentCardCount
            currentCardCount += newUnitCopyCount - prevCount;

            if (newUnitCopyCount == 0)
            {
                deckEntryDict.Remove(unitTypeId);
                UnitCardUIManager.Instance.ToggleDeckUnitUI(unitTypeId, false);
            }
            else
            {
                deckEntryDict[unitTypeId] = entry;
            }
        }
        else
        {
            if (addedCount < 0) return;

            newUnitCopyCount = addedCount;

            deckEntryDict.Add(unitTypeId, new DeckEntry(unitTypeId, addedCount));
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
            return;
        }

        int totalEntries = data.Value.Length;
        deckEntryDict.Clear();
        deckEntryDict.EnsureCapacity(totalEntries);
        currentCardCount = 0;

        for (int i = 0; i < totalEntries; i++)
        {
            int unitTypeId = data[i].Id;

            deckEntryDict.Add(unitTypeId, data[i]);
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
        int totalEntries = deckEntryDict.Count;
        if (totalEntries == 0) return;

        ArrayWrapper<DeckEntry> deckEntries = new ArrayWrapper<DeckEntry>(totalEntries);

        int i = 0;
        foreach (var kvp in deckEntryDict)
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
