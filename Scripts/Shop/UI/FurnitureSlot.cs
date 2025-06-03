using UnityEngine;
using UnityEngine.EventSystems;

public class FurnitureSlot : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Furniture furniture, tempFurniture;
    private FurnitureSlotSystem slotSystem;

    private void Start() {
        slotSystem = GetComponentInParent<FurnitureSlotSystem>();
    }

    public void SetFurniture() {
        if (furniture == null) {
            furniture = tempFurniture;
        } else {
            if (furniture == tempFurniture) {
                furniture = null;
            } else {
                furniture = tempFurniture;
            }            
            spriteRenderer.sprite = tempFurniture.SpriteRenderer.sprite;
        }
    }

    public void ResetTempFurniture() {
        tempFurniture = null;
        if (furniture == null) {
            spriteRenderer.sprite = null;
        } else {
            spriteRenderer.sprite = furniture.SpriteRenderer.sprite;
        }
    }

    public void SetTempFurniture() {        
        tempFurniture = slotSystem.GetCurrentFurniture();
        spriteRenderer.sprite = tempFurniture.SpriteRenderer.sprite;
    }
}
