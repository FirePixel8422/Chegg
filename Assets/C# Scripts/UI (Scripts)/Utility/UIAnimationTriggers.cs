using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Animator))]
public class UIAnimatorTriggers : MonoBehaviour, 
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    private Animator animator;

    private static readonly int NormalHash = Animator.StringToHash("Normal");
    private static readonly int HighlightedHash = Animator.StringToHash("Highlighted");
    private static readonly int PressedHash = Animator.StringToHash("Pressed");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger(HighlightedHash);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger(NormalHash);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger(PressedHash);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        animator.SetTrigger(HighlightedHash);
    }
}