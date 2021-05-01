using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotaryLock : MonoBehaviour
{
    [SerializeField]
    Sprite[] textbox;
    [SerializeField]
    Image icon;
    public int value = 0;
    [SerializeField]
    Camera cam;
    public void click()
    {
        if (cam.gameObject.activeInHierarchy)
        {
            value = (value + 1) % 4;
            icon.sprite = textbox[value];
            Player.PlayNoise(Sound.Rotary);

        }
    }

}
