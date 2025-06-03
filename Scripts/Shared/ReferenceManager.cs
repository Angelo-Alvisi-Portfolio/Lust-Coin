using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ReferenceManager", menuName = "Scriptable Objects/ReferenceManager")]
public class ReferenceManager : SerializedScriptableObject {
    [SerializeField]
    private ShopStatusManager shopStatusManager;
    public ShopStatusManager ShopStatusManager => shopStatusManager;
    
    public void SetSSM(ShopStatusManager ssm) {
        shopStatusManager = ssm;
    }

    
}
