using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GridRaycaster : MonoBehaviour
{
    [SerializeField] private InputActionReference onClickAction;
    [SerializeField] private LayerMask gridMask;

    public GridCell selectedGridCell;

    private bool processClick;

    private void Awake()
    {
        onClickAction.action.performed += OnClick;
        onClickAction.action.Enable();
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed == false)
            return;

        processClick = true;
    }

    private void Update()
    {
        if (processClick == false)
            return;

        processClick = false;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        int prevCelGridId = selectedGridCell.GridId;
        ResetSelectedGridCell();

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gridMask))
            return;

        if (!GridManager.Instance.TryGetGridCellFromWorldPoint(hit.point, out GridCell newGridCell))
            return;

        if (newGridCell.GridId == prevCelGridId)
            return;

        selectedGridCell = newGridCell;
        selectedGridCell.GridFloorTrans.IsSelected = true;

        if (selectedGridCell.GridEntry != null)
        {

        }
    }

    private void ResetSelectedGridCell()
    {
        if (selectedGridCell.GridFloorTrans != null)
        {
            selectedGridCell.GridFloorTrans.IsSelected = false;
            selectedGridCell = default;
        }
    }

    private void OnDestroy()
    {
        onClickAction.action.performed -= OnClick;
        onClickAction.action.Dispose();
    }
}