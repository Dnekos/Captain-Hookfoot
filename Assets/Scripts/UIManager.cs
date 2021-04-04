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
    bool inventoryopen = false;
    Vector3 startingposition;

    // content fitter
    //[SerializeField]
    //GameObject SizeHolderPrefab;
    GameObject SizeHolder; // prevents the panel from getting too small
    private void Start()
    {
        invImages = new List<GameObject>();
        startingposition = transform.localPosition;
        SizeHolder = null;
    }
    private void Update()
    {
        if (inventoryopen)
            transform.localPosition = Vector3.Lerp(transform.localPosition, startingposition + new Vector3(0, invPanel.sizeDelta.y), 0.05f);
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, startingposition, 0.05f);
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
    public void openInventory()
    {
        inventoryopen = !inventoryopen;
    }
}
