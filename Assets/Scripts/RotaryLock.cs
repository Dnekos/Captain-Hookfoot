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
    public void click()
    {
        value = (value + 1) % 4;
        icon.sprite = textbox[value];
    }

}
