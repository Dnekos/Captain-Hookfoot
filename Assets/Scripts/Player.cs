using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player instance = null;
    List<InventoryItems> inv;
    int heldIndex = -1;

    Actions currentAction = Actions.Interact;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    public void AddInvItem(InventoryItems item)
    {
        inv.Add(item);
    }
    public InventoryItems GetHeldItem()
    {
        if (heldIndex == -1)
            return InventoryItems.None;
        return inv[heldIndex];
    }

    public Actions GetAction() // TODO:CHANGE ONCE OTHER ACTIONS IMPLEMENTED
    {
        if (heldIndex == -1)
            return Actions.Interact;
        return Actions.UseWithItem;
    }
}
