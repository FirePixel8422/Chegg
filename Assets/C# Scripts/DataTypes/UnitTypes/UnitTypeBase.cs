using Unity.Mathematics;
using UnityEngine;


[System.Serializable]
public abstract class UnitTypeBase
{
    public UnitInfo Info;
    public MoveDirection Movement;

    public int ManaCost;
}


[System.Serializable]
public struct UnitInfo
{
    [TextArea]
    public string Name;
    public string Description;
}

[System.Serializable]
public struct MoveDirection
{
    public int2 Direction;
    public int MaxSteps;
}