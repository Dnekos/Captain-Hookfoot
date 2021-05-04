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
    Candle,
    CageRoomToWell,
    Poe,
    Acid,
    Poison,
    Bucket,
    CaptainsQuarters,
    HoleToCageRoom,
    Cage,
    Murphy,
    Crew
}

public class ClickableNode : Databaser, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{   
    [Header("Clickable")]
    [SerializeField]
    protected NodeIDs UID;
    [SerializeField]
    protected int state = 0;

    //sizing
    [SerializeField]
    bool EnlargeOnHover = true;
    float sizedelta = 1.05f;
    
    public virtual void LookAt()
    {
        DisplayThought(FetchTextByID((int)UID, "LookDialogue", "NodeDialogue", state));
    }

    /// <param name="item">when None, gets AdvanceState, else gets the default use text for the item</param>
    public virtual void Interact(InventoryItem item)
    {
        if (item == InventoryItem.None)
            DisplayThought(FetchTextByID((int)UID, "AdvanceStateDialogue", "NodeDialogue", state));
        else
            DisplayThought(FetchTextByID((int)item, "DefaultUse", "InventoryDialogue"));
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (EnlargeOnHover)
        {
            transform.localScale *= sizedelta;
            SoundManager.PlaySound(Sound.ButtonHover);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (EnlargeOnHover)
            transform.localScale /= sizedelta;
    }

    protected void DisplayThought(string text)
    {
        GameObject.Find("debug").GetComponent<UnityEngine.UI.Text>().text = text;
    }
}
