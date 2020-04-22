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

public class StateManager : MonoBehaviour
{
    //date and time display
    public Text timeText;
    public Text dateText;

    //skip time buttons 
    public Button dayBtn;
    public Button monthBtn;

    // text fields
    public Text monthTitleText;
    public Text minTempText;
    public Text maxTempText;
    public Text monthDescText;

    // populations
    public Text TotalPopText;
    public Text Age1PopText;
    public Text Age2PopText;
    public Text Age3PopText;
    public Text Age4PopText;
    public Text Age5PopText;
    public Text Age6PopText;
    public Text RestPopText;
    public Text ConservationText;
    public Text ConservationTitle;

    //panels
    public GameObject MonthPopup;

    // values that are saved

    //date and time default new game values
    public float timeStart { get; set; } = 0;
    public float day { get; set; } = 1;
    public float month { get; set; } = 1;
    public float year { get; set; } = 2020;

    // GAME STATE VALUES
    public string username { get; set; } = "TEST";
    public float profit { get; set; } = 0;
    public float loss { get; set; } = 0;

    public List<int> AgePopList { get; set; }

    // on start 
    void Start()
    {
        // disable popups by default
        MonthPopup.SetActive(false);

        //set timer
        timeText.text = Mathf.Round(timeStart).ToString() + ":00";
        //set date
        dateText.text = day + "/" + month + "/" + year;

        // use month popup by default atm -------- TODO add check for if its the month start etc
        ActivateMonthPopup();

        //enable buttons
        dayBtn.onClick.AddListener(NextDay);
        monthBtn.onClick.AddListener(NextMonth);

        AgePopList = new List<int>() { 1000000, 600000, 400000, 200000, 80000, 20000, 2000 };

        StatUpdate();
    }

    public void ResetGame()
    {
        timeStart = 0;
        day = 1;
        month = 1;
        year = 2020;

        username = "TEST";
        profit = 0;
        loss = 0;

        AgePopList = new List<int>() { 1000000, 600000, 400000, 200000, 80000, 20000, 2000 };
    }

    public void UpdateDB(SQLiteConnection connection)
    {
        SQLiteCommand cmnd = connection.CreateCommand();
        cmnd.CommandText =
            "UPDATE gameState"
            + " SET username = '" + username
            + "', profit = " + profit
            + ", loss = " + loss
            + ", timeStart = " + timeStart
            + ", day = " + day
            + ", month = " + month
            + ", year = " + year
            + " WHERE gameID = 'game1'";
        cmnd.ExecuteNonQuery();


        UpdateDBAge(connection, AgePopList[0], 1);
        UpdateDBAge(connection, AgePopList[1], 2);
        UpdateDBAge(connection, AgePopList[2], 3);
        UpdateDBAge(connection, AgePopList[3], 4);
        UpdateDBAge(connection, AgePopList[4], 5);
        UpdateDBAge(connection, AgePopList[5], 6);
        UpdateDBAge(connection, AgePopList[6], 7);
     

    }

    public void UpdateDBAge(SQLiteConnection connection, int pop, int age)
    {
        SQLiteCommand cmnd = connection.CreateCommand();
        cmnd.CommandText = "UPDATE shoalByAge SET population = " + pop + " WHERE age = "+ age;
        cmnd.ExecuteNonQuery();

    }

    public void UpdateClass(SQLiteConnection connection)
    {
        SQLiteCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM gameState WHERE gameID = 'game1'";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            //gameID = reader[0].ToString();
            username = reader[1].ToString();
            profit = int.Parse(reader[2].ToString());
            loss = int.Parse(reader[3].ToString());
            timeStart = int.Parse(reader[4].ToString());
            day = int.Parse(reader[5].ToString());
            month = int.Parse(reader[6].ToString());
            year = int.Parse(reader[7].ToString());
            //Debug.Log(Application.persistentDataPath);
        }

        UpdateClassAge(connection, 1);
        UpdateClassAge(connection, 2);
        UpdateClassAge(connection, 3);
        UpdateClassAge(connection, 4);
        UpdateClassAge(connection, 5);
        UpdateClassAge(connection, 6);
        UpdateClassAge(connection, 7);

    }

    public void UpdateClassAge(SQLiteConnection connection, int age)
    {
        SQLiteCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM shoalByAge WHERE age = " + age;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            AgePopList[(age-1)] = int.Parse(reader[1].ToString());

        }
    }

    public void NextMonth()
    {
        timeStart = 0;
        timeText.text = Mathf.Round(timeStart).ToString() + ":00";

        // -------------- new month/year code
        switch (month)
        {
            //if it is december
            case 12:
                //set next year
                day = 1;
                month = 1;
                year++;
                Purge();
                ActivateMonthPopup(); // ---------------- replace with year
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 2:
                //next month is spawning month
                day = 1;
                month++;
                ActivateMonthPopup();
                fishAge();
                Purge();
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
                day = 1;
                month++;
                Purge();
                ActivateMonthPopup();
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                day = 1;
                month++;
                Purge();
                ActivateMonthPopup();
                dateText.text = day + "/" + month + "/" + year;
                break;
            default:
                Debug.Log("error with NextMonth() method");
                break;
        }
    }
    //methods
    public void NextDay()
    {
        timeStart = 0;
        timeText.text = Mathf.Round(timeStart).ToString() + ":00";

        // -------------- new month/year code
        switch (month)
        {
            //if it is december
            case 12:
                //either next day or next month and year
                if (day < 31)
                {
                    day++;
                }
                else if (day == 31)
                {
                    day = 1;
                    month = 1;
                    year++;
                    Purge();
                    ActivateMonthPopup(); // ------------ replace with year popup
                }
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 2:
                //either next day or next month and year
                if (day < 28)
                {
                    day++;
                }
                else if (day == 28)
                {
                    day = 1;
                    month++;
                    Purge();
                    ActivateMonthPopup();
                }
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
                //either next day or next month
                if (day < 31)
                {
                    day++;
                }
                else if (day == 31)
                {
                    day = 1;
                    month++;
                    fishAge();
                    Purge();
                    ActivateMonthPopup();
                }
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                //either next day or next month and year
                if (day < 30)
                {
                    day++;
                }
                else if (day == 30)
                {
                    day = 1;
                    month++;
                    Purge();
                    ActivateMonthPopup();
                }
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            default:
                Debug.Log("error with NextDay() method");
                break;
        }
    }

    public void ActivateMonthPopup()
    {

        //switch statement checks the month and edits the text accordingly
        switch (month)
        {
            // January
            case 1:
                //set the text on the popup
                // 
                monthTitleText.text = "January";
                minTempText.text = (6.5).ToString();
                maxTempText.text = (8.6).ToString();
                monthDescText.text = "January is one of the coldest months of the year in terms of sea temperatures. We see the majority of fish in the south due to these temperatures.";

                break;

            // February
            case 2:
                //set the text on the popup
                monthTitleText.text = "February";
                minTempText.text = (6.1).ToString();
                maxTempText.text = (7.9).ToString();
                monthDescText.text = "February is one of the coldest months of the year in terms of sea temperatures. We see the majority of fish in the south due to these temperatures.";
                break;

            // March
            case 3:
                //set the text on the popup
                monthTitleText.text = "March";
                minTempText.text = (5.9).ToString();
                maxTempText.text = (7.9).ToString();
                monthDescText.text = "March is one of the coldest months of the year in terms of sea temperatures. We see the majority of fish in the south due to these temperatures. Also: this month is spawning season! Fish population increased.";
                break;

            // April
            case 4:
                //set the text on the popup
                monthTitleText.text = "April";
                minTempText.text = (6.8).ToString();
                maxTempText.text = (8.9).ToString();
                monthDescText.text = "April is one of the coldest months of the year in terms of sea temperatures. We see the majority of fish in the south due to these temperatures.";
                break;

            // May
            case 5:
                //set the text on the popup
                monthTitleText.text = "May";
                minTempText.text = (8.1).ToString();
                maxTempText.text = (10.4).ToString();
                monthDescText.text = "Things start to heat up in May, and fish start to move north to colder water.";
                break;

            // June
            case 6:
                //set the text on the popup
                monthTitleText.text = "June";
                minTempText.text = (9.8).ToString();
                maxTempText.text = (13.3).ToString();
                monthDescText.text = "Things still heat up in June, and fish contine to move north to colder water.";
                break;

            // July
            case 7:
                //set the text on the popup
                monthTitleText.text = "July";
                minTempText.text = (11.2).ToString();
                maxTempText.text = (15.5).ToString();
                monthDescText.text = "Its July and Summer is here! With that, most fish have moved north to spend the next few months in the colder waters.";
                break;

            // August
            case 8:
                //set the text on the popup
                monthTitleText.text = "August";
                minTempText.text = (12).ToString();
                maxTempText.text = (15).ToString();
                monthDescText.text = "August continues to bring more high temperatures! With that, most fish have moved north to spend the next few months in the colder waters.";
                break;

            // September
            case 9:
                //set the text on the popup
                monthTitleText.text = "September";
                minTempText.text = (12.1).ToString();
                maxTempText.text = (14.2).ToString();
                monthDescText.text = "September is the last month of heat before we see more fish moving down south! For now, most fish have still moved north to spend the next few months in the colder waters.";
                break;

            // October
            case 10:
                //set the text on the popup
                monthTitleText.text = "October";
                minTempText.text = (11.3).ToString();
                maxTempText.text = (13.3).ToString();
                monthDescText.text = "October sees temperatures start to decline! Fish start to go to the warmer waters in the south, slightly more fish can be found there.";
                break;

            // November
            case 11:
                //set the text on the popup
                monthTitleText.text = "November";
                minTempText.text = (10).ToString();
                maxTempText.text = (12.2).ToString();
                monthDescText.text = "November is here and temperatures still decline! Fish continue to go to the warmer waters in the south, slightly more fish can be found there.";
                break;

            // December
            case 12:
                //set the text on the popup
                monthTitleText.text = "December";
                minTempText.text = (7.6).ToString();
                maxTempText.text = (10.7).ToString();
                monthDescText.text = "December continues the temperature decline and its the last month of the year. Fish continue looking for warmer waters, slightly more fish can be found there.";
                break;
        }
        //display popup
        MonthPopup.SetActive(true);
    }

    public void StatUpdate()
    {
        int totalPop = 0;

        foreach (int i in AgePopList)
        {
            totalPop += i;
        }

        TotalPopText.text = totalPop.ToString();
        Age1PopText.text = AgePopList[0].ToString();
        Age2PopText.text = AgePopList[1].ToString();
        Age3PopText.text = AgePopList[2].ToString();
        Age4PopText.text = AgePopList[3].ToString();
        Age5PopText.text = AgePopList[4].ToString();
        Age6PopText.text = AgePopList[5].ToString();
        RestPopText.text = AgePopList[6].ToString();

        ConservationStatus(totalPop);
    }

    public void ConservationStatus(int totalPopulation)
    {
        if(totalPopulation > 10000000)
        {
            ConservationTitle.text = "LC";
            ConservationText.text = "Least Concern";
        }
        else if (totalPopulation > 5000000)
        {
            ConservationTitle.text = "NT";
            ConservationText.text = "Near Threatened";
        }
        else if (totalPopulation > 1000000)
        {
            ConservationTitle.text = "VU";
            ConservationText.text = "Vulnerable";
        }
        else if (totalPopulation > 100000)
        {
            ConservationTitle.text = "EN";
            ConservationText.text = "Endangered";
        }
        else if (totalPopulation > 1000)
        {
            ConservationTitle.text = "CR";
            ConservationText.text = "Critically Endangered";
        }
        else
        {
            ConservationTitle.text = "E";
            ConservationText.text = "Considered Exctinct";
        }
    }



    public void fishAge()
    {
        int oldAge1 = AgePopList[0];
        int oldAge2 = AgePopList[1];
        int oldAge3 = AgePopList[2];
        int oldAge4 = AgePopList[3];
        int oldAge5 = AgePopList[4];
        int oldAge6 = AgePopList[5];
        int oldAge7 = AgePopList[6];

        int matureFish = ((oldAge3/2)+(oldAge4 + oldAge5 + oldAge6 + oldAge7))/2;

        int newSpawn = matureFish * 7;

        int newOld = oldAge7 + oldAge6;

        AgePopList = new List<int>() { newSpawn, oldAge1, oldAge2, oldAge3, oldAge4, oldAge5, oldAge6, newOld };
    }

    public void Purge()
    {
        // using random inputs in certain ranges, a certain percentage of fish will die in each shoal
        Random random = new Random();
        // age 1
        int randomAge1 = random.Next(2, 5);
        AgePopList[0] = PurgeAge(AgePopList[0], randomAge1);

        // age 2
        int randomAge2 = random.Next(1, 4);
        AgePopList[1] = PurgeAge(AgePopList[1], randomAge2);

        // age 3
        int randomAge3 = random.Next(2, 6);
        AgePopList[2] = PurgeAge(AgePopList[2], randomAge3);

        // age 4
        int randomAge4 = random.Next(3, 7);
        AgePopList[3] = PurgeAge(AgePopList[3], randomAge4);

        // age 5
        int randomAge5 = random.Next(6, 10);
        AgePopList[4] = PurgeAge(AgePopList[4], randomAge5);

        // age 6
        int randomAge6 = random.Next(10, 13);
        AgePopList[5] = PurgeAge(AgePopList[5], randomAge6);

        // age 7
        int randomAge7 = random.Next(13, 16);
        AgePopList[6] = PurgeAge(AgePopList[6], randomAge7);
    }

    public int PurgeAge(int agePop, int random)
    {
        int newPop = 0;
        switch (random)
        {
            case 1:
                newPop = (int)(agePop * 0.98);
                break;
            case 2:
                newPop = (int)(agePop * 0.97);
                break;
            case 3:
                newPop = (int)(agePop * 0.96);
                break;
            case 4:
                newPop = (int)(agePop * 0.95);
                break;
            case 5:
                newPop = (int)(agePop * 0.94);
                break;
            case 6:
                newPop = (int)(agePop * 0.93);
                break;
            case 7:
                newPop = (int)(agePop * 0.92);
                break;
            case 8:
                newPop = (int)(agePop * 0.91);
                break;
            case 9:
                newPop = (int)(agePop * 0.9);
                break;
            case 10:
                newPop = (int)(agePop * 0.87);
                break;
            case 11:
                newPop = (int)(agePop * 0.85);
                break;
            case 12:
                newPop = (int)(agePop * 0.83);
                break;
            case 13:
                newPop = (int)(agePop * 0.73);
                break;
            case 14:
                newPop = (int)(agePop * 0.7);
                break;
            case 15:
                newPop = (int)(agePop * 0.67);
                break;
            default:
                break;
        }
        return newPop;
    }

    // Update is called once per frame
    void Update()
    {
        // update population 
        //
        StatUpdate();
        //if the time is less than 11pm
        if (timeStart < 23)
        {
            timeStart += Time.deltaTime;
            timeText.text = Mathf.Round(timeStart).ToString() + ":00";

        }
        //if it is 23, go back to 00 to start a new day
        else
        {
            NextDay();
        }

        
    }


}
