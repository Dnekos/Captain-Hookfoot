using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryItem
{
    None = -1,
    Key = 1,
    Rope,
    Shovel,
    MagnifyingGlass,
    Candle,
    Note,
    Poison,
    Bucket,
    Acid,
    SomewhatFilledBucket,
    FilledBucket,
    Bottle1,
    Bottle2,
    Bottle3,
    Bottle4,
    Poe
};

public class PickupNode : ClickableNode
{
    [SerializeField]
    InventoryItem data;

    private void Start()
    {
        if (Player.instance.GetState(UID) == 1)
            Destroy(gameObject);
    }

    override public void Interact(InventoryItem item)
    {
        base.Interact(item); // do dialogue
        Player.instance.LogState(UID, 1); // log that item was picked up
        Debug.LogError(data);
        GameObject.Find("InventoryMenu").GetComponent<UIManager>().AddInventoryImage(data); // create UI object
        Destroy(gameObject);
    }
    public override void LookAt()
    {
        base.LookAt(); // run look dialogue
    }
}
