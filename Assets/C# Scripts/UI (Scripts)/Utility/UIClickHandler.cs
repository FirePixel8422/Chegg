using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


/// <summary>
/// Utility UI Pointer class that gives a callback for leftclick and rightclick
/// </summary>
public class UIClickHandler : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnLeftClick;
    public UnityEvent OnRightClick;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick?.Invoke();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick?.Invoke();
        }
    }
}