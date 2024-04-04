using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller class to assess and edit dice information via an array.
/// </summary>
public class InformationController : MonoBehaviour
{
    public static string diceType;
    public static int diceAmount;
    public static List<string> diceColours;
    public static int eyesAmount;

    public static DiceInfo[] diceInfos = new DiceInfo[6];
}

/// <summary>
/// Sub class for the array which holds the major dice informations.
/// Has getter and setter functions, but variables can be assessed freely nontheless.
/// </summary>
public class DiceInfo
{
    public string colour;
    public int eyes;
    public string type;
    public bool blocked;
    public int currentEyes;

    public int getEyes() { return eyes; }

    public string getColour() { return colour; }

    public void setEyes(int eyes) { this.eyes = eyes; }

    public void setColour(string colour) { this.colour = colour; }

    public void setType(string type) { this.type = type; }

    public bool isBlocked() { return blocked; }

    public void setBlocked(bool blocked) {  this.blocked = blocked; }   

    public int getCurrentEyes() { return currentEyes;}

    public void setCurrentEyes(int currentEyes) { this.currentEyes = currentEyes; }
}
