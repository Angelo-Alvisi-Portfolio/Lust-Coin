using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureUI : MonoBehaviour {
    private int folder = 1;
    [SerializeField]
    private FurnitureUISlot[] slots = new FurnitureUISlot[16];
    [SerializeField]
    private Sprite highlightSlotBorder, normalSlotBorder;

    [SerializeField]
    private List<Image> toTransparency = new();
    [SerializeField]
    private List<GameObject> toDisable = new();

    private List<Furniture> furniture;
    private int selectedSlot = 0;

    bool visible = false;

    void OnEnable() {
        SelectSlot(slots[0]);
    }

    public void SelectSlot(FurnitureUISlot slot) {
        slots[selectedSlot].Deselect(normalSlotBorder);
        selectedSlot = slot.SlotN;
        slot.Select(highlightSlotBorder);
        //underMousePreview.ChangeSprite(furniture[selectedSlot].SpriteRenderer.sprite);
    }

    public void Populate(List<Furniture> furniture) {                
        for (int i = 0; i < Mathf.Min(furniture.Count, 16); i++) {
            slots[i].SetIcon(furniture[i].Icon);
        }
    }

    public void ToggleFurnitureUI(List<Furniture> furniture) {
        this.furniture = furniture;        
        Populate(furniture);        
        gameObject.SetActive(!gameObject.activeSelf);                
    }

    public void ToggleFurnitureUI() {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public Furniture GetCurrentFurniture() {
        return furniture[selectedSlot];
    }

    public void UIVisibilityToggle(bool toggle) {
        if (!toggle) {
            foreach (Image image in toTransparency) {
                Color color = image.color;
                color.a = 0f;
                image.color = color;
            }
            foreach (GameObject gameObject in toDisable) {
                gameObject.SetActive(false);
            }
            visible = false;
        } else {
            foreach (Image image in toTransparency) {
                Color color = image.color;
                color.a = 1f;
                image.color = color;
            }
            foreach (GameObject gameObject in toDisable) {
                gameObject.SetActive(true);
            }
            visible = true;
        }
    }
}
