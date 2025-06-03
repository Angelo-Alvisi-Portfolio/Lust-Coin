using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopInput : MonoBehaviour {

    [SerializeField]
    private TileDistances tileDistances;
    [SerializeField]
    private Vector2Int bounds;
    [SerializeField]
    private PathfindingMap map;
    [SerializeField]
    private Calendar calendar;
    [SerializeField]
    private ManagerController managerController;
    [SerializeField]
    private UIController uiController;

    public void OnMove(Vector2 dir, Axel axel) {        
        if (BaseInput.InputStatus == InputStatus.Moving) {
            if (dir == Vector2.zero) {
                BaseInput.ChangeInputStatus(InputStatus.Idle);
            }
            axel.mv = dir * 0.0625f;
        } else if (BaseInput.InputStatus == InputStatus.Idle) {
            if (dir.x > 0) {
                dir.x = 1;
            } else if (dir.x < 0) {
                dir.x = -1;
            }
            if (dir.y > 0) {
                dir.y = 1;
            } else if (dir.y < 0) {
                dir.y = -1;
            }
            BaseInput.ChangeInputStatus(InputStatus.Moving);
            axel.mv = dir * 0.0625f;
        } // else if (inputStatus == InputStatus.Selecting) {
        //    Vector3Int start = map.GetCellAtPos(shopEditor.transform.position);
        //    Vector3Int end = start + new Vector3Int(Mathf.RoundToInt(dir.x), Mathf.RoundToInt(dir.y), 0);
        //    Vector3 dest = map.GetPosOfCell(end);
        //    shopEditor.Move(dest);

        //    //Vector2Int selDir = new(Mathf.RoundToInt(dir.x), Mathf.RoundToInt(dir.y));
        //    //selector.Move(selDir, bounds, tileDistances);           
        //}

    }

    public void OnNext(InputValue value) {
        return;
        if (BaseInput.InputStatus == InputStatus.Selecting) {
            if (!value.isPressed) {
                return;
            }
            //shopEditor.SwitchFurniture(1);
            BaseInput.ChangeInputStatus(InputStatus.LockedSelecting);
            StartCoroutine(ResetSelecting());
        }
    }

    public void OnPrevious(InputValue value) {
        return;
        if (BaseInput.InputStatus == InputStatus.Selecting) {
            if (!value.isPressed) {
                return;
            }
            //shopEditor.SwitchFurniture(-1);
            BaseInput.ChangeInputStatus(InputStatus.LockedSelecting);
            StartCoroutine(ResetSelecting());
        }
    }
    //public void OnSexInteract(Axel Axel) {
    //    if (BaseInput.InputStatus == InputStatus.Idle && Axel.CurrentInteractable != null) {
    //        if (Axel.CurrentInteractable.TryGetComponent(out SexualInteractable interactable)) {
    //            BaseInput.ChangeInputStatus(InputStatus.SexInteraction);
    //            Axel.Animator.SetBool("Groping", true);
    //            Axel.Animator.SetBool("Walking", false);
    //            currentSexInteractable = interactable;
    //            currentSexInteractable.PausePatrol();
    //            interactable.SetGroping(true);
    //            Axel.transform.position = new Vector3(currentSexInteractable.transform.position.x - 2f + 2.5f, currentSexInteractable.transform.position.y + 0.5f, 0);
    //            Axel.SpriteRenderer.sortingOrder = currentSexInteractable.SpriteRenderer.sortingOrder - 1;
    //            Axel.SpriteRenderer.flipX = false;
    //            Axel.groping = true;
    //            Axel.moving = false;
    //            Axel.mouseDest = Vector3.zero;
    //        }
    //    } else {
    //        return;
    //    }
    //}

    private IEnumerator ResetSelecting() {
        yield return new WaitForSecondsRealtime(0.2f);
        BaseInput.ChangeInputStatus(InputStatus.Selecting);
    }

    public void ClickOnShop(Vector3 pos, Axel axel, SexualInteractable currentSexInteractable) {
        if (BaseInput.InputStatus == InputStatus.Idle) {
            axel.mouseDest = pos;
            BaseInput.ChangeInputStatus(InputStatus.MouseMoving);
        } else if (BaseInput.InputStatus == InputStatus.Selecting) {
            Vector3Int v = map.GetCellAtPos(pos);
            Vector2 dest = map.GetPosOfCell(v);
            //shopEditor.transform.position = dest;
        } else if (BaseInput.InputStatus == InputStatus.MouseMoving) {
            axel.moving = false;
            BaseInput.ChangeInputStatus(InputStatus.Idle);
            axel.mouseDest = Vector3.zero;
        } else if (BaseInput.InputStatus == InputStatus.Dialogue) {
            if (!uiController.NextDialogueLine()) {
                BaseInput.ChangeInputStatus(InputStatus.Idle);
            }
        } else if (BaseInput.InputStatus == InputStatus.SexInteraction) {
            uiController.CloseSexInteractionUI();
            axel.Animator.SetBool("Groping", false);
            axel.Animator.SetBool("Walking", true);
            axel.groping = false;
            currentSexInteractable.SetGroping(false);
            currentSexInteractable.ResumePatrol();
            BaseInput.ChangeInputStatus(InputStatus.Idle);
        }
    }
    public void OnConfirm(InputValue value) {
        if (BaseInput.InputStatus != InputStatus.Selecting) {
            return;
        }
        //PathfindingObject obj = map[new Vector2(shopEditor.transform.position.x, shopEditor.transform.position.y)];
        //if (obj == null) {
        //    Furniture f = shopEditor.TryDeploy();
        //    if (f != null) {
        //        map.AddEntity(f, shopEditor.transform.position);
        //        return;
        //    }
        //}
        //if (obj is Furniture furniture) {
        //    shopEditor.TryStore(furniture);
        //} else {
        //    return;
        //}        
    }

    private IEnumerator WaitAndChangeInput(InputStatus status, float time) {
        yield return new WaitForSecondsRealtime(time);
        BaseInput.ChangeInputStatus(status);
    }

    public void OnEscape() {
        managerController.ShopStatusManager.EndNightSegment();
        calendar.SkipSection();

    }
}
