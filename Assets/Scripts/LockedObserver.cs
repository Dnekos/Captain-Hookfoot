using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedObserver : BaseObserver
{
    [SerializeField]
    SpriteRenderer candle,background;
    [SerializeField]
    GameObject doorlock, doortobasement,LightBeam;
    [SerializeField]
    Sprite newbackground;
    [SerializeField]
    CameraShake cam;
    public void PlaceCandle()
    {
        candle.enabled = true;
    }
    public void BlowUpCandle()
    {
        candle.enabled = false;
        background.sprite = newbackground;
        doortobasement.SetActive(true);
        LightBeam.SetActive(false);
        cam.TriggerShake();
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
