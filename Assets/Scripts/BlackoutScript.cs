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
    float speedmultiplier = 11;
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
            if(col.a < 0.01f)
                Destroy(transform.parent.gameObject);
            Debug.Log(col.a);
            col.a = Mathf.Lerp(col.a, 0, speedmultiplier * Time.deltaTime);
        }
        else
        { 
            if(col.a > 0.99f)
            {
                if (index < 3) // delete the player UI when going to title screen
                    Player.ClosePlayer();
                SceneManager.LoadScene(index);
                falling = true;
            }
            col.a = Mathf.Lerp(col.a, 1, speedmultiplier * Time.deltaTime);
        }
        img.color = col;
    }
}