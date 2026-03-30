using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;



public class CardDraggerManager : UpdateMonoBehaviour
{
    public static CardDraggerManager Instance { get; private set; }


    [SerializeField] private CardDragSettingsSO cardDragSettingsSO;
    private CardDragSettings cardDragSettings;

    private Canvas canvas;
    private RectTransform targetTransform;
    private Vector3 lastTargetPos;


    // Set pos.y to 50 to fold cards when a card is seelcted, as=lso disable this script.


    private void Awake()
    {
        Instance = this;
        canvas = GetComponentInParent<Canvas>(true);
        cardDragSettings = cardDragSettingsSO.Value;
    }

    public void SetTargetCardTransform(RectTransform card)
    {
        targetTransform = card;
    }
    public void ClearTargetCardTransform()
    {
        targetTransform = null;
    }

    protected override void OnUpdate()
    {
        if (targetTransform == null) return;

        Vector2 mousePos = Mouse.current.position.value;

        RectTransform parentRect = transform as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, mousePos, canvas.worldCamera, out Vector2 localPoint);

        targetTransform.anchoredPosition = Vector2.Lerp(targetTransform.anchoredPosition, localPoint, cardDragSettings.CardDragLerp * Time.deltaTime);

        UpdateCardSway();
    }

    private void UpdateCardSway()
    {
        Vector3 currentPos = targetTransform.localPosition;

        // Calculate how much we moved horizontally this frame
        float deltaX = currentPos.x - lastTargetPos.x;
        float yMultiplier = 1 + math.distance(currentPos.y, lastTargetPos.y);

        // Calculate target sway based on movement
        float targetSway = math.clamp(deltaX * yMultiplier * cardDragSettings.CardSwayPower, -cardDragSettings.CardMaxSwayAngle, cardDragSettings.CardMaxSwayAngle);

        // Smoothly lerp current rotation toward target
        Vector3 euler = targetTransform.localEulerAngles;
        euler.z = Mathf.LerpAngle(euler.z, targetSway, cardDragSettings.CardSwaySnappiness * Time.deltaTime);

        // Apply rotation
        targetTransform.localRotation = Quaternion.Euler(euler);

        // Store for next frame
        lastTargetPos = currentPos;
    }
}