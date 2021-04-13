//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public enum Locations
{
    WellRoom,
    Island,
    ship,
    lockedRoom
}

public class LocationNode : ClickableNode
{
    [SerializeField]
    Locations Destination;

    override public void Interact(InventoryItem item)
    {
        SceneManager.LoadScene((int)Destination);
    }
    public override void LookAt()
    {
        base.LookAt();
    }
}
