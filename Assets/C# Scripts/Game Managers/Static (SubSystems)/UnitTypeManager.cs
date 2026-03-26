


/// <summary>
/// Static class initialized by <see cref="DataInitializer"/> that holds a list of all <see cref="UnitTypeBase"/>'s
/// </summary>
public static class UnitTypeManager
{
    public static UnitTypeBase[] UnitTypes { get; private set; }


    public static void Init(GlobalUnitTypesListSO unitListSO)
    {
        int unitCount = unitListSO.List.Length;
        UnitTypes = new UnitTypeBase[unitCount];

        for (int i = 0; i < unitCount; i++)
        {
            UnitTypes[i] = unitListSO.List[i].Value;
            UnitTypes[i].SetId(i);
        }
    }
}