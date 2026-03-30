using UnityEngine;



[CreateAssetMenu(fileName = "New ToolTips list", menuName = "ScriptableObjects/Misc/ToolTipsSO", order = -1003)]
public class ToolTipsSO : ScriptableObject
{
    public ToolTipWord[] Data;
}


[System.Serializable]
public struct ToolTipWord
{
    public string word;
    public Color wordColor;

    [TextArea]
    public string toolTip;

    [Header("Layout")]
    public float width;
    public float height;
}