using UnityEngine;



public class GridCellFloorAnimator : UpdateMonoBehaviour
{
    [SerializeField] private float animateHeight = 1f;
    [SerializeField] private float animateSpeed = 2f;

    public bool IsSelected;
    private bool hasMouseOver;


    private void OnMouseExit()
    {
        hasMouseOver = false;
    }
    private void OnMouseEnter()
    {
        hasMouseOver = true;
    }

    protected override void OnUpdate()
    {
        Vector3 current = transform.position;

        Vector3 target = current;
        target.y = (IsSelected || hasMouseOver) ? animateHeight : 0;

        transform.position = Vector3.MoveTowards(current, target, animateSpeed * Time.deltaTime);
    }
}