using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandObserver : BaseObserver
{
    [SerializeField]
    Sprite closedchest;
    [SerializeField]
    Sprite openchest;

    public void ShowChest(SpriteRenderer self)
    {
        self.sprite = closedchest;
    }
    public void OpenChest(SpriteRenderer self)
    {
        self.sprite = openchest;
        GiveItem(InventoryItem.Note);
    }

}
