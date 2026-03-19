using UnityEngine;
using Fire_Pixel.Networking;
using Unity.Netcode;
using UnityEngine.UI;



public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }


    [SerializeField] private Image manaBar;

    [SerializeField] private int StartManaPerTurn = 1;
    [SerializeField] private int MaxManaPerTurn;

    public int CurrentMana { get; private set; }

    private void Awake()
    {
        Instance = this;
        TurnManager.TurnStarted += OnTurnStarted;
        TurnManager.TurnEnded += OnTurnEnded;

        manaBar.fillAmount = 0;
    }

    private void OnTurnStarted()
    {
        if (StartManaPerTurn != MaxManaPerTurn)
        {
            StartManaPerTurn += 1;
        }

        CurrentMana = StartManaPerTurn;
        manaBar.fillAmount = (float)CurrentMana / MaxManaPerTurn;
    }
    private void OnTurnEnded()
    {
        CurrentMana = 0;
        manaBar.fillAmount = 0;
    }

    public bool TryUseMana(int amount)
    {
        if (amount > CurrentMana) return false;

        CurrentMana -= amount;
        manaBar.fillAmount = (float)CurrentMana / MaxManaPerTurn;

        return true;
    }

    private void OnDestroy()
    {
        TurnManager.TurnStarted -= OnTurnStarted;
        TurnManager.TurnEnded -= OnTurnEnded;
    }
}