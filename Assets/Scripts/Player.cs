using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player instance = null;
    public List<InventoryItems> inv;


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
}
