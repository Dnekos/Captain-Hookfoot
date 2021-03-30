using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject InventorySlotPrefab;
    [SerializeField]
    GameObject[] invImages;
    private void Start()
    {
        invImages = new GameObject[3];
    }
    public void AddInventoryImage(InventoryItem data,int index)
    {
        InventorySlotPrefab.GetComponent<InventorySlotManager>().index = index;
        invImages[index] = Instantiate(InventorySlotPrefab, transform);
    }
    public void RemoveInventoryImage(int index)
    {
        Debug.Log("Deleting" + index);
        Destroy(invImages[index].gameObject);
        invImages[index] = null;
    }
}
