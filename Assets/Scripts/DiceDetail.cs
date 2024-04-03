using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceDetail : MonoBehaviour
{
    public static List<Dice> dices = new List<Dice>();
}

public class Dice
{
    public string colour;
    public int eyes;

    public int getEyes() { return eyes; }

    public string getColour() { return colour; }

    public void setEyes(int eyes) { this.eyes = eyes; }

    public void setColour(string colour) { this.colour = colour; }
}
