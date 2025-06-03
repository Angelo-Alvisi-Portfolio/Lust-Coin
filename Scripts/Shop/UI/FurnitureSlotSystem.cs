using Sirenix.OdinInspector;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FurnitureSlotSystem : SerializedMonoBehaviour {
    [SerializeField]
    private FurnitureSlotRow[] furnitureSlotRows;
    [SerializeField]
    private FurnitureUI furnitureUI;
    public FurnitureUI FurnitureUI => furnitureUI;
    [SerializeField]
    private Shop shop;

    public Furniture GetCurrentFurniture() {
        return furnitureUI.GetCurrentFurniture();
    }
}
