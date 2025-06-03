using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public static class StaticGenerators {    

   /* public static GridMatrix<GameObject> InstantiateGrid(TileDistances distances, GameObject obj, int width, int height) {
        float startx = (width % 2 != 0) ? 0 : distances.BDX/2;
        float starty = (height % 2 != 0) ? 0 : distances.DY/2;
        int hW = Mathf.FloorToInt(width / 2);
        int hH = Mathf.FloorToInt(height / 2);
        List<Vector3> positions = new List<Vector3>();
        GameObject container = GameObject.Instantiate(new GameObject("Grid"));
        for (int v = -hW; v < hW; v++) {
            for (int e = -hH; e < hH; e++) {
                float ox = startx + v * distances.BDX;
                float oy = starty + e * distances.DY;
                positions.Add(new Vector3(ox, oy, 0));
                GameObject newObj = GameObject.Instantiate(obj, container.transform);
                Vector2 pos = new Vector2(ox, oy);
                newObj.transform.position = pos;
                Vector2Int cell = new Vector2Int(v, e);
                newObj.name = "Cell " + cell.ToString();                
                matrix.Add(new Vector2Int(v, e), newObj);
            }
        }
        return matrix;
    }*/
}
