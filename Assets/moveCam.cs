using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class moveCam : MonoBehaviour
{
    float screenWidth, screenHeight;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    SpriteRenderer border;
    private void Start()
    {
        Camera cam = GetComponent<Camera>();
        screenHeight = cam.scaledPixelHeight;
        screenWidth = cam.scaledPixelWidth;
    }
    private void Update()
    {
        Vector2 mouse = Mouse.current.position.ReadValue();
        if (mouse.x > screenWidth - 30 && mouse.x + speed * Time.deltaTime < border.sprite.border.w)
            transform.position += Vector3.right * speed * Time.deltaTime;
        else if (mouse.x < 30 && mouse.x + speed * Time.deltaTime > border.sprite.border.x)
            transform.position -= Vector3.right * speed * Time.deltaTime;

    }
}
