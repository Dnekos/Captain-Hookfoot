using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // inv panel
    [SerializeField]
    GameObject InventorySlotPrefab;
    [SerializeField]
    List<GameObject> invImages;
    [SerializeField]
    RectTransform invPanel;

    // inv button
    Vector3 startingposition;
    int row;

    // content fitter
    GameObject SizeHolder; // prevents the panel from getting too small
    private void Start()
    {
        invImages = new List<GameObject>();
        startingposition = Vector3.zero;//invPanel.localPosition;
        SizeHolder = null;
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

    public void AddInventoryImage(InventoryItem data)
    {
        InventorySlotPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Inventory/"+data);
        InventorySlotPrefab.GetComponent<InventorySlotManager>().invItem = data;
        invImages.Add(Instantiate(InventorySlotPrefab, invPanel));
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
}
