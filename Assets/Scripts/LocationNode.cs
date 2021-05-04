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
        BlackoutScript.Transition((int)Destination);
    }
    public override void LookAt()
    {
        base.LookAt();
    }
}
