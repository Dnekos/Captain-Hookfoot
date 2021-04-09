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
    Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
        screenHeight = cam.scaledPixelHeight;
        screenWidth = cam.scaledPixelWidth;
    }
    private void Update()
    {
        float diff = speed * Time.deltaTime, spriteradius = border.sprite.bounds.size.x * 0.5f;
        Vector2 mouse = Mouse.current.position.ReadValue();
        Debug.Log(spriteradius + " " + spriteradius / 50f + " " + spriteradius * 0.05f);
        if (mouse.x > screenWidth - 30 && cam.ScreenToWorldPoint(new Vector2(screenWidth, 0)).x + diff + spriteradius * 0.06 < border.transform.position.x + spriteradius)
            transform.position += Vector3.right * diff;
        else if (mouse.x < 30 && cam.ScreenToWorldPoint(new Vector2(0, 0)).x - diff - spriteradius * 0.06 > border.transform.position.x - spriteradius)
            transform.position += Vector3.left * diff;

    }
}
