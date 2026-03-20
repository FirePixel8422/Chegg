using UnityEngine;
using Fire_Pixel.Networking;
using UnityEngine.UI;
using Unity.Mathematics;
using System.Diagnostics;


public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }


    [SerializeField] private Image manaBar;
    [SerializeField] private NativeSampledAnimationCurve manaGainCurve = NativeSampledAnimationCurve.Default;

    public int CurrentMana { get; private set; }

    private int cRoundNumber;
    private const int MAX_MANA = 10;


    private void Awake()
    {
        Instance = this;
        TurnManager.TurnStarted += OnTurnStarted;
        TurnManager.TurnEnded += OnTurnEnded;

        manaGainCurve.Bake();

        manaBar.fillAmount = 0;
    }

    private void OnTurnStarted()
    {
        cRoundNumber += 1;

        CurrentMana = Mathf.CeilToInt(manaGainCurve.Evaluate(cRoundNumber));

        manaBar.fillAmount = (float)CurrentMana / MAX_MANA;
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
        manaBar.fillAmount = (float)CurrentMana / MAX_MANA;

        return true;
    }

    private void OnDestroy()
    {
        TurnManager.TurnStarted -= OnTurnStarted;
        TurnManager.TurnEnded -= OnTurnEnded;

        manaGainCurve.Dispose();
    }
}