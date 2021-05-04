using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipObserver : BaseObserver
{
    bool spoketoPete = false;
    public void EndGame()
    {
        OpenDialogue(11);
        spoketoPete = true;
    }
    private void Update()
    {
        if (spoketoPete && Player.instance.gameState == Player.GameState.PLAY) // used crew on Pete AND finished the convo
        {
            Player.ClosePlayer(); // delete player UI
            LoadScene(1); //load credits
        }
    }
}
