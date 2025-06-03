using UnityEngine;
using UnityEngine.EventSystems;

public class UIPointerOutsideClickController : MonoBehaviour, IPointerEnterHandler {
    [SerializeField]
    private FurnitureUI ui;

    public void OnPointerEnter(PointerEventData eventData) {
        ui.UIVisibilityToggle(false);
    }
}
