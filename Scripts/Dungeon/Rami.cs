using System.Collections;
using UnityEngine;

public class Rami : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Animator animator;
    private bool walking;

    private void Awake() {
        StartCoroutine(RamiMoving());
    }

    private IEnumerator RamiMoving() {
        bool directionRight = false;
        while (true) {
            if (walking) {
                if (transform.localPosition.x == -4.75f) {
                    directionRight = true;
                    walking = false;
                    animator.SetBool("Walking", false); 
                    yield return new WaitForSeconds(3f);
                } else if (transform.localPosition.x == 0) {
                    directionRight = false;
                    walking = false;
                    animator.SetBool("Walking", false);
                    yield return new WaitForSeconds(3f);
                }
                if (directionRight) {
                    sprite.flipX = false;
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, transform.position.y, transform.position.z), Time.deltaTime * 5f);
                } else {
                    sprite.flipX = true;
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(-4.75f, transform.position.y, transform.position.z), Time.deltaTime * 5f);
                }
                yield return null;
            } else {
                animator.SetBool("Walking", true);
                walking = true;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
