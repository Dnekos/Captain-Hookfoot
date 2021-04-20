using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleObserver : BaseObserver
{
    [SerializeField]
    GameObject dialoguebubble;
    [SerializeField]
    Sprite newwell;
    public void GiveShovel(SpriteRenderer well)
    {
        GiveItem(InventoryItem.Shovel);
        well.sprite = newwell;
    }
    public void GiveKey()
    {
        GiveItem(InventoryItem.Key);
        RemoveItem(InventoryItem.Note);
    }
    public void IntroDialogue()
    {
        AddNode(dialoguebubble);
    }
}
