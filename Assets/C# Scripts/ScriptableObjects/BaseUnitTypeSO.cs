using UnityEngine;



[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableObjects/Unit", order = -1003)]
public class BaseUnitTypeSO : ScriptableObject
{
    [SerializeReference] public UnitTypeBase Value;
}