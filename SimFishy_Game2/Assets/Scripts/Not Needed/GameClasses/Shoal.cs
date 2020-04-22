using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SQLite;

namespace GameClasses
{
    public class Shoal : MonoBehaviour
    {
        public string ShaolID { get; set; }
        public int Size { get; set; }

        public Shoal(string shaolID, int size)
        {
            ShaolID = shaolID;
            Size = size;
        }

        public void SaveToDB(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText = "INSERT INTO " + tablename + " (id, size) VALUES ('" + ShaolID + "', " + Size + ")";
            cmnd.ExecuteNonQuery();
        }

        public void UpdateDB(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText = "UPDATE " + tablename + " SET size = " + Size + " WHERE id = '" + ShaolID + "'";
            cmnd.ExecuteNonQuery();
        }

        public void UpdateClass(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT size FROM " + tablename + " WHERE id='" + ShaolID + "'";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();
            while (reader.Read())
            {
                //ShaolID = reader[0].ToString();
                Size = int.Parse(reader[0].ToString());
                //Debug.Log(Application.persistentDataPath);
            }

            
        }

        public void PrintTest(SQLiteConnection connection, string tablename)
        {
            // Read and print all values in table
            SQLiteCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM " + tablename + " WHERE id='" + ShaolID + "'";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();

            while (reader.Read())
            {
                Debug.Log("id: " + reader[0].ToString());
                Debug.Log("size: " + reader[1].ToString());
                Debug.Log(Application.persistentDataPath);
            }
        }
    }
}

