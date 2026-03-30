using UnityEngine;



public class GridCellFloorAnimator : UpdateMonoBehaviour
{
    [SerializeField] private float animateHeight = 1f;
    [SerializeField] private float animateSpeed = 2f;

    public bool IsSelected;
    public bool IsMouseOver;


    protected override void OnUpdate()
    {
        Vector3 current = transform.position;

        Vector3 target = current;
        target.y = (IsSelected || IsMouseOver) ? animateHeight : 0;

        transform.position = Vector3.MoveTowards(current, target, animateSpeed * Time.deltaTime);
    }
}