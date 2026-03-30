using System.Collections.Generic;
using UnityEngine;


public class CardHandLayout : UpdateMonoBehaviour
{
    public static CardHandLayout Instance { get; private set; }

    private List<(RectTransform RectTransform, int CardUnitId)> cards = new();


    [SerializeField] private CardLayoutSettingsSO cardLayoutSettingsSO;
    private CardLayoutSettings cardLayoutSettings;

    private int hoveredIndex = -1;
    private RectTransform hoveredCard;


    private void Awake()
    {
        Instance = this;
        cardLayoutSettings = cardLayoutSettingsSO.Value;
    }
     
    public void CreateNewCard(UnitInfo unitInfo, int unitTypeId)
    {
        RectTransform cardRect = Instantiate(cardLayoutSettings.HandCardPrefab, transform);
        UnitCardUI cardUI = cardRect.GetComponent<UnitCardUI>();

        cardUI.UpdateCardUI(unitInfo, unitTypeId);

        cards.Add((cardRect, cardUI.UnitTypeId));
        SortCards();
    }
    public void AddCard(RectTransform card, int unitTypeId)
    {
        cards.Add((card, unitTypeId));
        SortCards();
    }
    public void RemoveCard(RectTransform card, int unitTypeId)
    {
        cards.Remove((card, unitTypeId));
        SortCards();
    }
    private void SortCards()
    {
        cards.Sort((a, b) =>
        {
            int idA = a.CardUnitId;
            int idB = b.CardUnitId;
            return idA.CompareTo(idB);
        });

        int cardCount = cards.Count;
        for (int i = 0; i < cardCount; i++)
        {
            cards[i].RectTransform.SetSiblingIndex(i);
        }
    }

    public void SetHovered(RectTransform card, int unitTypeId)
    {
        hoveredCard = card;
        hoveredIndex = cards.IndexOf((card, unitTypeId));
    }
    public void ClearHovered()
    {
        if (hoveredIndex == -1) return;

        hoveredCard.SetSiblingIndex(hoveredIndex);

        hoveredCard = null;
        hoveredIndex = -1;
    }


    #region Card Selection Animation

    protected override void OnUpdate()
    {
        if (cards.Count == 0) return;

        UpdateLayout();
    }
    private void UpdateLayout()
    {
        int count = cards.Count;

        float width = Mathf.Min(cardLayoutSettings.MaxHandWidth, cardLayoutSettings.BaseSpacing * (count - 1));
        float spacing = width / Mathf.Max(1, count - 1);
        float center = (count - 1) * 0.5f;

        float handSizeFactor = Mathf.Clamp01((count - 2) / 6f);
        float dynamicArc = cardLayoutSettings.ArcHeight * handSizeFactor;
        float dynamicRotation = cardLayoutSettings.MaxRotation * handSizeFactor;

        for (int i = 0; i < count; i++)
        {
            RectTransform card = cards[i].RectTransform;

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

                    x += Mathf.Sign(signedDistance) * cardLayoutSettings.HoverSpacingPush * pushStrength;
                }
            }

            if (card == hoveredCard)
            {
                float liftBoost = Mathf.Lerp(1f, 1.5f, count / 10f);
                y += cardLayoutSettings.HoverLift * liftBoost;
            }

            Vector2 targetPos = new Vector2(x, y);
            Quaternion targetRot = Quaternion.Euler(0f, 0f, rot);
            Vector3 targetScale = card == hoveredCard ? Vector3.one * cardLayoutSettings.HoverScale : Vector3.one;

            card.anchoredPosition = Vector2.Lerp(card.anchoredPosition, targetPos, Time.deltaTime * cardLayoutSettings.MoveSpeed);
            card.localRotation = Quaternion.Lerp(card.localRotation, targetRot, Time.deltaTime * cardLayoutSettings.RotateSpeed);
            card.localScale = Vector3.Lerp(card.localScale, targetScale, Time.deltaTime * cardLayoutSettings.ScaleSpeed);
        }
    }

    #endregion




//#if UNITY_EDITOR
    [Header("Debug Spawn Card")]
    [SerializeField] private UnitTypeSO[] unitSO;


    [InspectorButton("Add Card")]
    public void DEBUG_AddCard() => CreateNewCard(unitSO[0].Value.Info, unitSO[0].Value.Id);
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnitTypeBase r = unitSO.SelectRandom().Value;
            CreateNewCard(r.Info, r.Id);
        }
    }
//#endif
}