using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotaryLock : ClickableNode
{
    [SerializeField]
    Text textbox;
    public int value = 0;
    override public void Interact(InventoryItem item)
    {
        value = (value + 1) % 10;
        textbox.text = value.ToString();
    }
    public override void LookAt()
    {
        //base.LookAt();
    }

}
