using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandObserver : BaseObserver
{
    [SerializeField]
    GameObject dialoguebubble;
    public void GiveShovel()
    {
        GiveItem(InventoryItem.Shovel);
    }
    public void IntroDialogue()
    {
        AddNode(dialoguebubble);
    }
}
