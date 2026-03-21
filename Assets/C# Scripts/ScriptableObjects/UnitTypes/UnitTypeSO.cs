using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableObjects/Unit", order = -10010)]
public class UnitTypeSO : ScriptableObject
{
    [SerializeReference] public UnitTypeBase Value;


#if UNITY_EDITOR
    private void OnValidate()
    {
        Value.Info.Name = name;

        Value.BakeMovementDirections();
        Value.BakeAttackDirections();
    }
#endif
}