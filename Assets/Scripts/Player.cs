using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player instance = null;
    [SerializeField]
    InventoryItem heldItem;
    [SerializeField]
    UIManager UI;
    [SerializeField]
    public GameObject pausePanel;
    Dictionary<NodeIDs, int> loggedStates;

    bool savedPoe;

    Controls inputs;

    public enum GameState
    {
        PLAY,
        PAUSE,
        DIALOGUE
    }

    public GameState gameState;
    GameState previousState;

    private void Awake()
    {
        // create singletons
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance.loggedStates = new Dictionary<NodeIDs, int>();
        inputs = new Controls();
        gameState = GameState.PLAY;
        inputs.Game.Pause.performed += ctx => OnPause(); // bind the escape key to the OnPause Function

        DontDestroyOnLoad(gameObject);
    }

    private void OnPause()
    {
        Debug.Log("pause");
        if(gameState == GameState.PAUSE)
        {
            SetPause(false);
        }
        else
        {
            SetPause(true);
        }

    }

    public void SetPause(bool paused)
    {
        if(paused == false)
        {
            pausePanel.SetActive(false);
            gameState = previousState;
        }
        else
        {
            previousState = gameState;
            gameState = GameState.PAUSE;
            pausePanel.SetActive(true);
        }
    }

    public void LogState(NodeIDs node, int state)
    {
        if (!loggedStates.ContainsKey(node))
            loggedStates.Add(node, state);
        else
            loggedStates[node] = state;
    }

    public int GetState(NodeIDs node)
    {
        if (loggedStates.ContainsKey(node))
            return loggedStates[node];
        else
            return 0;
    }

    public void RemoveInvItem(InventoryItem item)
    {
        Debug.Log("Removing " + item + " from inventory");
        UI.RemoveInventoryImage(item); // delete UI object

        if (item == heldItem) // reset heldIndex (may be redundant?)
            heldItem = InventoryItem.None;
    }
    public bool ContainsItem(InventoryItem item)
    {
        return UI.Contains(item);
    }
    public InventoryItem GetHeldItem()
    {
        return heldItem;
    }
    public void TriggerSavedCrew()
    {

    }

    public void SetHeldItem(InventoryItem item = InventoryItem.None)
    {
        if (item == InventoryItem.None)
            GameObject.Find("MouseInv").GetComponent<MouseFollow>().closeImage(); // set mouse Image
        instance.heldItem = item;
    }

    //these two are needed for the inputs to work
    private void OnEnable()
    {
        instance.inputs.Game.Enable();
    }
    private void OnDisable()
    {
         if (instance == this)
            instance.inputs.Game.Disable();
    }
}
