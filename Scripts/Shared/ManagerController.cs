using UnityEngine;

public class ManagerController : MonoBehaviour {

    [SerializeField]
    private ReferenceManager referenceManager;
    public ShopStatusManager ShopStatusManager => referenceManager.ShopStatusManager;
    

    private void Start() {
        GetComponent<AStarPathfinding>().gameMap = referenceManager.ShopStatusManager.PathfindingMap;
    }
}
