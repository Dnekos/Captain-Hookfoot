using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.EventSystems;

public class ClickableNode : MonoBehaviour, IPointerClickHandler
{
    string connection;
    IDbConnection dbcon;
    IDataReader reader;
    
    string dbName = "Project3.db";

    [SerializeField]
    int id;
    [SerializeField]
    protected int state = 0;

    void OpenDatabase()
    {

        // Open connection
        dbcon.Open();
    }

    void InitDatabase()
    {
        //Setup file with url
        connection = "URI=file:" + Application.dataPath + "/" + dbName;
        dbcon = new SqliteConnection(connection);
    }

    string FetchTextByID(int id, string term)
    {
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

    void Start()
    {
        InitDatabase();
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
            if (Player.instance.GetAction() != Actions.Interact)
                Player.instance.SetAction(Actions.Interact);
        }
    }
    public virtual void LookAt()
    {
        Debug.Log(FetchTextByID(id, "LookDialogue"));
    }
}
