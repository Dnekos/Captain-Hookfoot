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
    public void GiveCandle()
    {
        GiveItem(InventoryItem.Candle);
    }
    public void TalkToPLP(GameObject Pete)
    {
        disableObject(Pete);
        RemoveItem(InventoryItem.Key);
    }
}
