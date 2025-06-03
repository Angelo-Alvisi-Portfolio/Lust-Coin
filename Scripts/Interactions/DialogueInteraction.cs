using NUnit.Framework;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueInteraction", menuName = "Scriptable Objects/DialogueInteraction")]
public class DialogueInteraction : GameInteraction {
    [SerializeField]
    private Sprite sprite;
    [SerializeField, OdinSerialize]
    private List<string[]> dialogues = new List<string[]>();

    public override void Interact(string interactableName) {
        DialogueSystem diagSystem = FindFirstObjectByType<DialogueSystem>(FindObjectsInactive.Include);
        diagSystem.SecondCharPortrait.sprite = sprite;
        if (dialogues != null && dialogues.Count > 0 && dialogues[0] != null) {
            diagSystem.LoadDialogue(dialogues[0]);
        } else {
            diagSystem.LoadDialogue(null);
        }
        diagSystem.gameObject.SetActive(true);
    }
}
