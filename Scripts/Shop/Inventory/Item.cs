using System;
using UnityEngine;

[Serializable]
public abstract class Item {

    [SerializeField]
    protected string name;
    public string Name => name;
    [SerializeField]
    protected int value;
    public int Value => value;
    [SerializeField]
    protected Sprite sprite;
    public Sprite Sprite => sprite;

    public Item (string name, int value, Sprite sprite) {
        this.name = name;
        this.value = value;
        this.sprite = sprite;
    }
}
