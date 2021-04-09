using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleObserver : BaseObserver
{
    [SerializeField]
    GameObject dialoguebubble;
    public void GiveShovel()
    {
        GiveItem(InventoryItem.Shovel);
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
