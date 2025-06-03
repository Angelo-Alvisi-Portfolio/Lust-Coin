using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "TransitionInteraction", menuName = "Scriptable Objects/TransitionInteraction")]
public class TransitionInteraction : GameInteraction {
    [SerializeField]
    private string scene;
    public override void Interact(string interactableName) {
        FindFirstObjectByType<MainGameManager>().LoadScene(scene);                       
    }
}
