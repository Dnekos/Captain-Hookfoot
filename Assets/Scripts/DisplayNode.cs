using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayNode : ClickableNode
{
    [SerializeField]
    Camera target;

    override public void Interact(InventoryItem item)
    {
        GameObject maincam = Camera.main.gameObject;
        //Camera.main.enabled = false;    

        Camera.main.gameObject.SetActive(false);

        target.gameObject.SetActive(true);
    }
    public override void LookAt()
    {
        base.LookAt();
    }
}