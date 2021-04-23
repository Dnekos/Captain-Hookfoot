using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockManager : MonoBehaviour
{
    [SerializeField]
    RotaryLock[] locks;
    int[] combination = { 0, 7, 2, 5 };
    [SerializeField]
    GameObject display_desk;
    [SerializeField]
    DisplayNode Main;
    // Update is called once per frame
    void Update()
    {
        if (!locks[0].isActiveAndEnabled)//.gameObject.activeInHierarchy)
            return; 

        for (int i  = 0;  i < 4; i++)
        {
            if (locks[i].value != combination[i])
                return;
        }
        Destroy(display_desk);

        GameObject.Find("debug").GetComponent<UnityEngine.UI.Text>().text = "I think I got it!";
        for (int i = 0; i < 4; i++)
            locks[i].GetComponent<Button>().enabled = false;
        //Main.Interact(InventoryItem.None);
    }
}
