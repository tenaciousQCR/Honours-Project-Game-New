using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.SQLite;
using System.IO;
using DBClasses;

public class GridManager : MonoBehaviour
{
    //grid
    public Button Grid0212;
    public Button Grid0312;
    public Button Grid0412;
    public Button Grid0512;
    public Button Grid0612;

    public Button Grid0211;
    public Button Grid0311;
    public Button Grid0411;
    public Button Grid0511;
    public Button Grid0611;

    public Button Grid0110;
    public Button Grid0210;
    public Button Grid0310;
    public Button Grid0410;
    public Button Grid0510;
    public Button Grid0610;

    public Button Grid0109;
    public Button Grid0209;
    public Button Grid0309;
    public Button Grid0409;
    public Button Grid0509;
    public Button Grid0609;
    public Button Grid0709;

    public Button Grid0208;
    public Button Grid0308;
    public Button Grid0408;
    public Button Grid0508;
    public Button Grid0608;
    public Button Grid0708;
    public Button Grid0808;

    public Button Grid0207;
    public Button Grid0307;
    public Button Grid0407;
    public Button Grid0507;
    public Button Grid0607;
    public Button Grid0707;
    public Button Grid0807;

    public Button Grid0206;
    public Button Grid0306;
    public Button Grid0406;
    public Button Grid0506;
    public Button Grid0606;
    public Button Grid0706;
    public Button Grid0806;

    public Button Grid0205;
    public Button Grid0305;
    public Button Grid0405;
    public Button Grid0505;
    public Button Grid0605;
    public Button Grid0705;
    public Button Grid0805;

    public Button Grid0304;
    public Button Grid0404;
    public Button Grid0504;
    public Button Grid0604;
    public Button Grid0704;
    public Button Grid0804;

    public Button Grid0303;
    public Button Grid0403;
    public Button Grid0503;
    public Button Grid0603;

    public Button Grid0402;
    public Button Grid0502;
    public Button Grid0602;

    public Button Grid0401;
    public Button Grid0501;



    public List<string> coordList = new List<string>() {
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


    public List<Square> gridSquareList = new List<Square>();

    // Start is called before the first frame update
    void Start()
    {
        Square GS0212 = new Square(Grid0212, "0212", true, true);
        gridSquareList.Add(GS0212);
        Square GS0312 = new Square(Grid0312, "0312", false, true);
        gridSquareList.Add(GS0312);
        Square GS0412 = new Square(Grid0412, "0412", false, true);
        gridSquareList.Add(GS0412);
        Square GS0512 = new Square(Grid0512, "0512", false, true);
        gridSquareList.Add(GS0512);
        Square GS0612 = new Square(Grid0612, "0612", true, true);
        gridSquareList.Add(GS0612);

        Square GS0211 = new Square(Grid0211, "0211", true, true);
        gridSquareList.Add(GS0211);
        Square GS0311 = new Square(Grid0311, "0311", false, true);
        gridSquareList.Add(GS0311);
        Square GS0411 = new Square(Grid0411, "0411", false, true);
        gridSquareList.Add(GS0411);
        Square GS0511 = new Square(Grid0511, "0511", false, true);
        gridSquareList.Add(GS0511);
        Square GS0611 = new Square(Grid0611, "0611", true, true);
        gridSquareList.Add(GS0611);

        Square GS0110 = new Square(Grid0110, "0110", true, true);
        gridSquareList.Add(GS0110);
        Square GS0210 = new Square(Grid0210, "0210", false, true);
        gridSquareList.Add(GS0210);
        Square GS0310 = new Square(Grid0310, "0310", false, true);
        gridSquareList.Add(GS0310);
        Square GS0410 = new Square(Grid0410, "0410", false, true);
        gridSquareList.Add(GS0410);
        Square GS0510 = new Square(Grid0510, "0510", false, true);
        gridSquareList.Add(GS0510);
        Square GS0610 = new Square(Grid0610, "0610", true, true);
        gridSquareList.Add(GS0610);

        Square GS0109 = new Square(Grid0109, "0109", true, true);
        gridSquareList.Add(GS0109);
        Square GS0209 = new Square(Grid0209, "0209", false, true);
        gridSquareList.Add(GS0209);
        Square GS0309 = new Square(Grid0309, "0309", false, true);
        gridSquareList.Add(GS0309);
        Square GS0409 = new Square(Grid0409, "0409", false, true);
        gridSquareList.Add(GS0409);
        Square GS0509 = new Square(Grid0509, "0509", false, true);
        gridSquareList.Add(GS0509);
        Square GS0609 = new Square(Grid0609, "0609", false, true);
        gridSquareList.Add(GS0609);
        Square GS0709 = new Square(Grid0709, "0709", true, true);
        gridSquareList.Add(GS0709);

        Square GS0208 = new Square(Grid0208, "0208", true, true);
        gridSquareList.Add(GS0208);
        Square GS0308 = new Square(Grid0308, "0308", false, true);
        gridSquareList.Add(GS0308);
        Square GS0408 = new Square(Grid0408, "0408", false, true);
        gridSquareList.Add(GS0408);
        Square GS0508 = new Square(Grid0508, "0508", false, true);
        gridSquareList.Add(GS0508);
        Square GS0608 = new Square(Grid0608, "0608", false, true);
        gridSquareList.Add(GS0608);
        Square GS0708 = new Square(Grid0708, "0708", false, true);
        gridSquareList.Add(GS0708);
        Square GS0808 = new Square(Grid0808, "0808", true, true);
        gridSquareList.Add(GS0808);

        Square GS0207 = new Square(Grid0207, "0207", true, true);
        gridSquareList.Add(GS0207);
        Square GS0307 = new Square(Grid0307, "0307", false, true);
        gridSquareList.Add(GS0307);
        Square GS0407 = new Square(Grid0407, "0407", false, true);
        gridSquareList.Add(GS0407);
        Square GS0507 = new Square(Grid0507, "0507", false, true);
        gridSquareList.Add(GS0507);
        Square GS0607 = new Square(Grid0607, "0607", false, true);
        gridSquareList.Add(GS0607);
        Square GS0707 = new Square(Grid0707, "0707", false, true);
        gridSquareList.Add(GS0707);
        Square GS0807 = new Square(Grid0807, "0807", true, true);
        gridSquareList.Add(GS0807);

        Square GS0206 = new Square(Grid0206, "0206", true, false);
        gridSquareList.Add(GS0206);
        Square GS0306 = new Square(Grid0306, "0306", false, false);
        gridSquareList.Add(GS0306);
        Square GS0406 = new Square(Grid0406, "0406", false, false);
        gridSquareList.Add(GS0406);
        Square GS0506 = new Square(Grid0506, "0506", false, false);
        gridSquareList.Add(GS0506);
        Square GS0606 = new Square(Grid0606, "0606", false, false);
        gridSquareList.Add(GS0606);
        Square GS0706 = new Square(Grid0706, "0706", false, false);
        gridSquareList.Add(GS0706);
        Square GS0806 = new Square(Grid0806, "0806", true, false);
        gridSquareList.Add(GS0806);

        Square GS0205 = new Square(Grid0205, "0205", true, false);
        gridSquareList.Add(GS0205);
        Square GS0305 = new Square(Grid0305, "0305", false, false);
        gridSquareList.Add(GS0305);
        Square GS0405 = new Square(Grid0405, "0405", false, false);
        gridSquareList.Add(GS0405);
        Square GS0505 = new Square(Grid0505, "0505", false, false);
        gridSquareList.Add(GS0505);
        Square GS0605 = new Square(Grid0605, "0605", false, false);
        gridSquareList.Add(GS0605);
        Square GS0705 = new Square(Grid0705, "0705", false, false);
        gridSquareList.Add(GS0705);
        Square GS0805 = new Square(Grid0805, "0805", true, false);
        gridSquareList.Add(GS0805);

        Square GS0304 = new Square(Grid0304, "0304", true, false);
        gridSquareList.Add(GS0304);
        Square GS0404 = new Square(Grid0404, "0404", false, false);
        gridSquareList.Add(GS0404);
        Square GS0504 = new Square(Grid0504, "0504", false, false);
        gridSquareList.Add(GS0504);
        Square GS0604 = new Square(Grid0604, "0604", false, false);
        gridSquareList.Add(GS0604);
        Square GS0704 = new Square(Grid0704, "0704", true, false);
        gridSquareList.Add(GS0704);
        Square GS0804 = new Square(Grid0804, "0804", true, false);
        gridSquareList.Add(GS0804);

        Square GS0303 = new Square(Grid0303, "0303", true, false);
        gridSquareList.Add(GS0303);
        Square GS0403 = new Square(Grid0403, "0403", true, false);
        gridSquareList.Add(GS0403);
        Square GS0503 = new Square(Grid0503, "0503", false, false);
        gridSquareList.Add(GS0503);
        Square GS0603 = new Square(Grid0603, "0603", true, false);
        gridSquareList.Add(GS0603);

        Square GS0402 = new Square(Grid0402, "0402", true, false);
        gridSquareList.Add(GS0402);
        Square GS0502 = new Square(Grid0502, "0502", false, false);
        gridSquareList.Add(GS0502);
        Square GS0602 = new Square(Grid0602, "0602", true, false);
        gridSquareList.Add(GS0602);

        Square GS0401 = new Square(Grid0401, "0401", true, false);
        gridSquareList.Add(GS0401);
        Square GS0501 = new Square(Grid0501, "0501", true, false);
        gridSquareList.Add(GS0501);
    }

    public void MonthShuffle(int month)
    {

        switch (month)
        {
            // coldest
            case 1:
            case 2:
            case 3:
            case 4:
                foreach (var i in gridSquareList)
                {
                    // if the shoal is north, it gets a low fishing rate
                    if (i.IsNorth == true)
                    {
                        i.FishRate = 1;
                        i.UpdateColour();
                    }
                    // if south, it gets a high fishing rate
                    else
                    {
                        i.FishRate = 4;
                        i.UpdateColour();
                    }
                }
                break;
            // cold-warm
            case 5:
            case 6:
                foreach (var i in gridSquareList)
                {
                    // if the shoal is north, it gets a higher fishing rate
                    if (i.IsNorth == true)
                    {
                        i.FishRate = 3;
                        i.UpdateColour();
                    }
                    // if south, it gets a lower fishing rate
                    else
                    {
                        i.FishRate = 2;
                        i.UpdateColour();
                    }
                }
                break;
            // warmest
            case 7:
            case 8:
            case 9:
                foreach (var i in gridSquareList)
                {
                    // if the shoal is north, it gets a high fishing rate
                    if (i.IsNorth == true)
                    {
                        i.FishRate = 4;
                        i.UpdateColour();
                    }
                    // if south, it gets a low fishing rate
                    else
                    {
                        i.FishRate = 1;
                        i.UpdateColour();
                    }
                }
                break;
            // warm - cold
            case 10:
            case 11:
            case 12:
                foreach (var i in gridSquareList)
                {
                    // if the shoal is north, it gets a lower fishing rate
                    if (i.IsNorth == true)
                    {
                        i.FishRate = 2;
                        i.UpdateColour();
                    }
                    // if south, it gets a higher fishing rate
                    else
                    {
                        i.FishRate = 3;
                        i.UpdateColour();
                    }
                }
                break;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
