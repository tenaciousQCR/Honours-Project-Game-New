using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SQLite;
using System;
using Random = System.Random;

namespace DBClasses
{
    public class ShoalDB : MonoBehaviour
    {
        public string ShoalID { get; set; }
        public int Size { get; set; }
        public string Coords { get; set; }
        public int Age { get; set; }
        public List<string> CoordList { get; set; }

        public ShoalDB(string shoalID, int size, string coords)
        {
            ShoalID = shoalID;
            Size = size;
            Coords = coords;
            Age = 1;
            CoordList = new List<string>() {
                "0212", "0312", "0412", "0512", "0612",
                "0211", "0311", "0411", "0511", "0611",
        "0110", "0210", "0310", "0410", "0510", "0610",
        "0109", "0209", "0309", "0409", "0509", "0609", "0709",
                "0208", "0308", "0408", "0508", "0608", "0708", "0808",
                "0207", "0307", "0407", "0507", "0607", "0707", "0807",
                "0206", "0306", "0406", "0506", "0606", "0706", "0806",
                "0205", "0305", "0405", "0505", "0605", "0705", "0805",
                        "0304", "0404", "0504", "0604", "0704", "0804",
                        "0303", "0403", "0503", "0603",
                                "0402", "0502", "0602",
                                "0401", "0501"};
        }

        public void SaveToDB(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText = "INSERT INTO " + tablename + " (id, size, coords, age) VALUES ('" + ShoalID + "', " + Size + ", '" + Coords + "', " + Age + ")";
            cmnd.ExecuteNonQuery();
        }

        public void UpdateDB(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText = "UPDATE " + tablename 
                + " SET size = " + Size 
                + ", coords = '" + Coords
                + "', age = " + Age
                + " WHERE id = '" + ShoalID + "'";
            cmnd.ExecuteNonQuery();
        }

        public void UpdateClass(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM " + tablename + " WHERE id='" + ShoalID + "'";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();
            while (reader.Read())
            {
                //ShoalID = reader[0].ToString();
                Size = int.Parse(reader[1].ToString());
                Coords = reader[2].ToString();
                Age = int.Parse(reader[3].ToString());
                //Debug.Log(Application.persistentDataPath);
            }
        }

        public void PrintTest(SQLiteConnection connection, string tablename)
        {
            // Read and print all values in table
            SQLiteCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM " + tablename + " WHERE id='" + ShoalID + "'";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();

            while (reader.Read())
            {
                Debug.Log("id: " + reader[0].ToString());
                Debug.Log("size: " + reader[1].ToString());
                Debug.Log("coords: " + reader[2].ToString());
                Debug.Log(Application.persistentDataPath);
            }
        }

        public void FishShoal(Vessel boat)
        {
            if (Size >= 20)
            {
                Size -= 20;
                boat.Storage += 20;
            }
        }

        
        public void FishShoal(VesselDB boat)
        {
            Random random = new Random();
            int catchSize = 0;
            int randomGrade = 0;
            int remainingStorage = 0;
            //if there is more than 200 fish only take upto 200
            if (Size < 100)
            {
                catchSize = random.Next(Size+1);
            }
            else
            {
                catchSize = random.Next(100);
            }

            // the ages 1, 2 and 3 represent the young, medium and old shoals.
            //this is used for the grade distribution
            switch (Age)
            {
                //shoal is young
                case 1:
                case 2:
                    for (int i = 0; i < catchSize; i++)
                    {
                        //generate number between 4 and 6
                        randomGrade = random.Next(4, 7);
                        //add the fish to the boat
                        switch (randomGrade)
                        {
                            //if the fish is grade 4 (roughly weight 3)
                            case 4:
                                remainingStorage = boat.MaxStorage - boat.Storage;
                                if (remainingStorage >= 3)
                                {
                                    //add fish to storage weight and remove from the shoal
                                    boat.Storage += 3;
                                    Size--;
                                }
                                else
                                {
                                    Debug.Log("Not enough space");
                                }
                                break;
                            //if the fish is grade 5 (roughly weight 2)
                            case 5:
                                remainingStorage = boat.MaxStorage - boat.Storage;
                                if (remainingStorage >= 2)
                                {
                                    //add fish to storage weight and remove from the shoal
                                    boat.Storage += 2;
                                    Size--;
                                }
                                else
                                {
                                    Debug.Log("Not enough space");
                                }
                                break;
                            //if the fish is grade 6
                            case 6:
                                remainingStorage = boat.MaxStorage - boat.Storage;
                                if (remainingStorage >= 1)
                                {
                                    //add fish to storage weight and remove from the shoal
                                    boat.Storage += 1;
                                    Size--;
                                }
                                else
                                {
                                    Debug.Log("Not enough space");
                                }
                                break;
                            default:
                                Debug.Log("Age 1 shoal giving grade error");
                                break;
                        }
                    }
                    break;
                //shoal is average
                case 3:
                case 4:
                case 5:
                    for (int i = 0; i < catchSize; i++)
                    {
                        //generate number between 2 and 4
                        randomGrade = random.Next(2, 5);
                        //add the fish to the boat
                        switch (randomGrade)
                        {
                            //if the fish is grade 2 (roughly weight 5)
                            case 2:
                                remainingStorage = boat.MaxStorage - boat.Storage;
                                if (remainingStorage >= 5)
                                {
                                    //add fish to storage weight and remove from the shoal
                                    boat.Storage += 5;
                                    Size--;
                                }
                                else
                                {
                                    Debug.Log("Not enough space");
                                }
                                break;
                            //if the fish is grade 3 (roughly weight 4)
                            case 3:
                                remainingStorage = boat.MaxStorage - boat.Storage;
                                if (remainingStorage >= 4)
                                {
                                    //add fish to storage weight and remove from the shoal
                                    boat.Storage += 4;
                                    Size--;
                                }
                                else
                                {
                                    Debug.Log("Not enough space");
                                }
                                break;
                            //if the fish is grade 4 (roughly weight 3)
                            case 4:
                                remainingStorage = boat.MaxStorage - boat.Storage;
                                if (remainingStorage >= 3)
                                {
                                    //add fish to storage weight and remove from the shoal
                                    boat.Storage += 3;
                                    Size--;
                                }
                                else
                                {
                                    Debug.Log("Not enough space");
                                }
                                break;
                            default:
                                Debug.Log("Age 2 shoal giving grade error");
                                break;
                        }
                    }
                    break;
                default:
                    for (int i = 0; i < catchSize; i++)
                    {
                        //generate number between 1 and 3
                        randomGrade = random.Next(1, 4);
                        //add the fish to the boat
                        switch (randomGrade)
                        {
                            //if the fish is grade 1 (roughly weight 6)
                            case 1:
                                remainingStorage = boat.MaxStorage - boat.Storage;
                                if (remainingStorage >= 6)
                                {
                                    //add fish to storage weight and remove from the shoal
                                    boat.Storage += 6;
                                    Size--;
                                }
                                else
                                {
                                    Debug.Log("Not enough space");
                                }
                                break;
                            //if the fish is grade 2 (roughly weight 5)
                            case 2:
                                remainingStorage = boat.MaxStorage - boat.Storage;
                                if (remainingStorage >= 5)
                                {
                                    //add fish to storage weight and remove from the shoal
                                    boat.Storage += 5;
                                    Size--;
                                }
                                else
                                {
                                    Debug.Log("Not enough space");
                                }
                                break;
                            //if the fish is grade 3 (roughly weight 4)
                            case 3:
                                remainingStorage = boat.MaxStorage - boat.Storage;
                                if (remainingStorage >= 4)
                                {
                                    //add fish to storage weight and remove from the shoal
                                    boat.Storage += 4;
                                    Size--;
                                }
                                else
                                {
                                    Debug.Log("Not enough space");
                                }
                                break;
                            default:
                                Debug.Log("Age 3 shoal giving grade error");
                                break;
                        }
                    }
                    break;
                /*
                default:
                    Debug.Log("Default age selected in fishing shoal");
                    break;
                */
            }

        }

        // moving functions
        public void MoveShoalUp()
        {
            //spilt the coordinate sting into an array of characters
            char[] coordArray = Coords.ToCharArray();
            //split the coordinates into x and y
            string x = coordArray[0].ToString() + coordArray[1].ToString();
            string y = coordArray[2].ToString() + coordArray[3].ToString();
            string oldCoords = x + y;
            //turn y into an int and increse its value before making it a string again for comparison
            int yint = int.Parse(y);
            yint++;
            string newy = yint.ToString();
            if (yint < 10)
            {
                newy = "0" + yint.ToString();
            }

            foreach (var i in CoordList)
            {
                char[] coordArray2 = i.ToCharArray();
                string x2 = coordArray2[0].ToString() + coordArray2[1].ToString();
                string y2 = coordArray2[2].ToString() + coordArray2[3].ToString();
                string newCoords = x2 + y2;
                //if the new coordinates are on the list of possible coordinates, the shoal shall move
                if (x == x2 && newy == y2)
                {
                    Debug.Log("shoal: " + ShoalID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if (Coords == oldCoords)
            {
                Debug.Log("shoal: " + ShoalID + " tried to move up but failed");
            }
        }

        public void MoveShoalDown()
        {
            //spilt the coordinate sting into an array of characters
            char[] coordArray = Coords.ToCharArray();
            //split the coordinates into x and y
            string x = coordArray[0].ToString() + coordArray[1].ToString();
            string y = coordArray[2].ToString() + coordArray[3].ToString();
            string oldCoords = x + y;
            //turn y into an int and increse its value before making it a string again for comparison
            int yint = int.Parse(y);
            yint--;
            string newy = yint.ToString();
            if (yint < 10)
            {
                newy = "0" + yint.ToString();
            }

            foreach (var i in CoordList)
            {
                char[] coordArray2 = i.ToCharArray();
                string x2 = coordArray2[0].ToString() + coordArray2[1].ToString();
                string y2 = coordArray2[2].ToString() + coordArray2[3].ToString();
                string newCoords = x2 + y2;
                //if the new coordinates are on the list of possible coordinates, the shoal shall move
                if (x == x2 && newy == y2)
                {
                    Debug.Log("shoal: " + ShoalID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if (Coords == oldCoords)
            {
                Debug.Log("shoal: " + ShoalID + " tried to move down but failed");
            }
        }

        public void MoveShoalLeft()
        {
            //spilt the coordinate sting into an array of characters
            char[] coordArray = Coords.ToCharArray();
            //split the coordinates into x and y
            string x = coordArray[0].ToString() + coordArray[1].ToString();
            string y = coordArray[2].ToString() + coordArray[3].ToString();
            string oldCoords = x + y;
            //turn x into an int and decrease its value before making it a string again for comparison
            int xint = int.Parse(x);
            xint--;
            string newx = xint.ToString();
            if (xint < 10)
            {
                newx = "0" + xint.ToString();
            }

            foreach (var i in CoordList)
            {
                char[] coordArray2 = i.ToCharArray();
                string x2 = coordArray2[0].ToString() + coordArray2[1].ToString();
                string y2 = coordArray2[2].ToString() + coordArray2[3].ToString();
                string newCoords = x2 + y2;
                //if the new coordinates are on the list of possible coordinates, the shoal shall move
                if (newx == x2 && y == y2)
                {
                    Debug.Log("shoal: " + ShoalID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if (Coords == oldCoords)
            {
                Debug.Log("shoal: " + ShoalID + " tried to move left but failed");
            }
        }

        public void MoveShoalRight()
        {
            //spilt the coordinate sting into an array of characters
            char[] coordArray = Coords.ToCharArray();
            //split the coordinates into x and y
            string x = coordArray[0].ToString() + coordArray[1].ToString();
            string y = coordArray[2].ToString() + coordArray[3].ToString();
            string oldCoords = x + y;
            //turn x into an int and increse its value before making it a string again for comparison
            int xint = int.Parse(x);
            xint++;
            string newx = xint.ToString();
            if (xint < 10)
            {
                newx = "0" + xint.ToString();
            }
            

            foreach (var i in CoordList)
            {
                char[] coordArray2 = i.ToCharArray();
                string x2 = coordArray2[0].ToString() + coordArray2[1].ToString();
                string y2 = coordArray2[2].ToString() + coordArray2[3].ToString();
                string newCoords = x2 + y2;
                //if the new coordinates are on the list of possible coordinates, the shoal shall move
                if (newx == x2 && y == y2)
                {
                    Debug.Log("shoal: " + ShoalID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if(Coords == oldCoords)
            {
                Debug.Log("shoal: " + ShoalID + " tried to move right but failed");
            }
        }

        public void MoveRandom()
        {
            Random random = new Random();
            int randint = random.Next(1, 5);

            switch (randint) {
                case 1:
                    MoveShoalUp();
                    break;
                case 2:
                    MoveShoalDown();
                    break;
                case 3:
                    MoveShoalLeft();
                    break;
                case 4:
                    MoveShoalRight();
                    break;
                default:
                    Debug.Log("random error encountered for " + ShoalID);
                    break;
            }
        }
    }
}


