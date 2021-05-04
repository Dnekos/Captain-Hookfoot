using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackoutScript : MonoBehaviour
{
    Image img;
    bool falling;
    public int index;

    public static void Transition(int sceneIndex)
    {
        GameObject blackout = Object.Instantiate(Resources.Load<GameObject>("Prefabs/BlackoutCanvas"));
        DontDestroyOnLoad(blackout);
        blackout.GetComponentInChildren<BlackoutScript>().index = sceneIndex;
    }
    void Awake()
    {
        falling = false;
        img = gameObject.GetComponent<Image>();
        Color col = img.color;
        col.a = 0.0f;
        img.color = col;
    }
    
    void Update()
    { 
        Color col = img.color;
        if(falling)
        {
            if(col.a < 0.01)
            {
                Destroy(transform.parent.gameObject);
            }
            col.a = Mathf.Lerp(0, col.a, 0.90f);
        }
        else{ 
            if(col.a > 0.99)
            {
                SceneManager.LoadScene(index);
                falling = true;
            }
            col.a = Mathf.Lerp(col.a, 1, 0.1f);
        }
        img.color = col;
    }
}