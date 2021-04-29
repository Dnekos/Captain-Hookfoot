using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections.Generic;

public class Databaser : MonoBehaviour
{

    string connection;
    IDbConnection dbcon;
    IDataReader reader;

    string dbName = "Project3.db";

    protected struct DialogueLine
    {
        public string Name;
        public string Body;
        public string Left;
        public string Right;
        public DialogueLine(string name, string body,string left, string right)
        {
            this.Name = name;
            this.Body = body;
            this.Left = left;
            this.Right = right;
        }

    }

    void InitDatabase()
    {
        //Setup file with url
        connection = "URI=file:" + Application.dataPath + "/" + dbName;
        dbcon = new SqliteConnection(connection);
    }

    protected string FetchTextByID(int id, string term, string table = "NodeDialogue", int state = 0)
    {
        string query = "SELECT " + term + " FROM " + table + " WHERE ID=" + id;
        if (state != 0)
            query += " AND State=" + state.ToString();

        return FetchTextByQuery(query);
    }

    protected string FetchTextByQuery(string query)
    {
        InitDatabase();

        // READING DATA
        // Read and print all values in table
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();

        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        string output = reader[0].ToString();
        dbcon.Close();
        return output;
    }
    protected List<DialogueLine> FetchDialog(int TreeID)
    {
        InitDatabase();

        // READING DATA
        // Read and print all values in table
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        string query = "SELECT Speaker,Dialogue,LeftPortrait,RightPortrait FROM Conversations WHERE TreeID=" + TreeID;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        List<DialogueLine> output = new List<DialogueLine>();
        while (reader.Read())
        {
            output.Add(new DialogueLine(reader[0].ToString(),
                reader[1].ToString(), 
                reader[2].ToString(), 
                reader[3].ToString()));
        }

        dbcon.Close();
        return output;
    }
}

