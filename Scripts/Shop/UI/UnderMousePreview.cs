using UnityEngine;
using UnityEngine.UI;

public class UnderMousePreview : MonoBehaviour {

    [SerializeField]
    private Image image;

    private void Update() {
        transform.position = Input.mousePosition;
    }
    
    public void ChangeSprite (Sprite sprite) {
        image.sprite = sprite;
        image.SetNativeSize();
    }

}
