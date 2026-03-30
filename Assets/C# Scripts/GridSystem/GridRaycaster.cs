using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class GridRaycaster : MonoBehaviour
{
    [SerializeField] private InputActionReference onClickAction;
    [SerializeField] private LayerMask gridMask;

    private GridCell hoveredGridCell = GridCell.Null;
    private GridCell selectedGridCell = GridCell.Null;

    private bool clickInputBuffered;


    private void Awake()
    {
        onClickAction.action.performed += OnClick;
        onClickAction.action.Enable();
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed == false)
            return;

        clickInputBuffered = true;
    }

    private void Update()
    {
        bool isMouseOverUI = EventSystem.current.IsPointerOverGameObject();

        UpdateHoveredGridCell(isMouseOverUI);

        if (!clickInputBuffered)
            return;

        TrySelectGridCell(isMouseOverUI);
        clickInputBuffered = false;
    }


    #region GriCell Hover/Select Logic

    /// <summary>
    /// Try Get GridCell nder mouse and lose previous <see cref="hoveredGridCell"/>. Also mark their attached animation scripts as Hovered/Unhovered
    /// </summary>
    private void UpdateHoveredGridCell(bool isMouseOverUI)
    {
        if (hoveredGridCell.IsValid)
        {
            hoveredGridCell.AnimatorObject.IsMouseOver = false;
            hoveredGridCell = GridCell.Null;
        }

        if (isMouseOverUI)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gridMask))
            return;

        if (!GridManager.Instance.TryGetGridCellFromWorldPoint(hit.point, out GridCell newGridCell))
            return;

        hoveredGridCell = newGridCell;
        hoveredGridCell.AnimatorObject.IsMouseOver = true;
    }

    /// <summary>
    /// Try Select <see cref="hoveredGridCell"/> and deselect the previously selected cell. Also mark their attached anomations scripts as Selected/Unselected
    /// </summary>
    private void TrySelectGridCell(bool isMouseOverUI)
    {
        if (isMouseOverUI)
            return;

        int currentGridCellId = selectedGridCell.Id;
        if (selectedGridCell.IsValid)
        {
            selectedGridCell.AnimatorObject.IsSelected = false;
            selectedGridCell = GridCell.Null;
        }

        if (hoveredGridCell.IsValid == false || hoveredGridCell.Id == currentGridCellId)
            return;

        selectedGridCell = hoveredGridCell;
        selectedGridCell.AnimatorObject.IsSelected = true;

        if (selectedGridCell.Entry != null)
        {

        }
    }

    #endregion


    private void OnDestroy()
    {
        onClickAction.action.performed -= OnClick;
        onClickAction.action.Dispose();
    }
}