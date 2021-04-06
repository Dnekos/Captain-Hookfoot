//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public enum Locations
{
    WellRoom,
    Island
}

public class LocationNode : ClickableNode
{
    [SerializeField]
    Locations Destination;

    override public void Interact(InventoryItem item)
    {
        UIManager.SetInventoryState(false);
        SceneManager.LoadScene((int)Destination);
    }
    public override void LookAt()
    {
        UIManager.SetInventoryState(false);
        base.LookAt();
    }
}
