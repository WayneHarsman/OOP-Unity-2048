using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }

    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size / height;

    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();
    }

    private void Start()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            for (int j = 0; j < rows[i].cells.Length; j++)
            {
                var cell = rows[i].cells[j];
                cell.coordinates = new Vector2Int(j, i);
                cell.transform.position = new Vector3(j, 0, -i);
            }
        }
    }

    public TileCell GetCell(Vector2Int pCoordinates)
    {
        return GetCell(pCoordinates.x, pCoordinates.y);
    }

    public TileCell GetCell(int pX, int pY)
    {
        if (pX >= 0 && pX < width && pY >= 0 && pY < height) {
            return rows[pY].cells[pX];
        }

        return null;
    }

    public TileCell GetAdjacentCell(TileCell pCell, Vector2Int pDirection)
    {
        Vector2Int coordinates = pCell.coordinates;
        coordinates.x += pDirection.x;
        coordinates.y -= pDirection.y;

        return GetCell(coordinates);
    }

    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;

        while (cells[index].occupied)
        {
            index++;

            if (index >= cells.Length) {
                index = 0;
            }

            // all cells are occupied
            if (index == startingIndex) {
                return null;
            }
        }

        return cells[index];
    }

}
