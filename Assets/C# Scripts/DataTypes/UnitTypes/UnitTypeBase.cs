using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


/// <summary>
/// Base UnitType class used as a template for in complexity varying UnitTypes, containing all core UnitTypeData like name, movement and manacost
/// </summary>
[System.Serializable]
public abstract class UnitTypeBase
{
    public int Id { get; private set; }
    public void SetId(int id) => Id = id;


    public UnitInfo Info = new UnitInfo("New Unit", "Stupid bozo without description");

#if UNITY_EDITOR
    public DirectionPattern[] Movement;
    public DirectionPattern[] Attack;

    public void BakeMovementDirections() => BakeDirectionPatterns(Movement, out MovementDirections);
    public void BakeAttackDirections() => BakeDirectionPatterns(Attack, out AttackDirections);

    private void BakeDirectionPatterns(DirectionPattern[] input, out int2[] output)
    {
        List<int2> directions = new List<int2>();
        for (int i = 0; i < input.Length; i++)
        {
            DirectionPattern directionPattern = input[i];

            for (int j = 1; j <= directionPattern.MaxSteps; j++)
            {
                if (directionPattern.MirrorOnAllDirections == false)
                {
                    directions.Add(directionPattern.Direction * j);
                    continue;
                }

                int2 dir = directionPattern.Direction * j;
                directions.Add(dir);
                directions.Add(new int2(-dir.y, dir.x));
                directions.Add(new int2(-dir.x, -dir.y));
                directions.Add(new int2(dir.y, -dir.x));
            }
        }
        output = directions.ToArray();
    }
#endif
    public int2[] MovementDirections;
    public int2[] AttackDirections;

    [Range(0, 10)]
    public int ManaCost = 1;
}


[System.Serializable]
public struct UnitInfo
{
    public string Name;
    [TextArea]
    public string Description;

    public Sprite Icon;
    public Color Color;


    public UnitInfo(string name, string description)
    {
        Name = name; 
        Description = description;

        Icon = null;
        Color = Color.white;
    }
}

#if UNITY_EDITOR
[System.Serializable]
public struct DirectionPattern
{
    public int2 Direction;
    public int MaxSteps;
    public bool MirrorOnAllDirections;
}
#endif