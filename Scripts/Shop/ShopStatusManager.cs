using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ShopStatusManager : MonoBehaviour {

    [SerializeField]
    private Shop shop;
    [SerializeField]
    private FurnitureUI furnitureEditorUI;
    [SerializeField]
    private FurnitureSlotSystem furniturePlacementUI;
    [SerializeField]
    private PathfindingMap pathfindingMap;
    [SerializeField]
    private ReferenceManager referenceManager;
    [SerializeField, HideInInspector]
    private bool visits = false;

    private void Awake() {
        referenceManager.SetSSM(this);
    }

    public PathfindingMap PathfindingMap => pathfindingMap;
    public void StartShopDaySegment(Axel axel) {
        BaseInput.ChangeInputStatus(InputStatus.Idle);
        shop.SetVisits(axel);
        visits = true;
    }

    public void EndShopDaySegment(Axel axel) {
        BaseInput.ChangeInputStatus(InputStatus.EmptyShop);
        if (visits) {
            shop.ResetVisits(axel);
            visits = false;
        }
    }

    public void StartEveningSegment(bool firstTime) {
        if (firstTime) {
            furnitureEditorUI.ToggleFurnitureUI(shop.StoredFurniture);
        } else {
            furnitureEditorUI.ToggleFurnitureUI();
        }
        BaseInput.ChangeInputStatus(InputStatus.Selecting);
        furniturePlacementUI.gameObject.SetActive(true);
    }

    public void EndNightSegment() {
        furnitureEditorUI.ToggleFurnitureUI();
        furniturePlacementUI.gameObject.SetActive(false);
        BaseInput.ChangeInputStatus(InputStatus.EmptyShop);
    }

}
