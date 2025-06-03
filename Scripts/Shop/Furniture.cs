using UnityEngine;

public class Furniture : PathfindingObject {
    [SerializeField]
    private Sprite icon;

    public Sprite Icon => icon;
}
