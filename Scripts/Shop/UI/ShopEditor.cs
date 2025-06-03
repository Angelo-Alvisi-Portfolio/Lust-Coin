using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShopEditor : MonoBehaviour {
    [SerializeField, HideInInspector]
    private readonly List<Furniture> storedFurniture = new();
    private int index = -1;
    [SerializeField]
    private SpriteRenderer foregroundRenderer;
    [SerializeField]
    private GameObject furnitureShelf;
    [SerializeField, HideInInspector]
    private int filledSlots = 0;

    public void StoreFurniture(Furniture furniture) {        
        storedFurniture.Add(furniture);
        furniture.transform.SetParent(furnitureShelf.transform);
        int x = -5 + (filledSlots*2) + ((furniture.Size.x - 1) * 2);        
        furniture.transform.localPosition = new Vector3(x, 1, 0);
    }

    public Furniture TakeOutFurniture() {
        Furniture f = storedFurniture[index];
        storedFurniture.RemoveAt(index);
        return f;
    }

    public void Move(Vector3 dest) {
        transform.position = dest;        
    }

    public void SwitchFurniture(int n) {
        if (n > 0) {
            if (index == storedFurniture.Count - 1) {
                return;
            }
        }
        if (n < 0) {
            if (index == -1) {
                return;
            }
        }
        index += n;
        if (index == -1) {
            foregroundRenderer.sprite = null;
        } else {
            foregroundRenderer.sprite = storedFurniture[index].SpriteRenderer.sprite;
        }
    }

    public bool TryStore(Furniture f) {
        if (index != -1) {
            return false;
        } else {
            if (8 < filledSlots + f.Size.x) {
                return false;
            }
            StoreFurniture(f);
            return true;
        }
    }

    public Furniture TryDeploy() {
        if (index == -1) {
            return null;
        } else {
            return TakeOutFurniture();
        }

    }
}
