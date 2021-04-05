using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObserver : MonoBehaviour
{
    public virtual void AddNode(GameObject node)
    {
        Instantiate(node);
    }
    public virtual void ChangeSprite(Sprite newimage, SpriteRenderer changed_component)
    {
        changed_component.sprite = newimage;
    }
    public virtual void disableObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void GiveItem(InventoryItem item)
    {
        GameObject.Find("InventoryMenu").GetComponent<UIManager>().AddInventoryImage(item); // create UI object
    }
}
