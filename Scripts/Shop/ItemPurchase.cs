using UnityEngine;

public struct ItemPurchase {
    private NPCAI npc { get; }
    private Item item { get; }
    private float modifier { get; }

    public ItemPurchase(NPCAI npc, Item item, float modifier) {
        this.npc = npc;
        this.item = item;
        this.modifier = modifier;
    }
}
