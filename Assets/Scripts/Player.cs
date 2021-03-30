using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player instance = null;
    List<InventoryItem> inv;
    [SerializeField]
    int heldIndex = -1;
    [SerializeField]
    Actions currentAction = Actions.Interact;

    private void Awake()
    {
        // create singletons
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        inv = new List<InventoryItem>();
        DontDestroyOnLoad(gameObject); // may be unnecessary? prob is with shifting rooms
    }
    public List<InventoryItem> GetInventory()
    {
        return inv;
    }

    public void AddInvItem(InventoryItem item)
    {
        GameObject.Find("InventoryMenu").GetComponent<UIManager>().AddInventoryImage(item, inv.Count); // create UI object
        inv.Add(item); // add item to inventory list
    }
    public void RemoveInvItem(InventoryItem item)
    {
        Debug.Log("Removing " + item + " from inventory");
        GameObject.Find("InventoryMenu").GetComponent<UIManager>().RemoveInventoryImage(heldIndex); // delete UI object

        if (item == inv[heldIndex]) // reset heldIndex (may be redundant?)
            heldIndex = -1;

        inv.Remove(item); // remove item from list
    }
    public InventoryItem GetHeldItem()
    {
        if (heldIndex == -1) // prevent errors if not holding an item
            return InventoryItem.None;
        return inv[heldIndex];
    }

    public Actions GetAction()
    {
        return currentAction;
    }
    public void SetAction(Actions newaction, int index = -1)
    {
        Debug.Log("Current action is now " + newaction);
        instance.heldIndex = index;
        instance.currentAction = newaction;
    }
}
