using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Desires", menuName = "Scriptable Objects/Desire")]
public class Desires : SerializedScriptableObject {
    [OdinSerialize]
    private Dictionary<Item, int> desires = new Dictionary<Item, int>();

    public int GetDesire(Item item) {
        if (desires.ContainsKey(item)) {            
            return desires[item];
        } else {
            return 1;
        }        
    }
}
