using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryItems
{
    None = -1,
    Key
};

public class PickupNode : ClickableNode
{
    [SerializeField]
    InventoryItems data;

    override public void Interact(Actions action, InventoryItems item)
    {
        if (action == Actions.Interact)
        {
            Player.instance.AddInvItem(data);
            Destroy(gameObject);
        }
    }
}
