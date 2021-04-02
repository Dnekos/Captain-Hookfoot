using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableNode : MonoBehaviour, IPointerClickHandler
{
    public virtual void Interact(Actions action, InventoryItem item)
    {
        Debug.Log("clicked"); // unused definition as this is the base class
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            LookAt(); // TEMP: right click looks

        // interact, using the subclasses definitions
        Interact(Player.instance.GetAction(), Player.instance.GetHeldItem());

        // reset actions to default
        if (Player.instance.GetAction() != Actions.Interact)
            Player.instance.SetAction(Actions.Interact);
    }
    public virtual void LookAt()
    {
    //will run the look functions 
    //NOTE: Prob should put this in another base class that manages Databasing, so that InventorySlotManager can utilize it despite not needing IpointerClickHandler stuff
    }
}
