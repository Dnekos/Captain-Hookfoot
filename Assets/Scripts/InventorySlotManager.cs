using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotManager : ClickableNode
{
    public InventoryItem invItem = InventoryItem.None;
    public override void Interact(InventoryItem item)
    {
        Debug.Log("clicked");
        if (item == InventoryItem.None)
        {
            Player.instance.SetHeldItem(invItem);
            GameObject.Find("MouseInv").GetComponent<MouseFollow>().setImage(invItem); // set mouse Image
        }
        else
        {
            base.Interact(item);

            // CHECK IF THEY ARE COMPATIBLE, PERHAPS USE A DATABASE
        }
    }
    public override void LookAt()
    {
        DisplayThought(FetchTextByID((int)invItem, "LookDialogue", "InventoryDialogue"));
    }
}
