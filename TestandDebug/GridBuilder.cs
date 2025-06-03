using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class GridBuilder : SerializedMonoBehaviour {
    [SerializeField]
    private PathfindingObject prefab;
    [SerializeField]
    private PathfindingMap map;
    [SerializeField]
    private GameObject gridContainer;
    /*[OdinSerialize]
    private GridMatrix<GameObject> grid;
    public GridMatrix<GameObject> Grid => grid;*/
    //private GridMatrix<Vector2Int, GameObject> grid;
    //public Dictionary<Vector2Int, GameObject> Grid => grid;
    
    [Button]
    public void BuildGrid() {
        if (gridContainer != null) {
            DestroyImmediate(gridContainer);
        }
        gridContainer = new GameObject();
        map.GenerateMap();
        map.IterateAll(InstantiateCell);
        //grid = StaticGenerators.InstantiateGrid(tileDistances, prefab, width, height);
    } 

    private PathfindingObject InstantiateCell(Vector3Int position) {
        //PathfindingObject obj = Instantiate(prefab);
        //obj.transform.SetParent(gridContainer.transform);
        //obj.SetCell(position);
        //obj.transform.position = map.GetPosOfCell(position);
        //obj.name = obj.CurrentCell.ToString();
        //map.AddEntity(obj, obj.CurrentCell);
        return null;
    }
}
