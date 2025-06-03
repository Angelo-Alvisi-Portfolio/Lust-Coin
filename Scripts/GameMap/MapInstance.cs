using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MapInstance {

    private List<PathfindingObject> objects = new();
    public List<PathfindingObject> Objects => objects;

    public void AddEntity(PathfindingObject obj) {
        objects.Add(obj);
    }

    public void RemoveObject(PathfindingObject obj) {
        objects.Remove(obj);
    }
}
