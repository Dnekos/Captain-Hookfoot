using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleObserver : BaseObserver
{
    [SerializeField]
    GameObject dialoguebubble, murph;
    [SerializeField]
    Sprite newwell;
    public void GiveShovel(SpriteRenderer well)
    {
        GiveItem(InventoryItem.Shovel);
        well.sprite = newwell;
        OpenDialogue(4);
        murph.SetActive(true);
    }
    public void GiveKey()
    {
        GiveItem(InventoryItem.Key);
        RemoveItem(InventoryItem.Note);
    }
    public void IntroDialogue()
    {
        AddNode(dialoguebubble);
    }
    public void TalkToCrew(GameObject crew)
    {
        disableObject(crew);
        OpenDialogue(12); // meet up with crew
        RemoveItem(InventoryItem.Poe);
        GiveItem(InventoryItem.Crew);
    }
}
