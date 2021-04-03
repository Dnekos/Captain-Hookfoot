using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotManager : ClickableNode
{
    public int index = -1;
    public override void Interact(InventoryItem item)
    {
        Debug.Log("clicked");
        if(item == InventoryItem.None)
            Player.instance.SetAction(Actions.UseItem, index);
        else if (Player.instance.GetAction() == Actions.UseItem)
        {
            base.Interact(item);
            // CHECK IF THEY ARE COMPATIBLE, PERHAPS USE A DATABASE
        }
    }
}
