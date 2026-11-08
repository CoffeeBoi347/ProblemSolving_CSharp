using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;

    [SerializeField] private GridObjectPool objectPool;

    private GameObject[,] cells;
    private int[,] zoneMap;

    private readonly Color[] zoneColors =
{
        new Color(0.2f, 0.4f, 0.9f),
        new Color(0.2f, 0.7f, 0.4f),
        new Color(0.9f, 0.55f, 0.15f),
        new Color(0.6f, 0.3f, 0.8f)
    };

    private void Awake()
    {
        if (objectPool == null)
        {
            Debug.LogError("GridObjectPool reference is missing!");
            enabled = false;
            return;
        }

        cells = new GameObject[width, height];
        zoneMap = new int[width, height];
        objectPool.Init(width * height);
    }

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int zoneId = GetZoneID(x, y);
                zoneMap[x, y] = zoneId;

                Vector3 pos = new Vector3(x, y, 0);
                GameObject cell = objectPool.Get(pos);

                if (cell == null)
                {
                    Debug.LogError($"Failed to get cell at {x}, {y}");
                    continue;
                }

                SpriteRenderer sr = cell.GetComponent<SpriteRenderer>();

                sr.color = zoneColors[zoneId];
                cells[x, y] = cell;
            }
        }
    }

    private int GetZoneID(int x, int y)
    {
        bool right = x >= width / 2;
        bool top = y >= height / 2;

        if (!right && !top) return 0;
        if (right && !top) return 1;
        if (!right && top) return 2;

        return 3;
    }

    private int GetZoneAt(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return -1;

        return zoneMap[x, y];
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return cells[x, y].transform.position;
    }
}
