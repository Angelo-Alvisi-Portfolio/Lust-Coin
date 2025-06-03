using UnityEngine;

public class UIController : MonoBehaviour {
    [SerializeField]
    private SexInteractionUI sexInteractionUI;
    [SerializeField]
    private DialogueSystem dialogueSystem;
    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private IngameMenu ingameMenu;
    [SerializeField]
    private InteractableUI interactableUI;

    public void OpenIngameMenu() {
        ingameMenu.gameObject.SetActive(true);
    }

    public void OpenPauseMenu() {
        pauseMenu.gameObject.SetActive(true);
    }
    public void OpenSexInteractionUI(SexualInteractable interactable) {
        sexInteractionUI.gameObject.SetActive(true);
        sexInteractionUI.SetAnimation(interactable.CharacterNumber, 0);
    }

    public void CloseSexInteractionUI() {
        sexInteractionUI.ResetUI();
    }

    public bool NextDialogueLine() {
        return dialogueSystem.NextDiagLine();
    }

    public void InteractableUIOn() {
        interactableUI.gameObject.SetActive(true);
    }

    public void InteractableUIOff() {
        interactableUI.gameObject.SetActive(false);
    }

    public void ChangeInteractableUISprite(Sprite sprite) {
        interactableUI.ChangeSprite(sprite);
    }

}
