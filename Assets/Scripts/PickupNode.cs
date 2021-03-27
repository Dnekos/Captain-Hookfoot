using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryItems
{
    Key
};

public class PickupNode : InteractableNode
{
    [SerializeField]
    InventoryItems data;

    override public void Interact()
    {
        Player.instance.AddInvItem(data);
        Destroy(gameObject);
    }
}
