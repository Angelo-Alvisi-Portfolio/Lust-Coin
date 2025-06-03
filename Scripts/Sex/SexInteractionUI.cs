using UnityEngine;

public class SexInteractionUI : MonoBehaviour {
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer sprenderer;
    public void SetAnimation(int character, int animation) {
        animator.SetInteger("Character", character);
        animator.SetInteger("Animation", animation);
    }

    public void ResetUI() {
        animator.SetInteger("Character", 0);
        animator.SetInteger("Animation", 0);
        sprenderer.sprite = null;
        gameObject.SetActive(false);
    }

}
