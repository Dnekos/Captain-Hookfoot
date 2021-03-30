using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotManager : MonoBehaviour
{
    public int index = -1;
    public void OnInventoryClick()
    {
        Player.instance.SetAction(Actions.UseItem, index);
    }
}
