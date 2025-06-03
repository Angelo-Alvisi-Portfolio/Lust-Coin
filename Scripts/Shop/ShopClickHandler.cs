using UnityEngine;
using UnityEngine.EventSystems;

public class ShopClickHandler : MonoBehaviour, IPointerDownHandler {

    [SerializeField]
    private BaseInput input;

    void Start() {        
        input = FindFirstObjectByType<BaseInput>(FindObjectsInactive.Include);        
    }

    public void OnPointerDown(PointerEventData eventData) {
        Vector3 v = eventData.pointerCurrentRaycast.worldPosition;
        input.ClickOnShop(v);
    }
}
