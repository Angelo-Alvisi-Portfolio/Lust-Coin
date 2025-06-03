using Sirenix.OdinInspector;
using UnityEngine;

public abstract class GameInteraction : SerializedScriptableObject {

    [SerializeField]
    private Sprite interactionSprite;
    public Sprite InteractionSprite {
        get {
            if (interactionSprite != null) {
                return interactionSprite;
            } else {
                Debug.LogError("No sprite asset assigned.");
                return null;
            }
        }        
    }
    public abstract void Interact(string interactableName);

}
