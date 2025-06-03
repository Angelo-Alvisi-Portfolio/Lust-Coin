using UnityEngine;

public class SexualInteractable : MonoBehaviour {
    [SerializeField]
    private int characterNumber = 0;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private NPCAI npcAI;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public int CharacterNumber => characterNumber;    
    public void SetGroping(bool groping) {
        animator.SetBool("Groping", groping);
        if (groping) {
            animator.SetBool("Walking", false);
            spriteRenderer.flipX = false;
        }
    }
    public void PausePatrol() {
        npcAI.PausePatrol();
    }
    public void ResumePatrol() {
        npcAI.ResumePatrol();
    }
}
