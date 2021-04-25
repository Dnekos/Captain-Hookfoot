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
            Debug.Log("item is " + (int)invItem);
            Debug.Log(FetchTextByID((int)invItem, "MergeBucket", "InventoryDialogue") == "Y");
            if (FetchTextByID((int)item, "MergeBucket", "InventoryDialogue") == "Y" && (invItem == InventoryItem.Bucket || invItem == InventoryItem.SomewhatFilledBucket))
            {
                UIManager ui = GameObject.Find("InventoryMenu").GetComponent<UIManager>();
                ui.AddInventoryImage(InventoryItem.SomewhatFilledBucket); // create UI object
                ui.RemoveInventoryImage(item);
                ui.RemoveInventoryImage(InventoryItem.Bucket);

                if (ui.IncrementBucket())
                {
                    ui.RemoveInventoryImage(InventoryItem.SomewhatFilledBucket);
                    ui.AddInventoryImage(InventoryItem.FilledBucket); // create UI object
                }

            }
        }
    }
    public override void LookAt()
    {
        DisplayThought(FetchTextByID((int)invItem, "LookDialogue", "InventoryDialogue"));
    }
}
