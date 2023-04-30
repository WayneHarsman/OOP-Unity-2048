using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public GameMaster gameMaster;
    public Tile tilePrefab;

    private TileGrid grid;
    private List<Tile> tiles;
    private bool waiting;
    private bool isPaused;
    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(16);
    }

    public void Pause(bool _pause) { isPaused = _pause; }
    public void ClearBoard()
    {
        foreach (var cell in grid.cells)
        {
            cell.tile = null;
        }

        foreach (var tile in tiles)
        {
            Destroy(tile.gameObject);
        }

        tiles.Clear();
    }

    public void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(2);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }

    private void Update()
    {
        if (!waiting && !isPaused)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector2Int.up, 0, 1, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector2Int.left, 1, 1, 0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector2Int.down, 0, 1, grid.height - 2, -1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector2Int.right, grid.width - 2, -1, 0, 1);
            }
        }
    }

    private void Move(Vector2Int pDirection, int pStartX, int pIncrementX, int pStartY, int pIncrementY)
    {
        bool changed = false;

        for (int x = pStartX; x >= 0 && x < grid.width; x += pIncrementX)
        {
            for (int y = pStartY; y >= 0 && y < grid.height; y += pIncrementY)
            {
                TileCell cell = grid.GetCell(x, y);

                if (cell.occupied)
                {
                    changed |= MoveTile(cell.tile, pDirection);
                }
            }
        }

        if (changed)
        {
            StartCoroutine(WaitForChanges());
        }
    }

    private bool MoveTile(Tile pTile, Vector2Int pDirection)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(pTile.cell, pDirection);

        while (adjacent != null)
        {
            if (adjacent.occupied)
            {
                if (CanMerge(pTile, adjacent.tile))
                {
                    MergeTiles(pTile, adjacent.tile);
                    return true;
                }

                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, pDirection);
        }

        if (newCell != null)
        {
            pTile.MoveTo(newCell);
            return true;
        }

        return false;
    }

    private bool CanMerge(Tile pTileA, Tile pTileB)
    {
        return pTileA.value == pTileB.value && !pTileB.locked;
    }

    private void MergeTiles(Tile pTileA, Tile pTileB)
    {
        tiles.Remove(pTileA);
        pTileA.Merge(pTileB.cell);

        //int index = Mathf.Clamp(IndexOf(pTileB.state) + 1, 0, tileStates.Length - 1);
        int number = pTileB.value * 2;

        pTileB.SetState(number);

        gameMaster.IncreaseScore(number);
    }

    private IEnumerator WaitForChanges()
    {
        waiting = true;

        yield return new WaitForSeconds(0.1f);

        waiting = false;

        foreach (var tile in tiles)
        {
            tile.locked = false;
        }

        if (tiles.Count != grid.size)
        {
            CreateTile();
        }

        if (CheckForGameOver())
        {
            gameMaster.GameOver();
        }
    }

    public bool CheckForGameOver()
    {
        if (tiles.Count != grid.size)
        {
            return false;
        }

        foreach (var tile in tiles)
        {
            TileCell up = grid.GetAdjacentCell(tile.cell, Vector2Int.up);
            if (up != null && CanMerge(tile, up.tile))
            {
                return false;
            }

            TileCell down = grid.GetAdjacentCell(tile.cell, Vector2Int.down);
            if (down != null && CanMerge(tile, down.tile))
            {
                return false;
            }

            TileCell left = grid.GetAdjacentCell(tile.cell, Vector2Int.left);
            if (left != null && CanMerge(tile, left.tile))
            {
                return false;
            }

            TileCell right = grid.GetAdjacentCell(tile.cell, Vector2Int.right);
            if (right != null && CanMerge(tile, right.tile))
            {
                return false;
            }
        }

        return true;
    }

}
