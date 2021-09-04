using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Vector2Int boardSize = new Vector2Int(18, 9);
    [SerializeField] private int tileSize = 1;
    private Vector2 offset;
    private GameObject[,] tiles;
    public Vector2Int BoardSize => boardSize;
    
    void Awake()
    {
        tiles = new GameObject[boardSize.x, boardSize.y];
        offset = -boardSize / 2 * new Vector2(tileSize, tileSize);
        FillBoard();
    }

    private void FillBoard()
    {
        for (int x = 0; x < boardSize.x; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                CreateTile(x, y);
            }
        }
    }
    
    private void CreateTile(int x, int y)
    {
        Vector3 pos = new Vector3( (x) * tileSize + offset.x, (y) * tileSize + offset.y, 0);
        tiles[x, y] = Instantiate(tilePrefab, transform);
        tiles[x, y].transform.position = pos;
    }

    public Vector3 GridToWorld(Vector2Int pos)
    {
        return tiles[pos.x, pos.y].transform.position;
    }

    public Vector2Int WorldToGrid(Vector3 pos)
    {
        Vector2 temp;
        pos -= new Vector3(offset.x, offset.y, 0);
        temp.x = pos.x / tileSize;
        temp.y = pos.y / tileSize;
        return new Vector2Int((int)temp.x, (int)temp.y);
    }
}