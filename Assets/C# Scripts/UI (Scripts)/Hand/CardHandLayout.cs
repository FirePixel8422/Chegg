using System.Collections.Generic;
using UnityEngine;

public class CardHandLayout : UpdateMonoBehaviour
{
    public static CardHandLayout Instance { get; private set; }

    private readonly List<RectTransform> cards = new();


    [SerializeField] private RectTransform handCardPrefab;

    [Header("Layout")]
    [SerializeField] private float maxHandWidth = 1200f;
    [SerializeField] private float baseSpacing = 220f;
    [SerializeField] private float arcHeight = 140f;
    [SerializeField] private float maxRotation = 18f;

    [Header("Hover")]
    [SerializeField] private float hoverLift = 90f;
    [SerializeField] private float hoverSpacingPush = 140f;
    [SerializeField] private float hoverScale = 1.15f;

    [Header("Animation")]
    [SerializeField] private float moveSpeed = 14f;
    [SerializeField] private float rotateSpeed = 16f;
    [SerializeField] private float scaleSpeed = 16f;

    private int hoveredIndex = -1;
    private RectTransform hoveredCard;


    private void Awake()
    {
        Instance = this;
        RebuildCardList();
    }

    public void AddCard()
    {
        Instantiate(handCardPrefab, transform);
        RebuildCardList();
    }
    public void RemoveCard()
    {
        
    }
    private  void RebuildCardList()
    {
        cards.Clear();

        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            RectTransform card = transform.GetChild(i) as RectTransform;
            cards.Add(card);
        }
    }

    public void SetHovered(RectTransform card)
    {
        hoveredCard = card;
        hoveredIndex = cards.IndexOf(card);
    }
    public void ClearHovered()
    {
        hoveredCard = null;
        hoveredIndex = -1;
    }

    protected override void OnUpdate()
    {
        if (cards.Count == 0) return;

        UpdateLayout();
    }
    private void UpdateLayout()
    {
        int count = cards.Count;

        float width = Mathf.Min(maxHandWidth, baseSpacing * (count - 1));
        float spacing = width / Mathf.Max(1, count - 1);
        float center = (count - 1) * 0.5f;

        float handSizeFactor = Mathf.Clamp01((count - 2) / 6f);
        float dynamicArc = arcHeight * handSizeFactor;
        float dynamicRotation = maxRotation * handSizeFactor;

        for (int i = 0; i < count; i++)
        {
            RectTransform card = cards[i];

            float offsetFromCenter = i - center;
            float t = count == 1 ? 0f : offsetFromCenter / center;

            float x = offsetFromCenter * spacing;

            // smoother arc
            float y = -(t * t) * dynamicArc;

            float rot = -t * dynamicRotation;

            if (hoveredIndex != -1)
            {
                int signedDistance = i - hoveredIndex;

                if (signedDistance != 0)
                {
                    float normalizedDistance = Mathf.Abs(signedDistance) / (float)(count - 1);

                    float pushStrength = 1f - normalizedDistance;
                    pushStrength = Mathf.Clamp01(pushStrength);
                    pushStrength = Mathf.Pow(pushStrength, 1.6f);

                    x += Mathf.Sign(signedDistance) * hoverSpacingPush * pushStrength;
                }
            }

            if (card == hoveredCard)
            {
                float liftBoost = Mathf.Lerp(1f, 1.5f, count / 10f);
                y += hoverLift * liftBoost;
            }

            Vector2 targetPos = new Vector2(x, y);
            Quaternion targetRot = Quaternion.Euler(0f, 0f, rot);
            Vector3 targetScale = card == hoveredCard ? Vector3.one * hoverScale : Vector3.one;

            card.anchoredPosition = Vector2.Lerp(card.anchoredPosition, targetPos, Time.deltaTime * moveSpeed);
            card.localRotation = Quaternion.Lerp(card.localRotation, targetRot, Time.deltaTime * rotateSpeed);
            card.localScale = Vector3.Lerp(card.localScale, targetScale, Time.deltaTime * scaleSpeed);
        }
    }
}