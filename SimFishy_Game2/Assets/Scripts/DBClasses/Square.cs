using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    public Button GridSquare { get; set; }
    public string Coords { get; set; }
    public int FishRate { get; set; }
    public bool IsCoast { get; set; }
    public bool IsNorth { get; set; }

    public Square(Button gridSquare, string coords, bool isCoast, bool isNorth)
    {
        GridSquare = gridSquare;
        Coords = coords;
        FishRate = 0;
        IsCoast = isCoast;
        IsNorth = isNorth;
    }

    //used to update colour based on the fishing rate.
    public void UpdateColour()
    {
        switch (FishRate)
        {
            case 1:
                GridSquare.GetComponent<Image>().color = new Color(204f / 255f, 204f / 255f, 255f / 255f);
                break;
            case 2:
                GridSquare.GetComponent<Image>().color = new Color(153f / 255f, 153f / 255f, 255f / 255f);
                break;
            case 3:
                GridSquare.GetComponent<Image>().color = new Color(102f / 255f, 102f / 255f, 255f / 255f);
                break;
            case 4:
                GridSquare.GetComponent<Image>().color = new Color(0f / 255f, 0f / 255f, 255f / 255f);
                break;
            default:
                GridSquare.GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
                break;
        }
    }

}
