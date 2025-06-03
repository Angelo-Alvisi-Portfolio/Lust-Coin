using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PathfindingMap : SerializedMonoBehaviour {
    [SerializeField]
    private int depth = 1;
    [SerializeField]
    private float width = 20f, height = 20f, maxWidth = 20f, maxHeight = 20f;
    [SerializeField]
    private bool stackable, unstackableBump, useNegatives, roundPrimeSize;
    [OdinSerialize]
    private InstanceMatrix[] planes;
    [SerializeField]
    private TileDistances tileDistances;
    private float xOffset, yOffset;
    public TileDistances TileDistances => tileDistances;
    [SerializeField, HideInInspector]
    private int hSegs, vSegs;

    //Min\Max X\Y of the map
    //public int MinX => !useNegatives ? 1 : width * -1;
    //public int MaxX => width;

    //public int MinY => !useNegatives ? 1 : height * -1;
    //public int MaxY => height;

    //public List<PFBounds> objBounds = new();
    public List<Collider2D> objBounds = new();

    //private int OffsetX => hSegs * maxWidth - width;
    //private int OffsetY => vSegs * maxHeight - height;

    //public int Size => width * height * depth;

    private void Awake() {
        xOffset = -transform.position.x + (width / 2);
        yOffset = -transform.position.y;
        if (planes == null) {
            GenerateMap();
        }
        PathfindingObject[] objects = FindObjectsByType<PathfindingObject>(FindObjectsSortMode.None);
        foreach (PathfindingObject obj in objects) {
            if (!obj.IgnorePathfindingMap) {
                AddEntity(obj);
            }
            
        }
    }

    /// <summary>
    /// Generates the map, called in Awake()
    /// </summary>
    public void GenerateMap() {
        if (width < 1) {
            width = 1;
        } 
        if (height < 1) {
            height = 1;
        }  
        if (depth < 1) {
            depth = 1;
        }
        (int, float) widthTuple = StaticMath.FindResultAndDifference(width, maxWidth);
        (int, float) heightTuple = StaticMath.FindResultAndDifference(height, maxHeight);
        hSegs = widthTuple.Item1;
        vSegs = heightTuple.Item1;
        planes = new InstanceMatrix[depth];
        for (int d = 0; d < depth; d++) {
            planes[d] = new InstanceMatrix(hSegs, vSegs, maxWidth, maxHeight, useNegatives);            
        }
    }

    //Values must start from 1 or -1
    //public PathfindingObject this[Vector2Int c, int characterNumber = 0] {
    //    get { 
    //        return this[new Vector3Int(c.x, c.y, 0), characterNumber];
    //    }
    //}

    //public PathfindingObject this[Vector3Int c, int characterNumber = 0] {
    //    get {
    //        return planes[c.z][c.x, c.y, characterNumber];
    //    }
    //}

    public PathfindingObject this[Vector2 worldPos] {
        get {
            Collider2D collider = Physics2D.OverlapPoint(worldPos);
            if (collider == null) {
                return null;
            }
            return collider.GetComponent<PathfindingObject>();
        }
    }

    //public List<PathfindingObject> this[Vector2Int c] {
    //    get => this[new Vector3Int(c.x, c.y)];
    //}

    //public List<PathfindingObject> this[Vector3Int c] {
    //    get {
    //        return planes[c.z][c.x, c.y];
    //    }
    //}

    /// <summary>
    /// Adds a PathfindingObject to the 3D map
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="cell"></param>
    public void AddEntity(PathfindingObject obj) {
        //VectorSortedSet cells = GetCellsAtCollider(obj.Collider2D);

        Vector2 pos = obj.transform.position;
        pos += new Vector2(xOffset, yOffset);
        planes[0].AddEntity(obj, pos);
        //obj.SetCells(cells);
        if (!obj.IsTraversable) {
            objBounds.Add(obj.Collider2D);
        }
    }

    public void AddEntity(PathfindingObject obj, Vector3 worldPos) {
        obj.transform.SetParent(null);
        obj.transform.position = worldPos;
        Physics2D.SyncTransforms();
        AddEntity(obj);
    }

    /// <summary>
    /// Searches for an Entitity and removes it from the map
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveEntity(PathfindingObject obj) {
        planes[0].RemoveEntity(obj);        
    }

    /// <summary>
    /// Moves an entity to another cell on the 3D map
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="end"></param>
    //public void MoveEntity(PathfindingObject obj, Vector3Int end) {
    //    planes[0].RemoveEntity(obj, obj.CurrentCells);
    //    planes[end.z].AddEntity(obj, end.x, end.y);
    //    obj.SetCells(GetCellsAtCollider(obj.Collider2D));
    //}

    /// <summary>
    /// Determines if a cell is passable on a 2D map, if target is not null the target's cell will be considered passable
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    //public bool IsTraversable(Vector2Int pos, PathfindingObject target) {
    //    return IsTraversable(new Vector3Int(pos.x, pos.y, 0), target);
    //}

    /// <summary>
    /// Determines if a cell is passable on a 2D map, if target is not null the target'v cell will be considered passable
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    //public bool IsTraversable(Vector2 pos, PathfindingObject target) {        
    //    if (stackable) {
    //        List<PathfindingObject> list = this[pos];            
    //        foreach (PathfindingObject obj in list) {
    //            if (target != null && obj == target) {
    //                return true;
    //            }
    //            if (!obj.IsTraversable) {
    //                return false;
    //            }
    //        }
    //        return true;
    //    } else {
    //        PathfindingObject obj = this[pos, 0];
    //        if (target != null && obj == target) {
    //            return true;
    //        }
    //        if (obj == null || obj.IsTraversable) {
    //            return true;
    //        } else {
    //            return false;
    //        }
    //    }
    //} 

    public bool IsTraversable(Vector2 pos, PathfindingObject actor, PathfindingObject target) {
        return !IsInNonTraversableBounds(actor, pos);
    }
    /*
    /// <summary>
    /// Determines where on the map a grid is to be placed
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    private Vector2Int DetermineQuadrant(int h, int v) {
        float fH = h / 2;
        float vH = v / 2;
        bool negH = h % 2 == 0;
        bool negV = v % 2 == 0;
        int qH = Mathf.CeilToInt(fH * (negH ? 1 : -1));    
        int qV = Mathf.CeilToInt(vH * (negV ? 1 : -1));
        return new Vector2Int(qH, qV);
    }*/

    /// <summary>
    /// Returns the world position of a cell on a 2D map
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Vector2 GetPosOfCell(Vector2Int pos) {
        Vector2Int r = pos;
        if (pos.x > 0) {
            r.x--;
        }
        if (pos.y > 0) {
            r.y--;
        }
        float posx = tileDistances.SX + tileDistances.BDX * r.x + tileDistances.SkewHeight * r.y;
        float posy = tileDistances.SY + tileDistances.DY * r.y;
        return new Vector2(posx, posy);
    }

    /// <summary>
    /// Returns the world position of a cell on a 3D map
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Vector2 GetPosOfCell(Vector3Int pos) {
        return GetPosOfCell(new Vector2Int(pos.x, pos.y));
    }

    public Vector2 GetCellCenter(Vector3Int cell) {
        Vector2 centerOfBase = GetPosOfCell(cell);
        Vector2 center = new Vector2(centerOfBase.x, centerOfBase.y);
        center.y += tileDistances.DY / 2;
        center.x += tileDistances.FDX - tileDistances.BDX / 2;
        return center;
    }

    /// <summary>
    /// Returns the cell at world position in a 2D map
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Vector3Int GetCellAtPos(Vector2 pos) {
        float fy = (pos.y - tileDistances.SY) / tileDistances.DY;
        int y = fy > 0 ? Mathf.FloorToInt(fy) : Mathf.CeilToInt(fy);

        int skewN = Mathf.CeilToInt(fy / tileDistances.SkewHeight) - 1;
        float pdx = skewN * 0.0625f;
        float dx = pos.x - (tileDistances.SXLC + pdx);
        int x = dx > 0 ? Mathf.FloorToInt(dx / tileDistances.BDX) : Mathf.CeilToInt(dx / tileDistances.BDX);

        // Adjust x and y based on their signs
        x = x >= 0 ? x + 1 : x - 1;
        y = y >= 0 ? y + 1 : y - 1;

        return new Vector3Int(x, y, 0);
    }

    public void IterateAll(Func<Vector3Int, PathfindingObject> func) {
        for (int x = 1; x <= width; x++) {
            for (int y = 1; y <= height; y++) {
                for (int z = 0; z < depth; z++) {
                    func(new Vector3Int(x, y, z));
                    if (useNegatives) {
                        func(new Vector3Int(x, -y, z));
                        func(new Vector3Int(-x, y, z));
                        func(new Vector3Int(-x, -y, z));
                    }                    
                }
            }
        }
    }

    private bool IsInNonTraversableBounds(PathfindingObject actor, Vector2 point) {        
        foreach (Collider2D bound in objBounds) {         
            
            if (actor != null && actor.Collider2D == bound) {
                continue;
            }
            if (bound.bounds.Contains(point)) {
                return true;
            }
        }
        return false;
    }

    public bool IsInsideBounds(Bounds bounds, Vector2 position) {
        if (position.y <= tileDistances.LBIB.y || position.y >= tileDistances.RTIB.y || position.x - bounds.extents.x <= tileDistances.LBIB.x || position.x + bounds.extents.x >= tileDistances.RTIB.x) {
            return false;
        }
        return true;
    }

    public (Vector2, Vector2) GetInsideBounds() {
        return (tileDistances.LBIB, tileDistances.RTIB);
    }
    private VectorSortedSet GetCellsAtCollider(Collider2D collider) {
        Vector3Int cellA = GetCellAtPos(collider.bounds.min);
        Vector3Int cellB = GetCellAtPos(collider.bounds.max);
        SortedSet<int> sortedX = new() { cellA.x, cellB.x };
        SortedSet<int> sortedY = new() { cellA.y, cellB.y };
        VectorSortedSet cells = new ();        
        for (int x = sortedX.Min; x <= sortedX.Max; x++) {
            for (int y = sortedY.Min; y <= sortedY.Max; y++) {
                cells.Add(new Vector3Int(x, y, 0));
            }            
        }
        return cells;
    }

    public void ToggleNPCS(bool toggle) {
        foreach (Collider2D bound in objBounds) {
            if (bound.GetComponent<NPCAI>() != null) {
                bound.gameObject.SetActive(toggle);
            }
        }
    }

}
