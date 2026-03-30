using UnityEngine;



[CreateAssetMenu(fileName = "Card Drag Settings", menuName = "ScriptableObjects/CardDragSettingsSO", order = -1003)]
public class CardDragSettingsSO : ScriptableObject
{
    public CardDragSettings Value;
}


[System.Serializable]
public struct CardDragSettings
{
    public float CardDragLerp;
    public float CardMaxSwayAngle;
    public float CardSwayPower;
    public float CardSwaySnappiness;
}