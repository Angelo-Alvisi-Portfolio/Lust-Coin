using NUnit.Framework;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorageSpace<T> where T : Item {
    [OdinSerialize, ShowInInspector]
    private readonly List<T> storage = new();
    [SerializeField]
    private int maxStorageSize;

    public bool IsFull => storage.Count >= maxStorageSize;
    public string AddItem(T item) {
        if (IsFull) {
            return "Container is Full.";
        }
        storage.Add(item);
        return $"{item.Name} added to storage.";
    }
}
