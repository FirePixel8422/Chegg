#if UNITY_EDITOR
using UnityEngine;

public class UnitTypeTester : MonoBehaviour
{
    [SerializeField] private UnitTypeSO unitSO;

    [Header("Loaded unit from target unitSO")]
    [SerializeReference] private UnitTypeBase loadedUnit;


    private void OnDrawGizmos()
    {
        if (unitSO == null) return;

        loadedUnit = unitSO.Value;
        loadedUnit.BakeMovementDirections();
        loadedUnit.BakeAttackDirections();

        Gizmos.color = Color.white;
        Gizmos.DrawCube(Vector3.zero, Vector3.one * 0.75f);

        Gizmos.color = new Color(0, 0, 1, 1);
        for (int i = 0; i < unitSO.Value.MovementDirections.Length; i++)
        {
            Vector3 worldPos = new Vector3(unitSO.Value.MovementDirections[i].x, 0, unitSO.Value.MovementDirections[i].y);

            Gizmos.DrawCube(worldPos, Vector3.one * 0.75f);
        }

        Gizmos.color = new Color(1, 0, 0, 1);
        for (int i = 0; i < unitSO.Value.AttackDirections.Length; i++)
        {
            Vector3 worldPos = new Vector3(unitSO.Value.AttackDirections[i].x, 0, unitSO.Value.AttackDirections[i].y);

            Gizmos.DrawCube(worldPos, Vector3.one * 0.6f);
        }
    }
}
#endif