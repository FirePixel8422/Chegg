using System;
using UnityEngine;



[CreateAssetMenu(fileName = "New Global Unit List", menuName = "ScriptableObjects/GlobalUnitTypesListSO", order = -1010)]
public class GlobalUnitTypesListSO : ScriptableObject
{
    public UnitTypeSO[] Value;

    private void OnValidate()
    {
        if (Value.IsNullOrEmpty()) return;

        Array.Sort(Value, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
    }
}