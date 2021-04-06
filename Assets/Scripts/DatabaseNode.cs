using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class DatabaseNode : MonoBehaviour
{

    string connection;
    IDbConnection dbcon;
    IDataReader reader;
    
    string dbName = "Project3.db";

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
        string output = reader[1].ToString(); 
        dbcon.Close();
        return output;
    }

    void Start()
    {
        InitDatabase();
        Debug.Log(FetchTextByID(1, "LookDialogue"));
        Debug.Log(FetchTextByID(2, "LookDialogue"));
    }
}

