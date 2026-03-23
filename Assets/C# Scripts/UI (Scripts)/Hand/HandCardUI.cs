using UnityEngine;
using UnityEngine.EventSystems;


public class HandCardUI : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    private RectTransform rect;

    private void Awake()
    {
        rect = transform as RectTransform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CardHandLayout.Instance.SetHovered(rect);
        //rect.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardHandLayout.Instance.ClearHovered();
    }
}