using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTest : MonoBehaviour {
    [SerializeField]
    private PathfindingMap map;
    [SerializeField]
    private Vector3Int startCell, endCell;
    [SerializeField]
    private AStarPathfinding pathfinding;

    private List<Vector3Int> path = new List<Vector3Int>();

    //[Button]
    //public void Test() {
    //    path = pathfinding.StartSearch(startCell, endCell);
    //    foreach (Vector2Int cell in path) {
            
    //        map[cell, 0].GetComponent<SpriteRenderer>().color = Color.red;
    //    }
    //}

    //[Button]
    //public void Reset() {
    //    foreach (Vector2Int cell in path) {
    //        map[cell, 0].GetComponent<SpriteRenderer>().color = Color.black;
    //    }
    //}


}
