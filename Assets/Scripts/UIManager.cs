using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField]
    GameObject DialoguePanel;

    [Header("Inventory")]
    [SerializeField]
    GameObject InventorySlotPrefab;
    [SerializeField]
    List<GameObject> invImages;
    List<InventoryItem> previouslyhelditems;
    [SerializeField]
    RectTransform invPanel;

    // inv button
    Vector3 startingposition;
    int row;

    // content fitter
    GameObject SizeHolder; // prevents the panel from getting too small

    // BUCKET
    int bucketitems;
    private void Start()
    {
        invImages = new List<GameObject>();
        previouslyhelditems = new List<InventoryItem>();
        startingposition = Vector3.zero;//invPanel.localPosition;
        SizeHolder = null;
        DialoguePanel.SetActive(false);

        // DEBUG
        //StartDialogue(1);
    }
    private void Update()
    {
        invPanel.localPosition = Vector3.Lerp(invPanel.localPosition, 
            startingposition + row * new Vector3(0, InventorySlotPrefab.GetComponent<RectTransform>().sizeDelta.y - 10), 0.05f);

        if (invImages.Count == 0 && !SizeHolder)
        {
            SizeHolder = Instantiate(InventorySlotPrefab, invPanel);
            SizeHolder.GetComponent<Image>().enabled = false;
        }
        else if (invImages.Count > 0 && SizeHolder)
        {
            Destroy(SizeHolder);
            SizeHolder = null;
        }
    }
    public bool Contains(InventoryItem data)
    {
        foreach (GameObject slot in invImages)
            if (slot.GetComponent<InventorySlotManager>().invItem == data)
            {
                return true;
            }
        return false;
    }

    public void AddInventoryImage(InventoryItem data)
    {
        if (previouslyhelditems.Contains(data))
            return;
        InventorySlotPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Inventory/prop_"+data);
        InventorySlotPrefab.GetComponent<InventorySlotManager>().invItem = data;

        invImages.Add(Instantiate(InventorySlotPrefab, invPanel));
        previouslyhelditems.Add(data);
    }
    public void RemoveInventoryImage(InventoryItem index)
    {
        Debug.Log("Deleting " + index);
        foreach ( GameObject slot in invImages)
            if (slot.GetComponent<InventorySlotManager>().invItem == index)
            {
                Destroy(slot);
                invImages.Remove(slot);
                break;
            }
    }

    public void ChangeRow(int dir)
    {
        if (row + dir >= 0 && row + dir < Mathf.Ceil(invImages.Count / 3f))
            row += dir;
    }

    public void StartDialogue(int TreeID)
    {
        Player.instance.gameState = Player.GameState.DIALOGUE;
        DialoguePanel.SetActive(true);
        DialoguePanel.GetComponent<DialogueManager>().StartDialogue(TreeID);
    }

    public bool IncrementBucket()
    {
        bucketitems++;
        if (bucketitems >= 6)
            return true;
        return false;
    }
}
