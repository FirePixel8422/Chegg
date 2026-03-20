using UnityEngine;



[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableObjects/Unit", order = -10010)]
public class UnitTypeSO : ScriptableObject
{
    [SerializeReference] public UnitTypeBase Value;
}