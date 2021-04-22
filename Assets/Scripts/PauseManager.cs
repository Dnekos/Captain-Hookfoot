using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
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

    public void Toggle_FullScreen()
    
    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    // Update is called once per frame
    void Update()

    {
        Debug.Log("Full screen set to " + !Screen.fullScreen);
        Screen.fullScreen = !Screen.fullScreen;
    }
}
