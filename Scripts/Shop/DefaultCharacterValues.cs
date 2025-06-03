using UnityEngine;

[CreateAssetMenu(fileName = "DefaultCharacterValues", menuName = "Scriptable Objects/DefaultCharacterValues")]
public class DefaultCharacterValues : ScriptableObject {
    [SerializeField]
    private float speed = 0.2f;
    public float Speed => speed;
}
