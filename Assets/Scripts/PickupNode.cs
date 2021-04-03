using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryItem
{
    None = -1,
    Key
};

public class PickupNode : ClickableNode
{
    [SerializeField]
    InventoryItem data;

    override public void Interact(InventoryItem item)
    {
        base.Interact(item); // do dialogue
        Player.instance.AddInvItem(data);
        Destroy(gameObject);
    }
}
