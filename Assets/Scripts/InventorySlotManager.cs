using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotManager : ClickableNode
{
    public InventoryItem invItem = InventoryItem.None;
    public override void Interact(InventoryItem item)
    {
        Debug.Log("clicked");
        if (item == InventoryItem.None) // if equipping the item
        {
            Player.instance.SetHeldItem(invItem); // set item as active
            GameObject.Find("MouseInv").GetComponent<MouseFollow>().setImage(invItem); // set mouse Image
        }
        else // if using item on slot
        {
            base.Interact(item); // display default use item text

            string buckettext = FetchTextByID((int)item, "MergeBucket", "InventoryDialogue"); // query table
            // if the item is held item is mergable AND the slot being merged with is one of the buckets
            if ((buckettext != "" || buckettext != null) && (invItem == InventoryItem.Bucket || invItem == InventoryItem.SomewhatFilledBucket))
            {
                UIManager ui = GameObject.Find("InventoryMenu").GetComponent<UIManager>();
                ui.AddInventoryImage(InventoryItem.SomewhatFilledBucket); // ensure the somewhatfilled bucket is displayed
                ui.RemoveInventoryImage(item); // remove item from inventory
                ui.RemoveInventoryImage(InventoryItem.Bucket); // check to see if bucket is removed

                DisplayThought(buckettext); // overright the base interact with merge text
                SoundManager.PlaySound(Sound.ItemCombining); // give feedback

                if (ui.IncrementBucket()) // if the final bucket item is used, replace somewhat filled bucket with fully filled
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
