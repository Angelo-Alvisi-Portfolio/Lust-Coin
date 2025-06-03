using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingMatrix : GridMatrix<PathfindingGrid2D> {
    [SerializeField]
    protected int gridWidth, gridHeight;
    public PathfindingMatrix(int quadWidth, int quadHeight, int gridWidth, int gridHeight, bool negatives = true, bool stackable = false) : base(quadWidth, quadHeight, negatives) {
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;

        for (int h = 0; h < quadWidth; h++) {
            for (int v = 0; v < quadHeight; v++) {
                pxpy[h][v] = new PathfindingGrid2D(stackable, gridWidth, gridHeight);
                if (negatives) {
                    pxny[h][v] = new PathfindingGrid2D(stackable, gridWidth, gridHeight);
                    nxpy[h][v] = new PathfindingGrid2D(stackable, gridWidth, gridHeight);
                    nxny[h][v] = new PathfindingGrid2D(stackable, gridWidth, gridHeight);
                }
            }
        }
    }

    public PathfindingObject this[Vector2Int c, int i = 0] {
        get => this[c.x, c.y, i];
        //set => this[c.v, c.e] = value;
    }

    public PathfindingObject this[int cellX, int cellY, int i = 0] {
        get {
            (int, int) gridPos = GetGrid(cellX, cellY);
            (int, int) cellIndex = GetCellIndex(cellX, cellY, gridPos);
            return GetQuad(cellX, cellY)[gridPos.Item1][gridPos.Item2][cellIndex.Item1, cellIndex.Item2, i];
        }
    }

    public new List<PathfindingObject> this[Vector2Int c] {
        get => this[c.x, c.y];
    }

    public new List<PathfindingObject> this[int cellX, int cellY] {
        get {
            (int, int) gridPos = GetGrid(cellX, cellY);
            (int, int) cellIndex = GetCellIndex(cellX, cellY, gridPos);
            return GetQuad(cellX, cellY)[gridPos.Item1][gridPos.Item2][cellIndex.Item1, cellIndex.Item2];
        }
    }

    public void AddEntity(PathfindingObject obj, int cellX, int cellY) {
        (int, int) gridPos = GetGrid(cellX, cellY);
        (int, int) cellIndex = GetCellIndex(cellX, cellY, gridPos);
        GetQuad(cellX, cellY)[gridPos.Item1][gridPos.Item2].AddEntity(obj, cellIndex.Item1, cellIndex.Item2);
    }

    public void RemoveEntity(PathfindingObject obj, int cellX, int cellY) {
        (int, int) gridPos = GetGrid(cellX, cellY);
        (int, int) cellIndex = GetCellIndex(cellX, cellY, gridPos);
        GetQuad(cellX, cellY)[gridPos.Item1][gridPos.Item2].RemoveEntity(obj, cellIndex.Item1, cellIndex.Item2);
    }

    private (int, int) GetGrid(int cellX, int cellY) {
        int gridX = cellX > 0 ? Mathf.CeilToInt((float) cellX / gridWidth) - 1 : Mathf.CeilToInt((float) cellX / gridWidth * -1) - 1;
        int gridY = cellY > 0 ? Mathf.CeilToInt((float) cellY / gridHeight) - 1 : Mathf.CeilToInt((float) cellY / gridHeight * -1) - 1;
        return (gridX, gridY);
    }

    private (int, int) GetCellIndex(int cellX, int cellY, (int, int) gridPos) {
        int gridStartIndexX = gridPos.Item1 * gridWidth;
        int gridStartIndexY = gridPos.Item2 * gridHeight;
        int cellIndexX = Mathf.Abs(cellX) - gridStartIndexX - 1;
        int cellIndexY = Mathf.Abs(cellY) - gridStartIndexY - 1;
        return (cellIndexX, cellIndexY);
    }

    public void AddEntity(PathfindingObject obj, VectorSortedSet cells) {
        foreach (Vector3Int cell in cells) {
            AddEntity(obj, cell.x, cell.y);
        }
    }

    public void RemoveEntity(PathfindingObject obj, VectorSortedSet cells) {
        foreach (Vector3Int cell in cells) {
            RemoveEntity(obj, cell.x, cell.y);
        }
    }

}
