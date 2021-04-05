using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryItem
{
    None = -1,
    Key,
    Rope
};

public class PickupNode : ClickableNode
{
    [SerializeField]
    InventoryItem data;

    override public void Interact(InventoryItem item)
    {
        UIManager.SetInventoryState(false);
        base.Interact(item); // do dialogue
        GameObject.Find("InventoryMenu").GetComponent<UIManager>().AddInventoryImage(data); // create UI object
        Destroy(gameObject);
    }
    public override void LookAt()
    {
        UIManager.SetInventoryState(false);
        base.LookAt();
    }
}
