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
        if (!locks[0].isActiveAndEnabled || !locked)
            return; 

        for (int i  = 0;  i < 4; i++)
        {
            if (locks[i].value != i)
                return;
        }
        Destroy(display_desk);
        GameObject.Find("debug").GetComponent<UnityEngine.UI.Text>().text = "I think I got it, its open!";
        SoundManager.PlaySound(Sound.Drawer); // audio feedback

        for (int i = 0; i < 4; i++) // disable buttons
            locks[i].GetComponent<Button>().enabled = false;
        locked = false;
    }
}
