using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationController : MonoBehaviour
{
    public static string diceType;
    public static int diceAmount;
    public static List<string> diceColours;
    public static int eyesAmount;

    public static DiceInfo[] diceInfos = new DiceInfo[6];
}

/// <summary>
/// 
/// </summary>
public class DiceInfo
{
    public string colour;
    public int eyes;

    public int getEyes() { return eyes; }

    public string getColour() { return colour; }

    public void setEyes(int eyes) { this.eyes = eyes; }

    public void setColour(string colour) { this.colour = colour; }
}
