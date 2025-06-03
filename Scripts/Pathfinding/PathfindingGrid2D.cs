using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PathfindingGrid2D {
    
    private bool stackable;
    [OdinSerialize]
    private PathfindingObject[][] unstackedGrid;
    [OdinSerialize]
    private List<PathfindingObject>[][] stackedGrid;
    public PathfindingGrid2D(bool stackable, int width, int height) {
        
        this.stackable = stackable;
        if (stackable) {
            stackedGrid = new List<PathfindingObject>[width][];
            for (int x = 0; x < width; x++) {
                stackedGrid[x] = new List<PathfindingObject>[height];
                for (int y = 0; y < height; y++) {
                    stackedGrid[x][y] = new List<PathfindingObject>();
                }
            }
        } else {
            unstackedGrid = new PathfindingObject[width][];
            for (int x = 0; x < width; x++) {
                unstackedGrid[x] = new PathfindingObject[height];
            }
        }
    } 

    public PathfindingObject this[int x, int y, int i = 0] {
        get => stackable ? stackedGrid[x][y][i] : unstackedGrid[x][y];
    }

    public List<PathfindingObject> this[int x, int y] {
        get => stackedGrid[x][y];
    }

    public void AddEntity(PathfindingObject obj, int x, int y) {
        if (stackable) {
            stackedGrid[x][y].Add(obj);
        } else {
            unstackedGrid[x][y] = obj;
        }
    }

    public void RemoveEntity(PathfindingObject obj, int x, int y) { 
        if (stackable) {
            stackedGrid[x][y].Remove(obj);
        } else {
            unstackedGrid[x][y] = null;
        }
    }   

    public int GetSize() {
        if (stackable) {
            return stackedGrid.Length * stackedGrid[0].Length;
        } else {
            return unstackedGrid.Length * unstackedGrid[0].Length;
        }
    }
}
