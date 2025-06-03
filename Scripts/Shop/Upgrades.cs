using NUnit.Framework;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrades", menuName = "Scriptable Objects/Upgrades")]
public class Upgrades : SerializedScriptableObject {
    [OdinSerialize, ShowInInspector]
    private readonly List<(string, int)> upgradeList = new();
}
