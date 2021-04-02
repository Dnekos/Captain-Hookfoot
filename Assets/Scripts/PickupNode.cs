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

    override public void Interact(Actions action, InventoryItem item)
    {
        switch (action)
        {
            case Actions.Interact:
                Player.instance.AddInvItem(data);
                Destroy(gameObject);
                break;
            case Actions.Look:
                LookAt();
                break;
            case Actions.UseItem:
                break;
        }
    }
}
