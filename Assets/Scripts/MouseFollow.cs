using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class MouseFollow : MonoBehaviour
{
    Camera cam;
    Image im;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        im = GetComponent<Image>();
        im.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(GetComponent<RectTransform>().sizeDelta.x, -1*GetComponent<RectTransform>().sizeDelta.y) * 0.6f + Mouse.current.position.ReadValue();
    }
    public void setImage(InventoryItem data)
    {
        im.enabled = true;
        im.sprite = Resources.Load<Sprite>("Inventory/" + data);
    }
    public void closeImage()
    {
        im.enabled = false;
    }
}
