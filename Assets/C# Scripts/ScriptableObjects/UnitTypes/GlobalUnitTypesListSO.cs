using System;
using UnityEngine;



[CreateAssetMenu(fileName = "New Global Unit List", menuName = "ScriptableObjects/GlobalUnitTypesListSO", order = -1010)]
public class GlobalUnitTypesListSO : ScriptableObject
{
    public UnitTypeSO[] List;

    private void OnValidate()
    {
        if (List.IsNullOrEmpty()) return;

        Array.Sort(List, (a, b) => string.Compare(a.Value.Info.Name, b.Value.Info.Name, StringComparison.Ordinal));
    }
}