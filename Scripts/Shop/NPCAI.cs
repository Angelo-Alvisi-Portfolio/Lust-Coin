using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PatrolNodes;

public class NPCAI : MonoBehaviour {


    private int pathNode = 0;
    [SerializeField]
    private int cellsPatrol = 2, increment = 16;
    [SerializeField]
    private TileDistances tileDistances;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private DefaultCharacterValues defaultCharacterValues;
    [SerializeField]
    private PatrolNodes patrolNodes;
    [SerializeField]
    private PathfindingMap pathfindingMap;
    [SerializeField]
    private PathfindingObject pfObj;
    private bool patroling = true;


    private void Start() {
        if (patrolNodes == null) {
            StartCoroutine(StandardPatrolPath());
        } else {
            StartCoroutine(ScriptedPatrolPath());
        }
    }

    private IEnumerator ScriptedPatrolPath() {
        int nodeN = 0;
        List<Vector3> patrolNodes = new();
        for (int i = 0; i < this.patrolNodes.Nodes.Count; i++) {
            PatrolNode node = this.patrolNodes.Nodes[i];
            //WaitPatrolNode and Node is 0 add in place else, add the Node before this one.
            if (node is WaitPatrolNode) {
                if (i == 0) {
                    patrolNodes.Add(pfObj.transform.position);
                } else {
                    patrolNodes.Add(new Vector3(patrolNodes[i - 1].x, patrolNodes[i - 1].y, patrolNodes[i - 1].z));
                }
            } else if (node is DirectionPatrolNode DPN) {
                Vector3 pos;
                if (i == 0) {
                    pos = pfObj.transform.position;
                } else {
                    pos = patrolNodes[i - 1];
                }
                patrolNodes.Add(new Vector3(pos.x + (DPN.direction.x * DPN.distance * increment * 0.0625f), pos.y + (DPN.direction.y * DPN.distance * increment * 0.0625f), pos.z));
            } else if (node is GotoPatrolNode GPN) {
                ///CHECK : THIS IS PROBABLY WRONG, NEED TO CHECK IT OUT
                patrolNodes.Add(GPN.cell);
            }
        }
        while (true) {
            if (patroling) {
                if (nodeN == this.patrolNodes.Nodes.Count) {
                    nodeN = 0;
                }
                PatrolNode node = this.patrolNodes.Nodes[nodeN];
                if (node is WaitPatrolNode WPN) {
                    animator.SetBool("Walking", false);
                    yield return new WaitForSeconds(WPN.seconds);
                    nodeN++;
                } else if (node is DirectionPatrolNode || node is GotoPatrolNode) {
                    animator.SetBool("Walking", true);
                    if (pfObj.transform.position.x > patrolNodes[nodeN].x) {
                        spriteRenderer.flipX = true;
                    } else {
                        spriteRenderer.flipX = false;
                    }
                    //transform.position = Vector3.MoveTowards(transform.position, patrolNodes[nodeN], 0.0625f * speed);
                    pfObj.WalkTowards(patrolNodes[nodeN], pathfindingMap, defaultCharacterValues.Speed * Time.deltaTime);
                    yield return null;
                    if (pfObj.transform.position == patrolNodes[nodeN]) {
                        nodeN++;
                    }
                }
            } else {
                yield return null;
            }
        }
    }
    private IEnumerator StandardPatrolPath() {
        Vector3 leftPosition = transform.position;
        Vector3 rightPosition = transform.position + new Vector3(tileDistances.BDX * cellsPatrol, 0, 0);
        while (true) {
            if (patroling) {
                animator.SetBool("Walking", true);
                if (pathNode == 0) {
                    transform.position = Vector3.MoveTowards(transform.position, rightPosition, defaultCharacterValues.Speed * Time.deltaTime * 0.0625f);
                    if (transform.position == rightPosition) {
                        pathNode = 1;
                        spriteRenderer.flipX = true;
                    }
                } else {
                    transform.position = Vector3.MoveTowards(transform.position, leftPosition, defaultCharacterValues.Speed * Time.deltaTime * 0.0625f);
                    if (transform.position == leftPosition) {
                        pathNode = 0;
                        spriteRenderer.flipX = false;
                    }
                }
                yield return null;
            } else {
                yield return null;
            }
        }
    }

    public void PausePatrol() {
        patroling = false;
    }

    public void ResumePatrol() {
        patroling = true;
    }

}

