using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FurnitureUISlot : MonoBehaviour, IPointerDownHandler {

    [SerializeField]
    private Image border, icon;
    [SerializeField]
    private FurnitureUI furnitureUI;

    private int slotN;
    public int SlotN => slotN;

    void Awake() {
        // Extract all digits from the name and parse them as an integer
        slotN = int.Parse(System.Text.RegularExpressions.Regex.Match(name, @"\d+").Value);
    }

    //public SpriteRenderer Border => border;
    //public SpriteRenderer Icon => icon;

    public void Select(Sprite highlight) {
        border.sprite = highlight;        
    }

    public void Deselect(Sprite nonhl) {
        border.sprite = nonhl;
    }

    public void SetIcon(Sprite sprite) {        
        icon.sprite = sprite;
        if (icon != null) {
            icon.color = Color.white;
        } else {
            icon.color = Color.clear;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        furnitureUI.SelectSlot(this);
    }
}
