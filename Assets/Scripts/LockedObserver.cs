using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedObserver : BaseObserver
{
    [SerializeField]
    SpriteRenderer candle;
    public void PlaceCandle()
    {
        candle.enabled = true;
    }
    public void GiveGlass()
    {
        GiveItem(InventoryItem.MagnifyingGlass);
    }
    public void TalkToBoomstick(GameObject BS)
    {
        //COnversationhere STartDialogue(2);
        disableObject(BS);
        RemoveItem(InventoryItem.Key);
    }
}
