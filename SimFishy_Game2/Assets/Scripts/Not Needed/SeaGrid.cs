using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.SQLite;
using System.IO;
using DBClasses;

public class SeaGrid : MonoBehaviour
{
    //grid
    public Button Grid0101 { get; set; }
    public Button Grid0102 { get; set; }
    public Button Grid0103 { get; set; }

    public Button Grid0201 { get; set; }
    public Button Grid0202 { get; set; }
    public Button Grid0203 { get; set; }

    public Button Grid0301 { get; set; }
    public Button Grid0302 { get; set; }
    public Button Grid0303 { get; set; }

    // lists
    public List<ShoalDB> ShoalList { get; set; }
    public List<Button> buttonList { get; set; }


    // vessel
    public Vessel Boat { get; set; }

    public SeaGrid(Button grid0101, Button grid0102, Button grid0103, Button grid0201, Button grid0202, Button grid0203, Button grid0301, Button grid0302, Button grid0303, List<ShoalDB> shoalList, Vessel boat)
    {
        Grid0101 = grid0101;
        Grid0102 = grid0102;
        Grid0103 = grid0103;
        Grid0201 = grid0201;
        Grid0202 = grid0202;
        Grid0203 = grid0203;
        Grid0301 = grid0301;
        Grid0302 = grid0302;
        Grid0303 = grid0303;
        ShoalList = shoalList;
        Boat = boat;
    }



    // list
    List<Square> gridSquareList = new List<Square>();


    // Start is called before the first frame update
    void Start()
    {
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
    }

    ////////////////////////////////// grid click ////////////////////////////
    public void GridSquareOnClick(string coords)
    {
        foreach (var i in ShoalList)
        {
            if (i.Coords == coords)
            {
                i.FishShoal(Boat);
            }
        }

    }

    public void UpdateGrid()
    {
        //wipe the grid first
        foreach (var square in gridSquareList)
        {
            square.GridSquare.GetComponentInChildren<Text>().text = "";
        }

        //update with shoal ids
        foreach (var i in ShoalList)
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
}
