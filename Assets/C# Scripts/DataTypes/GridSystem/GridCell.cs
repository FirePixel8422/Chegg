using UnityEngine;


/// <summary>
/// Tile data of a single cell on the grid. Contains Cell World Position, GridId, GridEntry (if any) and ref to <see cref="GridCellFloorAnimator"/> which is the actual gameObject at this cells position.
/// </summary>
[System.Serializable]
public struct GridCell
{
    public  int Id;
    public  Vector3 WorldPos;
    public  GridCellFloorAnimator AnimatorObject;

    public GridEntry Entry { get; private set; }

    public readonly bool IsValid => Id >= 0;
    public readonly bool HasEntry => Entry != null;


    public GridCell(int gridId, Vector3 worldPos, GridCellFloorAnimator gridFloorAnimator)
    {
        Id = gridId;
        WorldPos = worldPos;

        Entry = null;
        AnimatorObject = gridFloorAnimator;
    }
    /// <summary>
    /// Returns a new GridCell struct copy with GridId set to -1, marking the GridCell as Null to all systems
    /// </summary>
    public static readonly GridCell Null = new GridCell(-1, Vector3.zero, null);
}