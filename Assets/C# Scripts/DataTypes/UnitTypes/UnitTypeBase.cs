using Unity.Mathematics;
using UnityEngine;


/// <summary>
/// Base UnitType class used as a template for in complexity varying UnitTypes, containing all core UnitTypeData like name, movement and manacost
/// </summary>
[System.Serializable]
public abstract class UnitTypeBase
{
    public int Id {  get; private set; }
    public void SetId(int id) => Id = id;


    public UnitInfo Info;
    public MoveDirection Movement;

    [Range(0, 6)]
    public int ManaCost;
}


[System.Serializable]
public struct UnitInfo
{
    public string Name;
    [TextArea]
    public string Description;
    public Sprite Icon;
}

[System.Serializable]
public struct MoveDirection
{
    public int2 Direction;
    public int MaxSteps;
}