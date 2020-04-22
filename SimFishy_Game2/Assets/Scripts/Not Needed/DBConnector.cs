using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SQLite;
using System.IO;
using GameClasses;
using DBClasses;

public class DBConnector : MonoBehaviour
{
    void Start()
    {
        // Create database
        string connectionLink = "URI=file:" + Application.dataPath + "/" + "My_Test_Database";

        // Open connection
        SQLiteConnection connection = new SQLiteConnection(connectionLink);
        connection.Open();

        //create table
        //CreateTable(connection);

        //insert
        //InsertTest(connection, 2, 8, "my_table");

        // Read and print all values in table
        SQLiteCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM my_table";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString());
            Debug.Log("val: " + reader[1].ToString());
            Debug.Log(Application.persistentDataPath);
        }

        connection.Close();
    }

    void CreateTable(SQLiteConnection connection)
    {
        // Create table
        SQLiteCommand dbcmd = connection.CreateCommand();
        //dbcmd.CommandType = System.Data.CommandType.Text;
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS my_table (id INTEGER PRIMARY KEY, val INTEGER )";
        dbcmd.ExecuteReader();
    }

    void InsertTest(SQLiteConnection connection, int id, int val, string tablename)
    {
        // Insert values in table
        SQLiteCommand cmnd = connection.CreateCommand();
        cmnd.CommandText = "INSERT INTO " + tablename + " (id, val) VALUES (" + id + ", " + val + ")";
        cmnd.ExecuteNonQuery();
    }

    void PrintTest(SQLiteConnection connection)
    {
        // Read and print all values in table
        SQLiteCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM my_table";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString());
            Debug.Log("val: " + reader[1].ToString());
            Debug.Log(Application.persistentDataPath);
        }
    }
}
