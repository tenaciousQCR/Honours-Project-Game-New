using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SQLite;

public class Vessel : MonoBehaviour
{
    public string VesselID { get; set; }
    public int Storage { get; set; }
    public int Profit { get; set; }

    public Vessel(string vesselID, int storage, int profit)
    {
        VesselID = vesselID;
        Storage = storage;
        Profit = profit;
    }

    public void SaveToDB(SQLiteConnection connection, string tablename)
    {
        SQLiteCommand cmnd = connection.CreateCommand();
        cmnd.CommandText = "INSERT INTO " + tablename 
            + " (id, storage, profit) VALUES ('" + VesselID + "', " + Storage + ", " + Profit + ")";
        cmnd.ExecuteNonQuery();
    }

    public void UpdateDB(SQLiteConnection connection, string tablename)
    {
        SQLiteCommand cmnd = connection.CreateCommand();
        cmnd.CommandText = 
            "UPDATE " + tablename 
            + " SET storage = " + Storage 
            + ", profit = " + Profit 
            + " WHERE id = '" + VesselID + "'";
        cmnd.ExecuteNonQuery();
    }

    public void UpdateClass(SQLiteConnection connection, string tablename)
    {
        SQLiteCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM " + tablename + " WHERE id='" + VesselID + "'";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            //VesselID = reader[0].ToString();
            Storage = int.Parse(reader[1].ToString());
            Profit = int.Parse(reader[2].ToString());
            //Debug.Log(Application.persistentDataPath);
        }
    }

    public void PrintTest(SQLiteConnection connection, string tablename)
    {
        // Read and print all values in table
        SQLiteCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM " + tablename + " WHERE id='" + VesselID + "'";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Debug.Log("Vessel id: " + reader[0].ToString());
            Debug.Log("Vessel storage: " + reader[1].ToString());
            Debug.Log("Vessel profit: " + reader[2].ToString());
            Debug.Log(Application.persistentDataPath);
        }
    }

}
