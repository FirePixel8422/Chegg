using UnityEngine;



public class UnitCardUIManager : MonoBehaviour
{
    public static UnitCardUIManager Instance { get; private set; }

    private UnitCardUIHandler[] uiHandlers;
    private int currentScroll;


    private void Awake()
    {
        Instance = this;
        uiHandlers = GetComponentsInChildren<UnitCardUIHandler>(true);
    }


    public void UpdateUnitCardsUI(int scroll)
    {

    }
}