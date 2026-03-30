using UnityEngine;



[CreateAssetMenu(fileName = "New Card Layout Settings", menuName = "ScriptableObjects/CardLayoutSettingsSO", order = -1003)]
public class CardLayoutSettingsSO : ScriptableObject
{
    public CardLayoutSettings Value;
}


[System.Serializable]
public struct CardLayoutSettings
{
    public RectTransform HandCardPrefab;

    [Header("Layout")]
    public float MaxHandWidth;
    public float BaseSpacing;
    public float ArcHeight;
    public float MaxRotation;

    [Header("Hover")]
    public float HoverLift;
    public float HoverSpacingPush;
    public float HoverScale;

    [Header("Animation")]
    public float MoveSpeed;
    public float RotateSpeed;
    public float ScaleSpeed;
}