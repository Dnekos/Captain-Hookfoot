using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockManager : MonoBehaviour
{
    [SerializeField]
    RotaryLock[] locks;
    //int[] combination = { 1, 2, 3, 4 };
    [SerializeField]
    GameObject display_desk;
    [SerializeField]
    DisplayNode Main;

    bool locked = true;

    // Update is called once per frame
    void Update()
    {
        if (!locks[0].isActiveAndEnabled || !locked)//.gameObject.activeInHierarchy)
            return; 

        for (int i  = 0;  i < 4; i++)
        {
            if (locks[i].value != i)
                return;
        }

        // results for completion
        Destroy(display_desk);
        GameObject.Find("debug").GetComponent<UnityEngine.UI.Text>().text = "I think I got it! There's a magnifying glass in the desk!";
        GameObject.Find("InventoryMenu").GetComponent<UIManager>().AddInventoryImage(InventoryItem.MagnifyingGlass);

        // locking the buttons
        for (int i = 0; i < 4; i++)
            locks[i].GetComponent<Button>().enabled = false;
        locked = false;
    }
}
