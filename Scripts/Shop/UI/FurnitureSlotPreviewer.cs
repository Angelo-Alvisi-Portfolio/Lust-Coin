using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FurnitureSlotPreviewer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private FurnitureSlot slot;

    public void OnPointerEnter(PointerEventData eventData) {
        slot.SetTempFurniture();
    }

    public void OnPointerExit(PointerEventData eventData) {
        slot.ResetTempFurniture();
    }
}
