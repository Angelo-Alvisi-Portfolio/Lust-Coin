using Sirenix.OdinInspector;
using UnityEngine;

public class BoundingBox : MonoBehaviour {    
    private int x;
    private int y;
    public void SetPos(int x, int y) {
        this.x = x; this.y = y;
    }
    [Button]
    public void GetPos() {
        PathfindingMap map = FindFirstObjectByType<PathfindingMap>();
        Debug.Log(map.GetCellAtPos(transform.position));
    }

}
