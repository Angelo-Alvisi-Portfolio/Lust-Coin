using JetBrains.Annotations;
using NUnit.Framework;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : SerializedMonoBehaviour {

    [OdinSerialize, ShowInInspector]
    private readonly List<ItemStorageSpace<Trinket>> trinketStorages = new();
    [OdinSerialize, ShowInInspector]
    private readonly List<ItemStorageSpace<RawMaterial>> rawMatStorages = new();
    [OdinSerialize, ShowInInspector]
    private readonly List<ItemStorageSpace<Consumable>> consumableStorages = new();
    [OdinSerialize, ShowInInspector]
    private readonly HashSet<NPCAI> npcs = new();
    [SerializeField]
    private List<Upgrades> allUpgradesList = new();
    [SerializeField]
    private List<Furniture> storedFurniture = new();
    [SerializeField]
    private Axel axel;
    private HashSet<NPCAI> todaysVisits;

    public List<Furniture> StoredFurniture => storedFurniture;

    public void AddItem(Item item) {
        if(item is Trinket trinket) {
            AddItem(trinket, trinketStorages);
        } else if (item is RawMaterial rawMat) {
            AddItem(rawMat, rawMatStorages);
        } else if (item is Consumable consumable) {
            AddItem(consumable, consumableStorages);
        }
    }

    public void AddItem<T>(T item, List<ItemStorageSpace<T>> storages) where T : Item {
        foreach (ItemStorageSpace<T> storage in storages) {
            if (!storage.IsFull) {
                storage.AddItem(item);
                return;
            }
        }
    }

    public void SetVisits(Axel axel) {
        int randomNPCVisits = Random.Range(1, 4);
        List<NPCAI> shuffledList = ShuffleFunctions.ShuffleList(npcs.ToList());
        todaysVisits = new();
        for (int i = 0; i < randomNPCVisits; i++) {
            todaysVisits.Add(shuffledList[i]);
            shuffledList[i].gameObject.SetActive(true);
        }
        axel.gameObject.SetActive(true);
    }

    public void ResetVisits(Axel axel) {
        foreach (NPCAI npc in todaysVisits) {
            npc.gameObject.SetActive(false);
        }
        axel.gameObject.SetActive(false);
    }

}
