using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.EventSystems;

public enum NodeIDs
{
    Well = 1,
    Anchor,
    CaveToIsland,
    CaveToWell,
    Lock,
    Rope,
    Ship,
    Sand,
    Mat,
    LockedRoomDoor,
    Boomstick,
    MagnifyingGlass,
    Desk,
    Light,
    Bottle1,
    Bottle2,
    Bottle3,
    Bottle4,
    Candle
}

public class ClickableNode : Databaser, IPointerClickHandler
{   
    [SerializeField]
    protected NodeIDs UID;
    [SerializeField]
    protected int state = 0;
    
    public virtual void LookAt()
    {
        DisplayThought(FetchTextByID((int)UID, "LookDialogue"));
    }
    public virtual void Interact(InventoryItem item)
    {
        Debug.Log("clicked"); // get dialogue
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            LookAt(); // TEMP: right click looks
            Player.instance.SetHeldItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Left && Player.instance.GetHeldItem() == InventoryItem.None)
            Interact(Player.instance.GetHeldItem());
        else
        {
            Interact(Player.instance.GetHeldItem());
            Player.instance.SetHeldItem();
        }
    }

    protected void DisplayThought(string text)
    {
        GameObject.Find("debug").GetComponent<UnityEngine.UI.Text>().text = text;
    }
}
