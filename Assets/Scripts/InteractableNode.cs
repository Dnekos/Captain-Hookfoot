using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableNode : MonoBehaviour, IPointerClickHandler
{
    public virtual void Interact()
    {
        Debug.Log("clicked");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Interact();
    }
}
