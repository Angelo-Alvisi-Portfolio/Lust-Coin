using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolNodes", menuName = "Scriptable Objects/PatrolNodes")]
public class PatrolNodes : SerializedScriptableObject {

    [SerializeField]
    private List<PatrolNode> nodes = new();
    public List<PatrolNode> Nodes { get {  return nodes; } }

    [Button("Add Wait Node")]
    public void AddWaitNode() {
        nodes.Add(new WaitPatrolNode(4));
    }
    [Button("Add Direction Node")]
    public void AddDirectionNode() {
        nodes.Add(new DirectionPatrolNode(Vector2Int.zero, 3));
    }
    [Button("Add Goto Node")]
    public void AddGotoNode() {
        nodes.Add(new GotoPatrolNode(Vector3Int.zero));
    }
    [Button("Add Search Node")]
    public void AddSearchNode() {
        nodes.Add(new SearchPatrolNode(null));
    }

    [Serializable]
    public abstract class PatrolNode {
        protected string name;    
    }

    public class WaitPatrolNode : PatrolNode {
        public float seconds;
        public WaitPatrolNode(float seconds) {
            name = "Wait Node";
            this.seconds = seconds;
        }
    }

    public class DirectionPatrolNode : PatrolNode {        
        public Vector2Int direction;
        public int distance;
        public DirectionPatrolNode(Vector2Int direction, int distance) {
            this.direction = direction;
            this.distance = distance;
            name = direction.ToString() + " Node";
        }
    }

    public class GotoPatrolNode : PatrolNode {
        public Vector3Int cell;
        public GotoPatrolNode(Vector3Int cell) {
            this.cell = cell;
            name = "Goto " + cell.ToString() + " Node";
        }
    }

    public class SearchPatrolNode : PatrolNode {
        public PathfindingObject target;
        public SearchPatrolNode(PathfindingObject target) {
            this.target = target;
            name = "Find Node";
        }
    }
}
