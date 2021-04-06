using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.EventSystems;

public enum NodeIDs
{
    Well = 1,
    Anchor,
    CaveToIsland,
    CaveToWell,
    Lock,
    Rope
}

public class ClickableNode : MonoBehaviour, IPointerClickHandler
{
    string connection;
    IDbConnection dbcon;
    IDataReader reader;
    
    string dbName = "Project3.db";

    [SerializeField]
    protected NodeIDs UID;
    [SerializeField]
    protected int state = 0;
    

    void InitDatabase()
    {
        //Setup file with url
        connection = "URI=file:" + Application.dataPath + "/" + dbName;
        Debug.LogError(connection);
        dbcon = new SqliteConnection(connection);
    }

    string FetchTextByID(int id, string term)
    {
        InitDatabase();

        // READING DATA
        // Read and print all values in table
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        string query = "SELECT " + term + " FROM NodeDialogue WHERE ID=" + id;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        string output = reader[0].ToString(); 
        dbcon.Close();
        return output;
    }

    public virtual void LookAt()
    {
        TextDebug(FetchTextByID((int)UID, "LookDialogue"));
    }
    public virtual void Interact(InventoryItem item)
    {
        Debug.Log("clicked"); // get dialogue
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            LookAt(); // TEMP: right click looks
        else if (eventData.button == PointerEventData.InputButton.Left && Player.instance.GetHeldItem() == InventoryItem.None)
            Interact(Player.instance.GetHeldItem());
        else
        {
            Interact(Player.instance.GetHeldItem());
            // reset actions to default
            if (Player.instance.GetAction() != Actions.Interact) // unsure if getaction/setaction are needed
                Player.instance.SetAction(Actions.Interact);
        }
    }

    void TextDebug(string text)
    {
        GameObject.Find("debug").GetComponent<UnityEngine.UI.Text>().text = text;
    }
}
