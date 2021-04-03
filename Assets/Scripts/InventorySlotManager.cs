using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotManager : ClickableNode
{
    public int index = -1;
    public override void Interact(Actions action, InventoryItem item)
    {
        Debug.Log("clicked");
        if(Player.instance.GetAction() == Actions.Interact)
            Player.instance.SetAction(Actions.UseItem, index);
        else if (Player.instance.GetAction() == Actions.UseItem)
        {
            // CHECK IF THEY ARE COMPATIBLE, PERHAPS USE A DATABASE
        }

    }
    public void OnInventoryLook()
    {

    }
}
