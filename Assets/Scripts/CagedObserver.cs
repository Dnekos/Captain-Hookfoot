using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagedObserver : BaseObserver
{
    [SerializeField]
    Sprite openCage;
    [SerializeField]
    GameObject anchor;

    public void FreePoe(SpriteRenderer cage)
    {
        OpenDialogue(8);
        cage.sprite = openCage;
        GiveItem(InventoryItem.Poe);

        Player.instance.CreateButNotModifyState(NodeIDs.Crew, 1);
        Player.instance.CreateButNotModifyState(NodeIDs.Murphy, 1);
    }

    public void MoveAnchor(GameObject obj)
    {
        disableObject(obj);
        SoundManager.PlaySound(Sound.Anchor);
    }
}
