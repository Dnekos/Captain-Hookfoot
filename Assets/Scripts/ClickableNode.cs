using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableNode : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    protected int state = 0;
    int UID;

    public KeyValuePair<int,int> getStatePair()
    {
        return new KeyValuePair<int, int>(UID, state);
    }

    public virtual void Interact(InventoryItem item)
    {
        Debug.Log("clicked"); // get dialogue
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            LookAt(); // TEMP: right click looks
        else if (eventData.button == PointerEventData.InputButton.Left && Player.instance.GetHeldItem() == InventoryItem.None)
            Interact(Player.instance.GetHeldItem());
        else
        {
            Interact(Player.instance.GetHeldItem());
            // reset actions to default
            if (Player.instance.GetAction() != Actions.Interact)
                Player.instance.SetAction(Actions.Interact);
        }
    }
    public virtual void LookAt()
    {
    //will run the look functions 
    //NOTE: Prob should put this in another base class that manages Databasing, so that InventorySlotManager can utilize it despite not needing IpointerClickHandler stuff
    }
}
