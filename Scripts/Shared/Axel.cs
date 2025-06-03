using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Axel : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Character character;

    public Character Character => character;
    [SerializeField]
    private PathfindingObject pfObj, targetObj;
    [SerializeField]
    private AStarPathfinding pathfinding;
    [SerializeField]
    private DialogueSystem dialogueSystem;
    [SerializeField]
    private GameObject interactableUI;
    [SerializeField]
    private float speed = 0.4f;
    [SerializeField]
    private MainGameManager mainGameManager;
    [SerializeField, HideInInspector]
    private PathfindingMap map;

    private void Start() {
        StaticManager.Axel = this;
    }

    private void OnEnable() {
        map = mainGameManager.ManagerController.ShopStatusManager.PathfindingMap;
    }

    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }
    public Animator Animator { get { return animator; } }


    [DoNotSerialize]
    public PathfindingObject CurrentInteractable;
    [HideInInspector]
    public Vector3 mouseDest = Vector3.zero;
    [HideInInspector]
    public Vector2 mv = Vector2.zero;
    [HideInInspector]
    public bool moving = false, groping = false;
    [SerializeField]
    private ClampedCamera shopCamera;
    private bool shopCameraActive = true;
    public void ShopCameraActive(bool active) {
        if (!active) {
            Camera.main.transform.position = new Vector3(-2, 0, -10);
        }
        shopCameraActive = active;
    }

    private void Update() {
        if (shopCameraActive) {
            shopCamera.MoveCameraTo(new Vector3(transform.position.x - 2, Camera.main.transform.position.y, Camera.main.transform.position.z), groping);
            if (mainGameManager.CurrentScene == MainGameManager.LoadedScene.Shop) {
                CheckInteractable();
            }
            
        }
    }

    private void OnDestroy() {
        Debug.LogError("Axel Destroyed");
    }
    private Vector3 GetNewPosition() {
        return new Vector3(transform.position.x + mv.x, transform.position.y + mv.y, transform.position.z);
    }
    private void FixedUpdate() {
        if (mouseDest != Vector3.zero) {
            if (!moving) {
                moving = true;
                StartCoroutine(MoveAxelToMouseClick());
                animator.SetBool("Walking", true);
            }
        } else if (mv != Vector2.zero) {
            MoveAxel();
            moving = true;
            animator.SetBool("Walking", true);
        } else {
            if (BaseInput.InputStatus == InputStatus.DungeonMoving) {
                return;
            }
            moving = false;
            animator.SetBool("Walking", false);
        }
    }

    public void ResetPosition() {
        transform.position = new Vector3(0, -5.25f, 0);
    }

    private void CheckInteractable() {
        targetObj = null;
        Collider2D topCollider = Physics2D.Raycast(new Vector2(spriteRenderer.flipX ? pfObj.Collider2D.bounds.max.x : pfObj.Collider2D.bounds.min.x, pfObj.Collider2D.bounds.max.y), spriteRenderer.flipX ? Vector3.right : Vector3.left, 2f).collider;
        Collider2D bottomCollider;
        if (topCollider == null) {
            bottomCollider = Physics2D.Raycast(new Vector2(spriteRenderer.flipX ? pfObj.Collider2D.bounds.max.x : pfObj.Collider2D.bounds.min.x, pfObj.Collider2D.bounds.min.y), spriteRenderer.flipX ? Vector3.right : Vector3.left, 2f).collider;
            if (bottomCollider != null) {
                targetObj = bottomCollider.GetComponent<PathfindingObject>();
            }
        } else {
            targetObj = topCollider.GetComponent<PathfindingObject>();
        }
        if (targetObj != null && targetObj.GetInteraction() != null) {
            CurrentInteractable = targetObj;
            if (CurrentInteractable == null) {
                interactableUI.SetActive(false);
                return;
            }
            interactableUI.transform.position = targetObj.transform.position + new Vector3(0, -0.5f, 0);
            interactableUI.GetComponentInChildren<Image>().sprite = CurrentInteractable.GetInteraction().InteractionSprite;
            interactableUI.SetActive(true);
        } else {
            CurrentInteractable = null;
            interactableUI.SetActive(false);
        }
    }

    public IEnumerator MoveAxelToMouseClick() {
        ////Vector2 roundedDestination = StaticMath.RoundToMult(mouseDest, 0.0625f);
        ////mouseDest = roundedDestination;
        Vector2 roundedDestination = mouseDest;
        //if (roundedDestination.y + 1 > map.TileDistances.RTIB.y) {
        //    roundedDestination.y = map.TileDistances.RTIB.y - 1;
        //}
        //if (!map.IsInsideBounds(pfObj.Collider2D.bounds, roundedDestination)) {
        //    yield break;
        //}
        if (roundedDestination.x - pfObj.Collider2D.bounds.extents.x < map.TileDistances.SXLC) {
            roundedDestination.x = map.TileDistances.SXLC + pfObj.Collider2D.bounds.extents.x;
        } else if (roundedDestination.x + pfObj.Collider2D.bounds.extents.x > map.TileDistances.RTIB.x) {
            roundedDestination.x = map.TileDistances.RTIB.x - pfObj.Collider2D.bounds.extents.x;
        }

        List<Vector2> points = pathfinding.StartSearch(transform.position, roundedDestination, pfObj, 1);
        if (points == null) {
            mouseDest = Vector3.zero;
            roundedDestination = mouseDest;
            moving = false;
            yield break;
        }

        if (roundedDestination.x < transform.position.x && spriteRenderer.flipX) {
            spriteRenderer.flipX = false;
        } else if (roundedDestination.x > transform.position.x && !spriteRenderer.flipX) {
            spriteRenderer.flipX = true;
        }
        while (true) {
            Vector2 dest;
            if (points.Count > 0) {
                dest = points[0];
                if (Vector2.Distance(pfObj.transform.position, dest) < 0.01f) {
                    points.RemoveAt(0);
                }
            } else {
                dest = roundedDestination;
                moving = false;
                mouseDest = Vector2.zero;
                yield break;
            }
            if (Vector2.Distance(pfObj.transform.position, roundedDestination) < 0.01f || !moving) {
                moving = false;
                transform.position = StaticMath.RoundToMult(transform.position, 0.0625f);
                mouseDest = Vector2.zero;
                yield break;
            }
            pfObj.WalkTowards(dest, map, speed * Time.deltaTime);
            pfObj.UpdateSortingOrder(map, map.TileDistances);
            yield return new WaitForEndOfFrame();
        }
    }

    private void MoveAxel() {
        //if (GetNewPosition().y >= -5.625f && GetNewPosition().y <= -1.25f && GetNewPosition().x >= -18.375 && GetNewPosition().x <= 18.1875f) {            
        //    Vector3Int dest = map.GetCellAtPos(GetNewPosition());
        //    if (dest != pfObj.transform.position) {
        //        if (map[dest] != null) {
        //            return;
        //        } else {
        //            map.MoveEntity(pfObj, dest);
        //            pfObj.SetCell(dest);
        //            Debug.Log(dest.x.ToString() + "," + dest.y.ToString());
        //        }
        //    }
        //    transform.Translate(mv);
        //    //rb.MovePosition(GetNewPosition());
        //}
        //if (mv.x < 0 && axelSprite.flipX) {
        //    axelSprite.flipX = false;
        //    //transform.Translate(-1, 0, 0);
        //} else if (mv.x > 0 && !axelSprite.flipX) {
        //    axelSprite.flipX = true;
        //    //transform.Translate(1, 0, 0);
        //}
    }
}
