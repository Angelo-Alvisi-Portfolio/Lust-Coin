using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class BaseInput : MonoBehaviour {

    [SerializeField]
    protected Axel axel;
    [SerializeField]
    protected ShopInput shopInput;
    [SerializeField]
    protected DungeonInput dungeonInput;
    [SerializeField]
    protected CombatInput combatInput;
    [SerializeField]
    private UIController uiController;

    private static InputStatus inputStatus = InputStatus.Idle;
    public static InputStatus InputStatus => inputStatus;
    private static InputStatus previousInputStatus;
    public static InputStatus PreviousInputStatus => previousInputStatus;

    private SexualInteractable currentSexInteractable;
    [SerializeField, HideInInspector]
    private GameInteraction forcedInteraction;
    public GameInteraction ForcedInteraction { get => forcedInteraction; set => forcedInteraction = value; }
    protected virtual void OnEnable() {
        inputStatus = InputStatus.Idle;
        Debug.Log(name);
    }

    private void Update() {
        if (InputStatus == InputStatus.MouseMoving) {
            if (axel.mouseDest == Vector3.zero) {
                ChangeInputStatus(InputStatus.Idle);
            }
        }
    }

    public static InputStatus ChangeInputStatus(InputStatus newStatus) {
        previousInputStatus = inputStatus;
        inputStatus = newStatus;
        Debug.Log($"InputStatus changed to {inputStatus}");
        Debug.Log($"PreviousInputStatus was {previousInputStatus}");
        return inputStatus;
    }

    public void OnOpenIngame(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }
        if (InputStatus == InputStatus.Idle || InputStatus == InputStatus.EmptyShop) {
            uiController.OpenIngameMenu();
        } 
    }

    public void OnEscape(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }
        if (InputStatus == InputStatus.Idle || InputStatus == InputStatus.EmptyShop) {
            uiController.OpenPauseMenu();
        } else if (InputStatus == InputStatus.Selecting) {
            shopInput.OnEscape();
            //StartCoroutine(WaitAndChangeInput(InputStatus.Idle, 0.1f));
            //shopStatusManager.StartEveningSegment(false, map);
            return;
        }
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }
        if (inputStatus == InputStatus.Dialogue) {
            if (!uiController.NextDialogueLine()) {
                ChangeInputStatus(InputStatus.Idle);
                return;
            }
        }
        if (!context.started || InputStatus != InputStatus.Idle) {
            return;
        }
        if (forcedInteraction) {
            TriggerInteraction(forcedInteraction, "Rami");
            forcedInteraction = null;
            return;
        }
        if (axel.CurrentInteractable) {
            TriggerInteraction(axel.CurrentInteractable.GetInteraction(), axel.CurrentInteractable.name);
            return;
        }        
    }

    private void TriggerInteraction(GameInteraction interaction, string name) {
        interaction.Interact(name);
        if (interaction is DialogueInteraction) {
            ChangeInputStatus(InputStatus.Dialogue);
        }
        axel.moving = false;
        axel.mouseDest = Vector3.zero;
    }

    public void OnSexInteract(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }
        if (InputStatus == InputStatus.Idle && axel.CurrentInteractable != null) {
            if (axel.CurrentInteractable.TryGetComponent(out SexualInteractable interactable)) {
                ChangeInputStatus(InputStatus.SexInteraction);
                axel.Animator.SetBool("Groping", true);
                axel.Animator.SetBool("Walking", false);
                currentSexInteractable = interactable;
                currentSexInteractable.PausePatrol();
                interactable.SetGroping(true);
                axel.transform.position = new Vector3(currentSexInteractable.transform.position.x - 2f + 2.5f, currentSexInteractable.transform.position.y + 0.5f, 0);
                axel.SpriteRenderer.sortingOrder = currentSexInteractable.SpriteRenderer.sortingOrder - 1;
                axel.SpriteRenderer.flipX = false;
                axel.groping = true;
                axel.moving = false;                
                axel.mouseDest = Vector3.zero;
                uiController.OpenSexInteractionUI(currentSexInteractable);
            } 
        }
        return;
    }

    public void ClickOnShop(Vector3 pos) {        
        shopInput.ClickOnShop(pos, axel, currentSexInteractable);
    }

    public void OnPotion(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }
        if (InputStatus == InputStatus.Idle) {
            combatInput.OnPotion1();
        }
    }

    public void OnLowerHP(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }
#if UNITY_EDITOR
        if (InputStatus == InputStatus.Idle) {
            combatInput.OnLowerHP();
        }
#endif
    }

    public void OnPotionPress(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }
        InputBinding binding = (InputBinding)context.action.GetBindingForControl(context.control);
        Debug.Log(binding);
        Debug.Log(binding.path);
        switch (binding.path) {
            case "<Keyboard>/1":
                combatInput.OnPotion1();
                break;
            case "<Keyboard>/2":
                combatInput.OnPotion2();
                break;
        }
        
        if (binding.path == "<Keyboard>/1") {
            combatInput.OnPotion1();
        }
    }
}
