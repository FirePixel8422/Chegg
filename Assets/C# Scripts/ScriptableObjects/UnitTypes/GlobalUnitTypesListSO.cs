using UnityEngine;



[CreateAssetMenu(fileName = "New Global Unit List", menuName = "ScriptableObjects/GlobalUnitTypesListSO", order = -1010)]
public class GlobalUnitTypesListSO : ScriptableObject
{
    public UnitTypeSO[] Value;
}