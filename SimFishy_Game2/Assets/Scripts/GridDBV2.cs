using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.SQLite;
using System.IO;
using DBClasses;
using System;
using Random = System.Random;


public class GridDBV2 : MonoBehaviour
{
    
    public Text shoalIDText1;
    public Text shoalSizeText1;
    public Text shoalAgeText1;

    public Text shoalIDText2;
    public Text shoalSizeText2;
    public Text shoalAgeText2;

    public Text shoalIDText3;
    public Text shoalSizeText3;
    public Text shoalAgeText3;

    //vessel text
    public Text vesselIDText;
    public Text vesselFuelText;
    public Text vesselMaxFuelText;
    public Text vesselProfitText;
    public Text vesselCaughtText;
    public Text vesselQuotaText;
    public Text vesselStorageText;
    public Text vesselMaxStorageText;

    //port buttons
    public Button refuelBtn;
    public Button sellBtn;

    //menu buttons
    public Button resetBtn;
    public Button saveBtn;
    public Button moveBtn;

    //vessel action buttons
    public Button upBtn;
    public Button downBtn;
    public Button leftBtn;
    public Button rightBtn;
    public Button fishBtn;

    public GameObject rescuePrompt;
    public Button rescueBtn;

    //panels
    public GameObject PortPanel;
    public GameObject VesselPanel;
    public GameObject MenuPanel;
    public GameObject ShoalPanel;

    public GameObject StatePanel;  // this is for the game state script


    public GameObject Grid;


    private GridManager GameGrid;
    private StateManager GameState;

    //Shoals
    //ShoalDB testShoal1 = new ShoalDB("GS001", 0, "0000"); // TODO: set the coords in the default field of the class
    //ShoalDB testShoal2 = new ShoalDB("GS002", 0, "0000");
    //ShoalDB testShoal3 = new ShoalDB("GS003", 0, "0000");
    VesselDB testBoat = new VesselDB("V01", "0000");

    // lists
    //List<ShoalDB> shoalList = new List<ShoalDB>();
    List<Square> gridSquareList = new List<Square>();

    int lastMonth;



    void Start()
    {
        //hide panels
        PortPanel.SetActive(false);
        VesselPanel.SetActive(false);
        MenuPanel.SetActive(false);
        ShoalPanel.SetActive(false);

        rescuePrompt.SetActive(false);

        //grid set up
        GameGrid = Grid.GetComponent<GridManager>();

        // game state 
        GameState = StatePanel.GetComponent<StateManager>();

        //button functions
        sellBtn.onClick.AddListener(sellBtnOnClick);
        refuelBtn.onClick.AddListener(refuelBtnOnClick);
        rescueBtn.onClick.AddListener(rescueBtnOnClick);

        resetBtn.onClick.AddListener(resetBtnOnClick);
        saveBtn.onClick.AddListener(saveBtnOnClick);
        //moveBtn.onClick.AddListener(moveBtnOnClick);

        //vessel move functions
        upBtn.onClick.AddListener(delegate { MoveOnClick(1); });
        downBtn.onClick.AddListener(delegate { MoveOnClick(2); });
        leftBtn.onClick.AddListener(delegate { MoveOnClick(3); });
        rightBtn.onClick.AddListener(delegate { MoveOnClick(4); });

        fishBtn.onClick.AddListener(FishOnClick);

        //load game
        LoadGame();

        //updating shoal list
        //shoalList.Add(testShoal1);
        //shoalList.Add(testShoal2);
        //shoalList.Add(testShoal3);

        // for monthly updates

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
        //testShoal1.UpdateClass(connection, "gridShoals");
        //testShoal2.UpdateClass(connection, "gridShoals");
        //testShoal3.UpdateClass(connection, "gridShoals");
        testBoat.UpdateClass(connection, "gridVessels");

        // state updateclass
        GameState.UpdateClass(connection);

        connection.Close();
    }

    public void SaveGame()
    {
        // Create database
        string connectionLink = "URI=file:" + Application.dataPath + "/" + "My_Test_Database";
        // Open connection
        SQLiteConnection connection = new SQLiteConnection(connectionLink);
        connection.Open();

        testBoat.UpdateDB(connection, "gridVessels");

        // state update db
        GameState.UpdateDB(connection);

        connection.Close();
    }

    public void ResetGame()
    {
        // boat
        testBoat.ResetBoat();

        // game state
        GameState.ResetGame();

        //shoals
        //testShoal1.Size = 400;
        //testShoal2.Size = 10000;
        //testShoal3.Size = 3000;

        SaveGame();
    }

    //////////////////////////////////////////// button functions ///////////////////////////////////////
    public void sellBtnOnClick()
    {
        //if the boat is holding fish
        if (testBoat.Storage > 0)
        {
            //if the weight in storage is less than the quota left
            if (testBoat.Storage <= testBoat.Quota-testBoat.Caught)
            {
                //pay the boat and store the fish
                testBoat.Profit += (testBoat.Storage) * 5;
                testBoat.Caught += testBoat.Storage;
                testBoat.Storage = 0;
            }
            //the player has caught too much
            else if (testBoat.Storage > testBoat.Quota - testBoat.Caught)
            {
                //see how much the player can sell
                int space = testBoat.Quota - testBoat.Caught;
                //give money
                testBoat.Profit += (space) * 5;
                //add to quota
                testBoat.Caught += space;
                //calculate losses
                int loss = testBoat.Storage-space;

                //empty storage
                testBoat.Storage = 0;
                //inform player of losses
                Debug.Log("cannont sell more than quota: you have lost " + loss + "kg of fish");
            }
            else
            {
                Debug.Log("error while attempting to sell fish");
            }
        }
    }

    public void refuelBtnOnClick()
    {
        if (testBoat.Fuel < testBoat.MaxFuel)
        {
            int fuelSpace = testBoat.MaxFuel - testBoat.Fuel;

            testBoat.Profit -= (fuelSpace*10);
            testBoat.Fuel = testBoat.MaxFuel;
        }
    }

    public void rescueBtnOnClick()
    {
        // send boat to dock and remove prompt
        testBoat.Coords = "0208";
        rescuePrompt.SetActive(false);
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

    /*
    public void moveBtnOnClick()
    {
        foreach (var i in shoalList)
        {
            i.MoveRandom();
        }
    }
    */

    //////////////////////////////////////////////////////////////////// movement, fishing and grid click //////////////////////////// ////////////////////////////
    public void FishOnClick()
    {
        //testBoat.IsFishing = true;
        int remainingStorage = testBoat.MaxStorage - testBoat.Storage;

        if (testBoat.Fuel > 1 && remainingStorage > 0)
        {
            foreach (var i in GameGrid.gridSquareList)
            {
                if (i.Coords == testBoat.Coords)
                {
                    FishSquare(i);
                    testBoat.Fuel -= 2;
                }
            }
        }
        else
        {
            Debug.Log("Not enough space or fuel");
        }
    }

    public void FishSquare(Square currentSquare)
    {
        Random random = new Random();
        int catchSize = 0;
        int remainingStorage = testBoat.MaxStorage - testBoat.Storage;

        // get a catch size
        if (testBoat.MaxStorage >= 50000)
        {
            catchSize = random.Next(5000);
        }
        else if (testBoat.MaxStorage >= 10000)
        {
            catchSize = random.Next(1000);
        }
        else if (testBoat.MaxStorage >= 1000)
        {
            catchSize = random.Next(100);
        }
        else
        {
            catchSize = random.Next(10);
        }

        // times the catch
        catchSize = (catchSize * currentSquare.FishRate);

        if (currentSquare.IsCoast == true)
        {
            FishCoastSquare(currentSquare, catchSize);
        }
        else if (currentSquare.IsCoast == false)
        {
            Debug.Log("fishing mid");
            FishMidSquare(currentSquare, catchSize);
        }
        
    }

    public void FishCoastSquare(Square currentSquare, int catchSize)
    {
        int remainingStorage = testBoat.MaxStorage - testBoat.Storage;

        Random random = new Random();
        int randomNum = 0;
        int newWeight = 0;

        int age1 = 0;
        int age2 = 0;
        int age3 = 0;

        for (int i = 0; i < catchSize; i++)
        {
            randomNum = random.Next(10);
            //take from age 1
            if (randomNum > 4)
            {
                newWeight = GetRandomWeight(1);
                if (remainingStorage >= newWeight)
                {
                    testBoat.Storage += newWeight;
                    age1++;
                }
            }
            // take form age 2
            else if (randomNum > 0)
            {
                newWeight = GetRandomWeight(1);
                if (remainingStorage >= newWeight)
                {
                    testBoat.Storage += newWeight;
                    age2++;
                }
            }
            // if it is 3
            else
            {
                newWeight = GetRandomWeight(1);
                if (remainingStorage >= newWeight)
                {
                    testBoat.Storage += newWeight;
                    age3++;
                }
            }

            remainingStorage = testBoat.MaxStorage - testBoat.Storage;
        }

        //update the populations
        GameState.AgePopList[0] -= age1;
        GameState.AgePopList[1] -= age2;
        GameState.AgePopList[2] -= age3;
    }

    public void FishMidSquare(Square currentSquare, int catchSize)
    {
        int remainingStorage = testBoat.MaxStorage - testBoat.Storage;
        

        Random random = new Random();
        int newWeight = 0;

        int totalCaught = 0;

        for (int i = 0; i < catchSize; i++)
        {
            //take from age 1
            newWeight = GetRandomWeight(4);
            if (remainingStorage >= newWeight)
            {
                testBoat.Storage += newWeight;
                totalCaught++;
            }

            //update remaining storage
            remainingStorage = testBoat.MaxStorage - testBoat.Storage;
        }

        //distribute caught numbers x
        //most fo fish are age 3, each age gets less taken off from the previous as to componsate for the fewer numbers of them in the sea
        GameState.AgePopList[2] -= GetFishPercent(GameState.AgePopList[2], totalCaught);
        GameState.AgePopList[3] -= GetFishPercent(GameState.AgePopList[3], totalCaught); 
        GameState.AgePopList[4] -= GetFishPercent(GameState.AgePopList[4], totalCaught); 
        GameState.AgePopList[5] -= GetFishPercent(GameState.AgePopList[5], totalCaught); 
        GameState.AgePopList[6] -= GetFishPercent(GameState.AgePopList[6], totalCaught); 
    }

    public int GetFishPercent(int ageNumber, int totalCaught)
    {
        //define variables
        int totalPop = 0;

        // get total population
        foreach (int i in GameState.AgePopList)
        {
            totalPop += i;
        }

        //work out what percent of fish are this age
        decimal fishPercent = ((decimal)ageNumber / (decimal)totalPop);

        //use that numhber to get the same % of the newest catch
        int fishTaken = (int)(totalCaught * fishPercent);
        //Debug.Log(fishTaken);
        return fishTaken;
    }

    public int GetRandomWeight(int age)
    {
        Random random = new Random();
        int randomWeight = 0;

        switch (age)
        {
            //cases needed for first 3 years
            case 1:
                randomWeight = random.Next(1, 3);
                break;
            case 2:
                randomWeight = random.Next(1, 4);
                break;
            case 3:
                randomWeight = random.Next(3, 6);
                break;
            default:
                randomWeight = random.Next(4, 8);
                break;
        }
        return randomWeight;
    }

    public void MoveOnClick(int direction)
    {
        // stop fishing.
        //testBoat.IsFishing = false;
        //switch used for each direction
        switch (direction)
        {
            case 1:
                testBoat.MoveUp();
                break;
            case 2:
                testBoat.MoveDown();
                break;
            case 3:
                testBoat.MoveLeft();
                break;
            case 4:
                testBoat.MoveRight();
                break;
            default:
                break;
        }

    }


    public void UpdateVesselUI()
    {
        //updating vessels
        vesselIDText.text = "ID: " + testBoat.VesselID;
        vesselFuelText.text = "Fuel: " + (testBoat.Fuel).ToString();
        vesselMaxFuelText.text = "/ " + (testBoat.MaxFuel).ToString();
        vesselProfitText.text = "Profit: " + (testBoat.Profit).ToString();
        vesselCaughtText.text = "Quota (kg): " + (testBoat.Caught).ToString();
        vesselQuotaText.text = "/ " + (testBoat.Quota).ToString();
        vesselStorageText.text = "Storage (kg) " + (testBoat.Storage).ToString();
        vesselMaxStorageText.text = "/ " + (testBoat.MaxStorage).ToString();

        if (testBoat.isAtPort())
        {
            PortPanel.SetActive(true);
        }
        else
        {
            PortPanel.SetActive(false);
        }

        //if it is a new month
        if(GameState.timeStart == 0 && GameState.day == 1)
        {
            // reset quota and boat location
            testBoat.Caught = 0;
            testBoat.Coords = "0208";
        }

        // if the boat is stranded
        if (testBoat.Fuel<5 && testBoat.isAtPort() == false)
        {
            rescuePrompt.SetActive(true);
        }
    }

    /*
    public void UpdateShoalUI()
    {
        //updating shoals
        shoalIDText1.text = "ID: " + testShoal1.ShoalID;
        shoalSizeText1.text = "Size: " + (testShoal1.Size).ToString();
        shoalAgeText1.text = "Age: " + (testShoal1.Age).ToString();

        shoalIDText2.text = "ID: " + testShoal2.ShoalID;
        shoalSizeText2.text = "Size: " + (testShoal2.Size).ToString();
        shoalAgeText2.text = "Age: " + (testShoal2.Age).ToString();

        shoalIDText3.text = "ID: " + testShoal3.ShoalID;
        shoalSizeText3.text = "Size: " + (testShoal3.Size).ToString();
        shoalAgeText3.text = "Age: " + (testShoal3.Age).ToString();
    }
    

    public void UpdateUI()
    {
        UpdateVesselUI();
        //UpdateShoalUI();
    }
    */

    public void UpdateGrid()
    {
        //wipe the grid first
        foreach (var square in GameGrid.gridSquareList)
        {
            square.GridSquare.GetComponentInChildren<Text>().text = "";
            //square.GridSquare.GetComponent<Image>().color = Color.white;
        }

        //update boat location
        foreach (var square in GameGrid.gridSquareList)
        {
            if (testBoat.Coords == square.Coords)
            {
                square.GridSquare.GetComponentInChildren<Text>().text = (square.GridSquare.GetComponentInChildren<Text>().text) + testBoat.VesselID + " ";
            }
        }

        GameGrid.MonthShuffle((int)GameState.month);

        
    }



    // Update is called once per frame
    void Update()
    {
        UpdateVesselUI();
        //UpdateUI();
        UpdateGrid();
    }
}
