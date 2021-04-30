using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutScript : MonoBehaviour
{
    float percent;
    Image img;
    static void Transition()
    {

    }
    void Awake()
    {
        percent = 0.0f;
    }
    
    void Update()
    {
        if(percent < 100)
        {
            percent += 1.0f;
            Color col = img.color;
            col.a = Mathf.Lerp(0.0f,100.0f, 50.0f);
            img.color = col;
        }
    }
}
