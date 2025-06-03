using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class AStarPathfinding : MonoBehaviour {    
    public enum Heuristic { Manhattan, Chebyshev, Euclidean }
    [SerializeField]
    private Heuristic heuristic;
    [SerializeField]
    private bool octile, straightenLines;
    
    public PathfindingMap gameMap;

    

    [SerializeField, Range(0, 100)]
    private int straightenDistance;


    private float HeuristicResult(Vector3Int startCell, Vector3Int endCell) {
        switch (heuristic) {
            case Heuristic.Manhattan:
                return Math.Abs(startCell.x - endCell.x) + Math.Abs(startCell.y - endCell.y);
            case Heuristic.Chebyshev:
                return Mathf.Max(Math.Abs(startCell.x - endCell.x), Math.Abs(startCell.y - endCell.y));
            case Heuristic.Euclidean:
                return Vector3Int.Distance(startCell, endCell);
            default:
                return 0;
        }
    }

    private float HeuristicResult(Vector2 startPos, Vector2 endPos) {
        switch (heuristic) {
            case Heuristic.Manhattan:
                return Mathf.Abs(startPos.x - endPos.x) + Mathf.Abs(startPos.y - endPos.y);
            case Heuristic.Chebyshev:
                return Mathf.Max(Math.Abs(startPos.x - endPos.x), Math.Abs(startPos.y - endPos.y));
            case Heuristic.Euclidean:
                return Vector2.Distance(startPos, endPos);
            default:
                return 0;
        }
    }

    

    //private List<Node> GetNeighbors(Node startNode) {
    //    List<Node> neighbors = new List<Node>();
    //    int[,] directions;
    //    if (!octile) {
    //        int[,] array = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
    //        directions = array;
    //    } else {
    //        int[,] array = { { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 } };
    //        directions = array;
    //    }

    //    for (int characterNumber = 0; characterNumber < directions.GetLength(0); characterNumber++) {
    //        int tX = startNode.x + directions[characterNumber, 0];
    //        if (tX == 0) {
    //            tX += directions[characterNumber, 0];
    //        }             
    //        int tY = startNode.y + directions[characterNumber, 1];
    //        if (tY == 0) {
    //            tY += directions[characterNumber, 1];
    //        }
    //        if (tX >= gameMap.MinX && tX <= gameMap.MaxX && tY >= gameMap.MinY && tY <= gameMap.MaxY) {
    //            neighbors.Add(new Node(tX, tY, startNode.g + 1, 0, null));
    //        }
    //    }
    //    return neighbors;
    //}

    private List<Vector2Node> GetNeighbors(Vector2Node parentNode, float step) {
        List<Vector2Node> neighbors = new();
        float[,] directions = octile
            ? new float[,] { { step, 0 }, { step, step }, { 0, step }, { -step, step }, { -step, 0 }, { -step, -step }, { 0, -step }, { step, -step } }
            : new float[,] { { step, 0 }, { 0, step }, { -step, 0 }, { 0, -step } };

        (Vector2, Vector2) insideBounds = gameMap.GetInsideBounds();
        for (int i = 0; i < directions.GetLength(0); i++) {
            float tX = parentNode.v.x + directions[i, 0];
            float tY = parentNode.v.y + directions[i, 1];
            
            if (tX >= insideBounds.Item1.x && tX <= insideBounds.Item2.x && tY >= insideBounds.Item1.y && tY <= insideBounds.Item2.y) {
                
                neighbors.Add(new Vector2Node(new Vector2(tX, tY), parentNode.g + step, 0, parentNode));
            }
        }
        return neighbors;
    }



    private List<Node> ResetPath(Node node) {
        List<Node> nodes = new List<Node>();
        while (node != null) {
            nodes.Add(node);
            node = node.parent;
        }
        nodes.Reverse();
        nodes.RemoveAt(0);
        return nodes;
    }

    private List<Vector2Node> ResetPath(Vector2Node node) {
        List<Vector2Node> nodes = new List<Vector2Node>();
        while (node != null) {
            nodes.Add(node);
            node = node.parent;
        }
        nodes.Reverse();
        nodes.RemoveAt(0);
        return nodes;
    }


    //public List<Vector3Int> StartSearch(Vector3Int startCell, PathfindingObject target) {
    //    return StartSearch(startCell, target.CurrentCell, target);
    //}

    //public List<Vector3Int> StartSearch(Vector3Int startCell, Vector3 destination, bool raw = true) {
    //    return StartSearch(startCell, gameMap.GetCellAtPos(destination));
    //}

    //public List<Vector3Int> StartSearch(Vector3Int startCell, Vector3Int targetCell, PathfindingObject target = null) {
    //    Queue<Node> openList = new();
    //    Dictionary<Vector3Int, Node> allNodes = new();

    //    Node startNode = new(startCell.x, startCell.y, 0, HeuristicResult(startCell, targetCell));
    //    openList.Enqueue(startNode);
    //    allNodes.Add(new Vector3Int(startCell.x, startCell.y), startNode);
    //    while (openList.Count > 0) {
    //        Node current = openList.Dequeue();

    //        if (current.x == targetCell.x && current.y == targetCell.y) {
    //            return ConvertNodeList(ResetPath(current));
    //        }

    //        foreach (Node n in GetNeighbors(current)) {                
    //            if (!gameMap.IsTraversable(new Vector3Int(n.x, n.y), target)) {
    //                continue;
    //            }
    //            n.h = HeuristicResult(new Vector3Int(n.x, n.y), targetCell);
    //            n.parent = current;

    //            Vector3Int key = new(n.x, n.y);
    //            if (!allNodes.ContainsKey(key) || n.g < allNodes[key].g) {
    //                allNodes[key] = n;
    //                openList.Enqueue(n);
    //            }
    //        }
    //    }
    //    return null;
    //}

    public List<Vector2> StartSearch(Vector2 startPos, Vector2 endPos, PathfindingObject actor, float step) {
        PriorityQueue<Vector2Node, float> openList = new();
        HashSet<Vector2> closedList = new();
        if (!gameMap.IsTraversable(endPos, actor, null)) {
            return null;
        }
        float boundsWidthFromCenter = actor.Collider2D.bounds.extents.x;
        
        Vector2 edge = actor.GetEdge((endPos - startPos).normalized);

        Vector2Node startNode = new(edge, 0, HeuristicResult(startPos, endPos));
        openList.Enqueue(startNode, startNode.F);
        closedList.Add(edge);
        while (openList.Count > 0) {
            Vector2Node current = openList.Dequeue();            

            //if (current.v == endPos) {
            //    return ConvertNodeList(ResetPath(current));
            //}

            if (Mathf.Abs(endPos.x - current.v.x) <= step && Mathf.Abs(endPos.y - current.v.y) <= step) {
                return ConvertNodeList(ResetPath(current));
            }
            foreach (Vector2Node n in GetNeighbors(current, step)) {        
                
                if (!gameMap.IsTraversable(new Vector2(n.v.x, n.v.y), actor, null)) {
                    continue;
                }
                n.h = HeuristicResult(new Vector2(n.v.x, n.v.y), endPos);
                n.parent = current;
                if (!closedList.Contains(n.v)) {
                    openList.Enqueue(n, n.F);
                    closedList.Add(n.v);
                }
            }
        }
        return null;
    }

    //private List<Vector3Int> ConvertNodeList(List<Node> nodes) {
    //    List<Vector3Int> list = new List<Vector3Int>();
    //    foreach (Node node in nodes) {
    //        list.Add(new Vector3Int(node.x, node.y));
    //    }
    //    List<List<Vector3Int>> straightLines;
    //    if (straightenLines) {
    //        straightLines = StraightenPath(list);

    //        foreach (List<Vector3Int> sL in straightLines) {
    //            sL.Reverse();
    //            int characterNumber = list.IndexOf(sL[0]);
    //            for (int n = 0; n < sL.Count; n++) {
    //                list[characterNumber + n] = sL[n];
    //            }
    //        }
    //    } else {
    //        list.Reverse();
    //    }
    //    return list;
    //}

    private List<Vector2> ConvertNodeList(List<Vector2Node> nodes) {
        List<Vector2> list = new();
        foreach (Vector2Node node in nodes) {
            list.Add(new Vector2(node.v.x, node.v.y));
        }
        //List<List<Vector2>> straightLines;
        //if (straightenLines) {
        //    straightLines = StraightenPath(list);

        //    foreach (List<Vector3Int> sL in straightLines) {
        //        sL.Reverse();
        //        int characterNumber = list.IndexOf(sL[0]);
        //        for (int n = 0; n < sL.Count; n++) {
        //            list[characterNumber + n] = sL[n];
        //        }
        //    }
        //} else {
            //list.Reverse();
        //}
        return list;
    }


    //private List<List<Vector3Int>> StraightenPath(List<Vector3Int> nodes) { 
    //    List<Vector3Int> nodesToTraverse = nodes.ToList();
    //    List<List<Vector3Int>> straightenedLines = new List<List<Vector3Int>>();
    //    while (nodesToTraverse.Count > 2) {
    //        List<Vector3Int> straightenedLine = StraightenLine(nodesToTraverse);
    //        if (straightenedLine == null) {
    //            nodesToTraverse.RemoveAt(0);
    //        } else {
    //            if (straightenedLine.Count != nodesToTraverse.Count) {
    //                straightenedLines.Add(straightenedLine);
    //                nodesToTraverse.RemoveRange(0, straightenedLine.Count);                    
    //            } else {
    //                straightenedLines.Add(straightenedLine);
    //                break;
    //            }
    //        }
    //    }
    //    return straightenedLines;
    //}

    /// <summary>
    /// Traverses a list of nodes to find if all nodes are traversable. They are supposed to have the same difficulty to traverse.
    /// </summary>
    /// <param name="nodes">The list of nodes to traverse, from a start node to an end node</param> 
    /// <returns></returns>
    //private List<Vector3Int> StraightenLine(List<Vector3Int> nodes) {     
    //    if (nodes.Count < 3) {
    //        return null;
    //    }
    //    //Get the starting node
    //    Vector3Int sNode = nodes[0];
    //    //The distance is the smallest number between the maximum search distance and the number of nodes in the list, minus 1
    //    int distance = Math.Min(straightenDistance, nodes.Count - 1);
    //    //Loop while the distance is 2 or more cells
    //    while (distance > 1) { 
    //        //Get the final node
    //        Vector3Int eNode = nodes[distance];
    //        //Check if there is a straight lineN between Start and End, if false, reduce distance by 1
    //        if (IsStraight(sNode, eNode)) {                
    //            List<Vector3Int> tenativeList = new List<Vector3Int>();
    //            //DeltaVector
    //            Vector3Int deltaV = eNode - sNode;                
    //            Vector3Int next = eNode;
    //            bool straightLine = true;
    //            //Loop between all nodes in the lineN, if a straight lineN can be created, return the list, else, reduce the distance by the number of checked nodes +1
    //            for (int d = 0; d < distance; d++) {
    //                if (deltaV.x < 0) {
    //                    next.x++;
    //                } else if (deltaV.x > 0) {
    //                    next.x--;
    //                }
    //                if (deltaV.y < 0) {
    //                    next.y++;
    //                } else if (deltaV.y > 0) {
    //                    next.y--;
    //                }
    //                if (!gameMap.IsTraversable(next, null)) {
    //                    distance -= (d + 1);
    //                    straightLine = false;
    //                    break;
    //                }
    //                tenativeList.Add(next);
    //            }
    //            if (straightLine) {
    //                return tenativeList;
    //            }
    //        } else {
    //            distance--;
    //        }
    //    }
    //    return null;
    //}

    private bool IsStraight(Vector3Int start, Vector3Int end) {
        if (start.x == end.x || start.y == end.y || (start.x - end.x) == (start.y - end.y)) {
            return true;
        } else {
            return false;
        }
    }



    class Node {
        internal int x;
        internal int y;
        internal float g;
        internal float h;
        internal Node parent;

        internal Node(int x, int y, float g, float h, Node parent = null) {
            this.x = x; this.y = y; this.g = g; this.h = h; this.parent = parent;
        }

        internal float F() {
            return g + h;
        }

        internal int CompareTo(Node other) {
            return F().CompareTo(other.F());
        }
    }

    class Vector2Node : IComparable<Vector2Node> {
        internal Vector2 v;
        internal float g;
        internal float h;
        internal Vector2Node parent;

        internal Vector2Node(Vector2 v, float g, float h, Vector2Node parent = null) {
            this.v = v; this.g = g; this.h = h; this.parent = parent;
        }

        public int CompareTo(Vector2Node other) {
            return F.CompareTo(other.F);
        }

        internal float F => g + h;
    }
}