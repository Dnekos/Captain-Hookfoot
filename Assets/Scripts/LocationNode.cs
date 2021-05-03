//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public enum Locations
{
    WellRoom = 3,
    Island,
    ship,
    lockedRoom,
    CageRoom
}

public class LocationNode : ClickableNode
{
    [SerializeField]
    Locations Destination;

    override public void Interact(InventoryItem item)
    {
        base.Interact(item); // do dialogue
        if (item == InventoryItem.None)
            SceneManager.LoadScene((int)Destination);
    }
    public override void LookAt()
    {
        base.LookAt();
    }
}
