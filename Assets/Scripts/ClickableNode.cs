using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableNode : MonoBehaviour, IPointerClickHandler
{
    public virtual void Interact(Actions action, InventoryItems item)
    {
        Debug.Log("clicked");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Interact(Player.instance.GetAction(), Player.instance.GetHeldItem());
    }
}
