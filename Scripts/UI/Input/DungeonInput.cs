using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class DungeonInput : MonoBehaviour {

    private Vector3 dest = Vector3.zero;
    [SerializeField]
    private TransitionInteraction exitInteraction;
    [SerializeField]
    private DialogueInteraction impInteraction;
    [SerializeField]
    private Axel axel;
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private GameInteraction ramiInteraction;
    [SerializeField]
    private GameInteraction exitDungeon;
    [SerializeField]
    private BaseInput baseInput;

    protected void OnEnable() {
        CheckInteractable();
    }


    public void ClickOnDungeon(Vector3 pos) {
        if (BaseInput.InputStatus == InputStatus.Idle) {
            dest = new Vector3(pos.x, 0, 0);
            StartCoroutine(AxelMoving());
            BaseInput.ChangeInputStatus(InputStatus.DungeonMoving);
        //} else if (BaseInput.InputStatus == InputStatus.DungeonMoving) {
        //    //axelMoving = false;
        //    BaseInput.ChangeInputStatus(InputStatus.Idle);
        //    //dest = Vector3.zero;
            //StopCoroutine(AxelMoving());
        } else if (BaseInput.InputStatus == InputStatus.Dialogue) {
            if (!uiController.NextDialogueLine()) {
                BaseInput.ChangeInputStatus(InputStatus.Idle);
            }
        }
    }

    private IEnumerator AxelMoving() {        
        axel.Animator.SetBool("Walking", true);
        axel.CurrentInteractable = null;
        uiController.InteractableUIOff();
        if (dest.x < axel.transform.position.x) {
            axel.SpriteRenderer.flipX = false;
        } else if (dest.x > axel.transform.position.x) {
            axel.SpriteRenderer.flipX = true;
        }
        while (true) {
            if (axel.transform.position == dest) {
                BaseInput.ChangeInputStatus(InputStatus.Idle);
                axel.Animator.SetBool("Walking", false);
                CheckInteractable();
                yield break;
            }
            axel.transform.position = Vector3.MoveTowards(axel.transform.position, dest, Time.deltaTime * 5f);
            yield return null;
        }
    }

    private void CheckInteractable() {
        if (axel.transform.position.x >= 44f) {
            baseInput.ForcedInteraction = exitDungeon;
        } else if ( axel.transform.position.x <= 43.5f) {
            baseInput.ForcedInteraction = ramiInteraction;
        } else {
            baseInput.ForcedInteraction = null;
        }
            
        if (baseInput.ForcedInteraction) {
            StaticManager.InteractableUI.transform.position = axel.transform.position + new Vector3(0, 7, 0);

            uiController.ChangeInteractableUISprite(baseInput.ForcedInteraction.InteractionSprite);
            uiController.InteractableUIOn();
        } else {
            uiController.InteractableUIOff();
        }
    }

    //public void OnInteract(InputValue value) {
    //    if (BaseInput.InputStatus == InputStatus.Idle && targetInteraction != null) {
    //        targetInteraction.Interact(targetInteraction.name);
    //        if (targetInteraction is DialogueInteraction) {
    //            BaseInput.ChangeInputStatus(InputStatus.Dialogue);
    //        }
    //    } else if (BaseInput.InputStatus == InputStatus.Dialogue) {
    //        if (!uiController.NextDialogueLine()) {
    //            BaseInput.ChangeInputStatus(InputStatus.Idle);
    //        }
    //    }
    //}
}
