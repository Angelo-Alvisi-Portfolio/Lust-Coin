using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathfindingObject : MonoBehaviour {
    [SerializeField]
    protected bool isTraversable, ignorePathfindingMap;
    public bool IsTraversable => isTraversable;
    public bool IgnorePathfindingMap => ignorePathfindingMap;
    /// <summary>
    /// Size of the object in increments
    /// </summary>
    [SerializeField, OnValueChanged("OnSizeChanged")]
    protected Vector3Int size = new(1, 1, 0);
    [SerializeField, OnValueChanged("OnSizeIncrementsChanged")]
    protected Vector3Int sizeInIncrements = new (16, 16, 0);
    public Vector3Int Size => size;    
    public Vector3Int SizeInIncrements => sizeInIncrements;
    [SerializeField]
    protected Collider2D coll2D;
    public Collider2D Collider2D => coll2D;
    //[SerializeField, HideInInspector]
    //private VectorSortedSet currentCells = new();
    //public VectorSortedSet CurrentCells => currentCells;
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    [SerializeField]
    protected List<GameInteraction> interactions = new();
    [SerializeField, HideInInspector]
    protected int interactionFlag = 0;
    [SerializeField, HideInInspector]
    public float bottomY, cellDY = 0;

    public GameInteraction GetInteraction() {
        if (interactions.Count > interactionFlag) {
            return interactions[interactionFlag];
        } else { 
            return null; 
        }
    }

    public void Initialize(float bottomY, float cellDY) {        
        this.bottomY = bottomY;
        this.cellDY = cellDY;
    }

    public void WalkTowards(Vector3Int destination, PathfindingMap map, float speed) {
        Vector3 dest = map.GetCellCenter(destination);
        transform.position = Vector3.MoveTowards(transform.position, dest, speed);
        //if (transform.position == dest) {
        //    map.MoveEntity(this, destination);
        //}
    }



    public void WalkTowards(Vector3 destination, PathfindingMap map, float speed) {
        float walkDistance = speed * 0.0625f;
        transform.position = Vector3.MoveTowards(transform.position, destination, walkDistance);
        //if (transform.position == destination) {
        //    map.MoveEntity(this, map.GetCellAtPos(destination));
        //}
    }
    
    public HashSet<Vector3Int> GetCells(Vector3Int[] cells, PathfindingMap map) {
        
        HashSet<Vector3Int> cellSet = new() { map.GetCellAtPos(coll2D.bounds.min), map.GetCellAtPos(new Vector2(coll2D.bounds.max.x, coll2D.bounds.min.y)),
            map.GetCellAtPos(new Vector2(coll2D.bounds.min.x, coll2D.bounds.max.y)), map.GetCellAtPos(coll2D.bounds.max)};
        return cellSet;    
    }

    //public void SetCells(VectorSortedSet cells) {
    //    currentCells.Clear();
    //    foreach(Vector3Int cell in cells) {
    //        currentCells.Add(cell);
    //    }
    //}

    protected void OnSizeChanged() {
        sizeInIncrements = new Vector3Int(size.x*16, size.y/16, size.z);
    }

    protected void OnSizeIncrementsChanged() {
        size = new Vector3Int(sizeInIncrements.x / 16, size.y / 16, size.z);
    }

    public Vector2 GetEdge(Vector2 direction) {
        Vector2 edge = new();
        if (direction.x > 0) {
            edge.x = coll2D.bounds.max.x;
        } else if (direction.x < 0) {
            edge.x = coll2D.bounds.min.x;
        } else { 
            edge.x = coll2D.bounds.center.x;
        }
        if (direction.y > 0) {
            edge.y = coll2D.bounds.max.y;
        } else if (direction.y < 0) {
            edge.y = coll2D.bounds.min.y;
        } else {
            edge.y = coll2D.bounds.center.y;
        }
        return edge;
    }    

    public bool HasPointInCollider(Vector2 point) {
        if (coll2D.OverlapPoint(point)) {
            return true;
        }
        return false;
    }

    public void UpdateSortingOrder(PathfindingMap map, TileDistances tileDistances) {
        int height = Mathf.FloorToInt(tileDistances.height());
        spriteRenderer.sortingOrder = height - Mathf.FloorToInt(transform.position.y - map.transform.position.y) - 1; 
    }
}
