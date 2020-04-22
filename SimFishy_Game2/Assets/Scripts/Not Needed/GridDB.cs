using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.SQLite;
using System.IO;
using DBClasses;

public class GridDB : MonoBehaviour
{
    // Start is called before the first frame update
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
    public Button sellBtn;
    public Button resetBtn;
    public Button saveBtn;
    public Button moveBtn;

    //grid
    public Button Grid0101;
    public Button Grid0102;
    public Button Grid0103;

    public Button Grid0201;
    public Button Grid0202;
    public Button Grid0203;

    public Button Grid0301;
    public Button Grid0302;
    public Button Grid0303;

    //List<string> coordList = new List<string>() {"0101", "0102","0103", "0201", "0202", "0203", "0301", "0302", "0303"};

    //Shoals
    ShoalDB testShoal1 = new ShoalDB("GS001", 0, "0000"); // TODO: set the coords in the default field of the class
    ShoalDB testShoal2 = new ShoalDB("GS002", 0, "0000");
    ShoalDB testShoal3 = new ShoalDB("GS003", 0, "0000");
    Vessel testBoat = new Vessel("V01", 0, 0);

    // lists
    List<ShoalDB> shoalList = new List<ShoalDB>();
    List<Square> gridSquareList = new List<Square>();



    void Start()
    {
        //button functions
        sellBtn.onClick.AddListener(sellBtnOnClick);
        resetBtn.onClick.AddListener(resetBtnOnClick);
        saveBtn.onClick.AddListener(saveBtnOnClick);
        moveBtn.onClick.AddListener(moveBtnOnClick);

        //grid functions
        Grid0101.onClick.AddListener(delegate { GridSquareOnClick("0101"); });
        Grid0102.onClick.AddListener(delegate { GridSquareOnClick("0102"); });
        Grid0103.onClick.AddListener(delegate { GridSquareOnClick("0103"); });

        Grid0201.onClick.AddListener(delegate { GridSquareOnClick("0201"); });
        Grid0202.onClick.AddListener(delegate { GridSquareOnClick("0202"); });
        Grid0203.onClick.AddListener(delegate { GridSquareOnClick("0203"); });

        Grid0301.onClick.AddListener(delegate { GridSquareOnClick("0301"); });
        Grid0302.onClick.AddListener(delegate { GridSquareOnClick("0302"); });
        Grid0303.onClick.AddListener(delegate { GridSquareOnClick("0303"); });

        // set up squares and then add to the list
        Square GS0101 = new Square(Grid0101, "0101", false, true);
        gridSquareList.Add(GS0101);
        Square GS0102 = new Square(Grid0102, "0102", false, true);
        gridSquareList.Add(GS0102);
        Square GS0103 = new Square(Grid0103, "0103", false, true);
        gridSquareList.Add(GS0103);

        Square GS0201 = new Square(Grid0201, "0201", false, true);
        gridSquareList.Add(GS0201);
        Square GS0202 = new Square(Grid0202, "0202", false, true);
        gridSquareList.Add(GS0202);
        Square GS0203 = new Square(Grid0203, "0203", false, true);
        gridSquareList.Add(GS0203);

        Square GS0301 = new Square(Grid0301, "0301", false, true);
        gridSquareList.Add(GS0301);
        Square GS0302 = new Square(Grid0302, "0302", false, true);
        gridSquareList.Add(GS0302);
        Square GS0303 = new Square(Grid0303, "0303", false, true);
        gridSquareList.Add(GS0303);


        //load game
        LoadGame();

        //updating shoal list
        shoalList.Add(testShoal1);
        shoalList.Add(testShoal2);
        shoalList.Add(testShoal3);

    }

    /////////////////////////////////////////////// game state functions //////////////////////////////////////// 
    public void LoadGame()
    {
        // Create database
        string connectionLink = "URI=file:" + Application.dataPath + "/" + "My_Test_Database";

        // Open connection
        SQLiteConnection connection = new SQLiteConnection(connectionLink);
        connection.Open();

        //pulls the saved data from the database
        testShoal1.UpdateClass(connection, "gridShoals");
        testShoal2.UpdateClass(connection, "gridShoals");
        testShoal3.UpdateClass(connection, "gridShoals");
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
        testShoal1.UpdateDB(connection, "gridShoals");
        testShoal2.UpdateDB(connection, "gridShoals");
        testShoal3.UpdateDB(connection, "gridShoals");
        testBoat.UpdateDB(connection, "vessels");

        connection.Close();
    }

    public void ResetGame()
    {
        testShoal1.Size = 400;
        testShoal2.Size = 10000;
        testShoal3.Size = 3000;
        testBoat.Storage = 0;
        testBoat.Profit = 0;

        SaveGame();
    }

    //////////////////////////////////////////// button functions ///////////////////////////////////////
    public void sellBtnOnClick()
    {

        if (testBoat.Storage > 0)
        {
            testBoat.Profit += (testBoat.Storage) * 5;
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

    public void moveBtnOnClick()
    {
        foreach (var i in shoalList)
        {
            i.MoveRandom();
        }
    }

    ////////////////////////////////// grid click ////////////////////////////
    public void GridSquareOnClick(string coords)
    {
        foreach (var i in shoalList)
        {
            if (i.Coords == coords)
            {
                FishShoal(i);
            }
        }

    }

    

    /////////////////////////////////// other functions //////////////
    public void FishShoal(ShoalDB shoal)
    {
        if (shoal.Size >= 20)
        {
            shoal.Size -= 20;
            testBoat.Storage += 20;
        }
    }

    public void UpdateUI()
    {
        //updating shoals
        shoalIDText1.text = "ID: " + testShoal1.ShoalID;
        shoalSizeText1.text = "Size: " + (testShoal1.Size).ToString();

        shoalIDText2.text = "ID: " + testShoal2.ShoalID;
        shoalSizeText2.text = "Size: " + (testShoal2.Size).ToString();

        shoalIDText3.text = "ID: " + testShoal3.ShoalID;
        shoalSizeText3.text = "Size: " + (testShoal3.Size).ToString();

        //updating vessels
        vesselIDText.text = "ID: " + testBoat.VesselID;
        vesselStorageText.text = "Storage: " + (testBoat.Storage).ToString();
        vesselProfitText.text = "Profit: " + (testBoat.Profit).ToString();
    }

    public void UpdateGrid()
    {
        //wipe the grid first
        foreach (var square in gridSquareList)
        {
            square.GridSquare.GetComponentInChildren<Text>().text = "";
        }

        //update with shoal ids
        foreach (var i in shoalList)
        {
            foreach (var square in gridSquareList)
            {
                if (i.Coords == square.Coords)
                {
                    square.GridSquare.GetComponentInChildren<Text>().text = (square.GridSquare.GetComponentInChildren<Text>().text) + i.ShoalID + " ";
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        UpdateGrid();
    }
}
