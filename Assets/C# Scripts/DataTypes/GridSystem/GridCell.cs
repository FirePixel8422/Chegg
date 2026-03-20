using UnityEngine;


[System.Serializable]
public struct GridCell
{
    public int GridId { get; private set; }
    public Vector3 WorldPos { get; private set; }


    public GridEntry GridEntry;
    public GridCellFloorAnimator GridFloorTrans;
    public bool Full => GridEntry != null;


    public GridCell(int gridId, Vector3 worldPos, GridCellFloorAnimator gridFloorAnimator)
    {
        GridId = gridId;
        WorldPos = worldPos;

        GridEntry = null;
        GridFloorTrans = gridFloorAnimator;
    }
}