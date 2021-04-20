using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObserver : MonoBehaviour
{
    public void AddNode(GameObject node)
    {
        Instantiate(node);
    }
    protected void ChangeSprite(Sprite newimage, SpriteRenderer changed_component)
    {
        changed_component.sprite = newimage;
    }
    public  void disableObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    protected void GiveItem(InventoryItem item)
    {
        GameObject.Find("InventoryMenu").GetComponent<UIManager>().AddInventoryImage(item); // create UI object
    }
    protected void RemoveItem(InventoryItem item)
    {
        Player.instance.RemoveInvItem(item);
    }
    public void OpenDialogue(int index)
    {
        GameObject.Find("InventoryMenu").GetComponent<UIManager>().StartDialogue(index);
    }
}
