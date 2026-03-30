using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;


public class GridManager : NetworkBehaviour
{
    public static GridManager Instance { get; private set; }

    [SerializeField] private GridSettingsSO gridSettingsSO;
    [SerializeField] private GridCellFloorAnimator gridTilePrefab;
    [SerializeField] private float tileSize, tileHeight;

    [SerializeField] private GridCell[] Grid;
    private int2 GridSize;
    private int GridLength;

    private GridSettings gridSettings;
    private Vector3 gridBottomLeft;



    private void Awake()
    {
        Instance = this;
        gridSettings = gridSettingsSO.Value;
    }

    public override void OnNetworkSpawn()
    {
        GenerateGrid_RPC(gridSettings);
    }

    /// <summary>
    /// Generate a grid on all clients with the same settings to ensure everyone sees the same grid.
    /// </summary>
    [Rpc(SendTo.ClientsAndHost)]
    private void GenerateGrid_RPC(GridSettings settings)
    {
        GridLength = settings.GridLength;
        GridSize = new int2(settings.Width, settings.Height);
        Grid = new GridCell[GridLength];

        gridBottomLeft = -new Vector3(settings.Width * tileSize * 0.5f, 0, settings.Height * tileSize * 0.5f);
        for (int x = 0; x < settings.Width; x++)
        {
            for (int z = 0; z < settings.Height; z++)
            {
                Vector3 worldPos = gridBottomLeft + new Vector3(x * tileSize, 0, z * tileSize) + new Vector3(tileSize * 0.5f, 0, tileSize * 0.5f);

                GridCellFloorAnimator gridFloorTransform = Instantiate(gridTilePrefab, worldPos, Quaternion.identity, transform);

                int gridId = x + z * settings.Width;
                Grid[gridId] = new GridCell(gridId, worldPos, gridFloorTransform);
            }
        }

        GetComponent<BoxCollider>().size = new Vector3(settings.Width * tileSize, tileHeight, settings.Height * tileSize);
    }


    /// <summary>
    /// Try get GridCell from the grid with 3d <paramref name="worldPos"/>
    /// </summary>
    /// <returns>True if any cell was found</returns>
    public bool TryGetGridCellFromWorldPoint(Vector3 worldPos, out GridCell gridCell)
    {
        Vector3 localPos = worldPos - gridBottomLeft;

        int x = Mathf.FloorToInt(localPos.x / tileSize);
        int z = Mathf.FloorToInt(localPos.z / tileSize);

        // Out-of-bounds check
        if (x < 0 || x >= GridSize.x || z < 0 || z >= GridSize.y)
        {
            gridCell = default;
            return false;
        }

        int gridId = x + z * GridSize.x;
        gridCell = Grid[gridId];
        return true;
    }




#if UNITY_EDITOR
    [ContextMenu("Generate Grid")]
    private void DEBUG_GenerateGrid()
    {
        GridSettings settings = gridSettings;

        GridLength = settings.GridLength;
        GridSize = new int2(settings.Width, settings.Height);
        Grid = new GridCell[GridLength];

        gridBottomLeft = -new Vector3(settings.Width * tileSize * 0.5f, 0, settings.Height * tileSize * 0.5f);
        for (int x = 0; x < settings.Width; x++)
        {
            for (int z = 0; z < settings.Height; z++)
            {
                Vector3 worldPos = gridBottomLeft + new Vector3(x * tileSize, 0, z * tileSize) + new Vector3(tileSize * 0.5f, 0, tileSize * 0.5f);

                GridCellFloorAnimator gridFloorTransform = Instantiate(gridTilePrefab, worldPos, Quaternion.identity);

                int gridId = x + z * settings.Width;
                Grid[gridId] = new GridCell(gridId, worldPos, gridFloorTransform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (gridSettingsSO == null) return; 

        GridSettings settings = gridSettingsSO.Value;
        Vector3 bottomLeft = -new Vector3(settings.Width * tileSize * 0.5f - tileSize * 0.5f, 0, settings.Height * tileSize * 0.5f - tileSize * 0.5f);

        for (int x = 0; x < settings.Width; x++)
        {
            for (int z = 0; z < settings.Height; z++)
            {
                Vector3 worldPos = bottomLeft + new Vector3(x * tileSize, 0, z * tileSize);

                Gizmos.DrawWireCube(worldPos, Vector3.one * tileSize);
            }
        }
    }
#endif
}
