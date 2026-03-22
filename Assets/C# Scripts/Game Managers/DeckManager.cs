using System.Threading.Tasks;
using UnityEngine;



public class DeckManager : MonoBehaviour
{
    private int[] deckPile;
    private int[] discardPile;



    private async void Awake()
    {
        await LoadDeck_Async();
        deckPile.Shuffle();
    }


    /// <summary>
    /// Load saved deck into deckPile
    /// </summary>
    private async Task LoadDeck_Async()
    {
        (bool succes, ArrayWrapper<DeckEntry> data) = await FileManager.LoadInfoAsync<ArrayWrapper<DeckEntry>>(DeckBuilder.DECK_SAVE_FILE_PATH);

        if (succes == false || data.Value.IsNullOrEmpty())
        {
            return;
        }

        int entryCount = data.Value.Length;

        deckPile = new int[DeckBuilder.MAX_CARDS_PER_DECK];
        discardPile = new int[DeckBuilder.MAX_CARDS_PER_DECK];

        for (int i = 0; i < DeckBuilder.MAX_CARDS_PER_DECK;)
        {
            int unitTypeId = data[i].Id;
            int copyCount = data[i].Count;

            for (int j = 0; j < copyCount; i++)
            {
                deckPile[i] = unitTypeId;
            }
        }
    }
}