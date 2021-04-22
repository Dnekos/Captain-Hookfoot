using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Button_Exit()
    {
        Debug.Log("exit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Button_Resume()
    {
        Player.instance.GetComponent<Player>().SetPause(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
