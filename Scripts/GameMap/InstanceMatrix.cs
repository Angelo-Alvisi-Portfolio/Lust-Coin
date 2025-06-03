using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class InstanceMatrix : FloatGridMatrix<MapInstance> {

    public InstanceMatrix(int horizontalQuadsN, int verticalQuadsN, float quadWidth, float quadHeight, bool negatives = true, bool stackable = false) : base(horizontalQuadsN, verticalQuadsN, quadWidth, quadHeight, negatives) {
        for (int h = 0; h < horizontalQuadsN; h++) {
            for (int v = 0; v < verticalQuadsN; v++) {
                pxpy[h][v] = new MapInstance();
                if (negatives) {
                    pxny[h][v] = new MapInstance();
                    nxpy[h][v] = new MapInstance();
                    nxny[h][v] = new MapInstance();
                }
            }
        }
    }

    public PathfindingObject GetObjectAtPosition(Vector2 pos) {
        MapInstance instance = this[pos];
        foreach (PathfindingObject obj in instance.Objects) {
            if (obj.HasPointInCollider(pos)) {
                return obj;
            }
        }
        return null;
    }

    public void AddEntity (PathfindingObject obj, Vector2 pos) {       
        this[pos].AddEntity(obj);
    }

    public void RemoveEntity(PathfindingObject obj) {
        this[obj.transform.position].RemoveObject(obj);
    }
}
