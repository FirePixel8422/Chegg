using UnityEngine;



/// <summary>
/// MB class responsible for initializing static data containers
/// </summary>
public class DataInitializer : MonoBehaviour
{
    [SerializeField] private GlobalUnitTypesListSO globalUnitListSO;



    private void Awake()
    {
        UnitTypeManager.Init(globalUnitListSO);
    }
}