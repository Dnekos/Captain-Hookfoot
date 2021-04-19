using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedObserver : BaseObserver
{
    [SerializeField]
    SpriteRenderer candle;
    [SerializeField]
    GameObject doorlock;
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
        OpenDialogue(2);
        disableObject(BS);
        doorlock.SetActive(true);
        RemoveItem(InventoryItem.Key);
    }
}
