using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.SQLite;
using System.IO;
using GameClasses;
using DBClasses;

public class TestDBConn : MonoBehaviour
{
    //UI Text
    public Text shoalIDText1;
    public Text shoalSizeText1;

    public Text shoalIDText2;
    public Text shoalSizeText2;

    public Text shoalIDText3;
    public Text shoalSizeText3;

    public Text vesselIDText;
    public Text vesselStorageText;
    public Text vesselProfitText;

    //buttons
    public Button fishBtn1;
    public Button fishBtn2;
    public Button fishBtn3;
    public Button sellBtn;
    public Button resetBtn;
    public Button saveBtn;

    //Shoals
    Shoal testShoal1 = new Shoal("S01", 0);
    Shoal testShoal2 = new Shoal("S02", 0);
    Shoal testShoal3 = new Shoal("S03", 0);
    Vessel testBoat = new Vessel("V01", 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        //activate buttons
        fishBtn1.onClick.AddListener(FishBtn1OnClick);
        fishBtn2.onClick.AddListener(FishBtn2OnClick);
        fishBtn3.onClick.AddListener(FishBtn3OnClick);
        sellBtn.onClick.AddListener(sellBtnOnClick);
        resetBtn.onClick.AddListener(resetBtnOnClick);
        saveBtn.onClick.AddListener(saveBtnOnClick);

        //load game
        LoadGame();
    }

    //new test methods
    public void UpdateUI()
    {
        //updating shoals
        shoalIDText1.text = "ID: " + testShoal1.ShaolID;
        shoalSizeText1.text = "Size: " + (testShoal1.Size).ToString();

        shoalIDText2.text = "ID: " + testShoal2.ShaolID;
        shoalSizeText2.text = "Size: " + (testShoal2.Size).ToString();

        shoalIDText3.text = "ID: " + testShoal3.ShaolID;
        shoalSizeText3.text = "Size: " + (testShoal3.Size).ToString();

        //updating vessels
        vesselIDText.text = "ID: " + testBoat.VesselID;
        vesselStorageText.text = "Storage: " + (testBoat.Storage).ToString();
        vesselProfitText.text = "Profit: " + (testBoat.Profit).ToString();
    }

    public void LoadGame()
    {
        // Create database
        string connectionLink = "URI=file:" + Application.dataPath + "/" + "My_Test_Database";

        // Open connection
        SQLiteConnection connection = new SQLiteConnection(connectionLink);
        connection.Open();

        //pulls the saved data from the database
        testShoal1.UpdateClass(connection, "shoals");
        testShoal2.UpdateClass(connection, "shoals");
        testShoal3.UpdateClass(connection, "shoals");
        testBoat.UpdateClass(connection, "vessels");

        connection.Close();
    }

    public void SaveGame()
    {
        // Create database
        string connectionLink = "URI=file:" + Application.dataPath + "/" + "My_Test_Database";
        // Open connection
        SQLiteConnection connection = new SQLiteConnection(connectionLink);
        connection.Open();

        //pulls the saved data from the database
        testShoal1.UpdateDB(connection, "shoals");
        testShoal2.UpdateDB(connection, "shoals");
        testShoal3.UpdateDB(connection, "shoals");
        testBoat.UpdateDB(connection, "vessels");

        connection.Close();
    }

    public void ResetGame()
    {
        testShoal1.Size = 100;
        testShoal2.Size = 200;
        testShoal3.Size = 300;
        testBoat.Storage = 0;
        testBoat.Profit = 0;

        SaveGame();
    }

    // button methods
    public void FishBtn1OnClick()
    {
        
        if (testShoal1.Size >= 20)
        {
            testShoal1.Size -= 20;
            testBoat.Storage += 20;
        }
    }

    public void FishBtn2OnClick()
    {

        if (testShoal2.Size >= 20)
        {
            testShoal2.Size -= 20;
            testBoat.Storage += 20;
        }
    }

    public void FishBtn3OnClick()
    {

        if (testShoal3.Size >= 20)
        {
            testShoal3.Size -= 20;
            testBoat.Storage += 20;
        }
    }

    public void sellBtnOnClick()
    {

        if (testBoat.Storage > 0)
        {
            testBoat.Profit = (testBoat.Storage) * 5;
            testBoat.Storage = 0;
        }
    }

    public void resetBtnOnClick()
    {
        ResetGame();
        SaveGame();
    }

    public void saveBtnOnClick()
    {
        SaveGame();
    }

    // original test stuff
    void CreateTable(SQLiteConnection connection)
    {
        // Create table
        SQLiteCommand dbcmd = connection.CreateCommand();
        //dbcmd.CommandType = System.Data.CommandType.Text;
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS my_table (id INTEGER PRIMARY KEY, val INTEGER )";
        dbcmd.ExecuteReader();
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

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
}
