using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyMovement : MonoBehaviour {

    [SerializeField]
    private float waitTime;
    [SerializeField]
    private Character character;
    private float dx;
    [SerializeField]
    private int speed;

    private bool moving = false;

    private void Update() {
        if (moving) {
            transform.Translate(Vector3.right* speed * 0.0625f * Time.deltaTime);
        }
    }

    public IEnumerator AttackCoroutine() {
        Character player = StaticManager.Axel.Character;
        dx = (transform.position.x - player.transform.position.x) * -1;
        moving = true;
        while (moving) {
            yield return new WaitForSeconds(waitTime);
            if (transform.position.x >= player.transform.position.x - 4) {
                moving = false;
                player.SetAttacking(true);
                character.SetAttacking(true);
                character.CombatSceneController.CombatBackground.moving = false;
            }
        }
        yield return null;
    }
}
