using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player instance = null;
    [SerializeField]
    InventoryItem heldItem;
    [SerializeField]
    Actions currentAction = Actions.Interact;
    Dictionary<NodeIDs, int> loggedStates;

    Controls inputs;

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
        inputs.Game.Exit.performed += ctx => OnExit(); // bind the escape key to the OnExit Function

        DontDestroyOnLoad(gameObject); // may be unnecessary? prob is with shifting rooms
    }

    private void OnExit()
    {
        Debug.Log("exit");
        Application.Quit();
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
        GameObject.Find("InventoryMenu").GetComponent<UIManager>().RemoveInventoryImage(item); // delete UI object

        if (item == heldItem) // reset heldIndex (may be redundant?)
            heldItem = InventoryItem.None;
    }
    public InventoryItem GetHeldItem()
    {
        return heldItem;
    }

    public Actions GetAction()
    {
        return currentAction;
    }
    public void SetAction(Actions newaction, InventoryItem item = InventoryItem.None)
    {
        Debug.Log("Current action is now " + newaction);
        instance.heldItem = item;
        instance.currentAction = newaction;
    }

    //these two are needed for the inputs to work
    private void OnEnable()
    {
        instance.inputs.Game.Enable();
    }
    private void OnDisable()
    {
        instance.inputs.Game.Disable();
    }
}
