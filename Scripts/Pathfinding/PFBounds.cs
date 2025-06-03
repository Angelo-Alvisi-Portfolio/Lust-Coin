using System;
using UnityEngine;

[Serializable]
public class PFBounds {
    private float minX, maxX, minY, maxY;
    public float MinX => minX;
    public float MaxX => maxX;
    public float MinY => minY;
    public float MaxY => maxY;
    private PathfindingObject obj;

    public PFBounds(PathfindingObject obj, Vector3 position, Vector3Int size) {
        this.obj = obj;
        float xOffset = size.x / 2f;
        float yOffset = size.y / 2f;
        minX = position.x - xOffset;
        minY = position.y - yOffset;
        maxX = position.x + xOffset;
        maxY = position.y + yOffset;
    }

    public void Move(Vector3 position, Vector3Int size) {
        float xOffset = size.x / 2f;
        float yOffset = size.y / 2f;
        minX = position.x - xOffset;
        minY = position.y - yOffset;
        maxX = position.x + xOffset;
        maxY = position.y + yOffset;
    }


    public bool IsPointInBounds(Vector2 point, Collider2D collider) {
        return point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY;
    }


}
