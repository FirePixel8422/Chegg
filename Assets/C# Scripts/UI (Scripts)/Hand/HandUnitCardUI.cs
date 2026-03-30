using UnityEngine;
using UnityEngine.EventSystems;


public class HandUnitCardUI : UnitCardUI
{
    public RectTransform RectTransform;

    private bool isBeingDragged;
    private bool isHovered;


    public void OnPointerEnter(BaseEventData _)
    {
        isHovered = true;

        if (isBeingDragged) return;

        CardHandLayout.Instance.SetHovered(RectTransform, UnitTypeId);
        RectTransform.SetAsLastSibling();
    }
    public void OnPointerExit(BaseEventData _)
    {
        isHovered = false;

        if (isBeingDragged) return;

        CardHandLayout.Instance.ClearHovered();
    }

    public void OnPointerDown(BaseEventData _)
    {
        isBeingDragged = true;

        CardHandLayout.Instance.RemoveCard(RectTransform, UnitTypeId);
        CardHandLayout.Instance.ClearHovered();
        RectTransform.localScale = Vector3.one;

        CardDraggerManager.Instance.SetTargetCardTransform(RectTransform);
    }
    public void OnPointerUp(BaseEventData _)
    {
        isBeingDragged = false;

        CardHandLayout.Instance.AddCard(RectTransform, UnitTypeId);
        CardDraggerManager.Instance.ClearTargetCardTransform();

        if (isHovered)
        {
            CardHandLayout.Instance.SetHovered(RectTransform, UnitTypeId);
            RectTransform.SetAsLastSibling();
        }
    }
}