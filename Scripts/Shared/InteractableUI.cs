using UnityEngine;
using UnityEngine.UI;

public class InteractableUI : MonoBehaviour {

    [SerializeField]
    private Image image;

    private void Start() {
        StaticManager.InteractableUI = this;
    }

    public void ChangeSprite(Sprite sprite) {
        image.sprite = sprite;
    }

}

