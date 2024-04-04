using UnityEngine;

/// <summary>
/// Controller class to assess and edit dice information via an array.
/// </summary>
public class InformationController : MonoBehaviour
{
    public static int diceAmount;
    public static readonly DiceInfo[] DiceInfos = new DiceInfo[6];
}

/// <summary>
/// Sub class for the array which holds the major dice information.
/// Has getter and setter functions, but variables can be assessed freely nontheless.
/// </summary>
public class DiceInfo
{
    string _colour;
    int _eyes;
    string _type;
    bool _blocked;
    int _currentEyes;

    public int GetEyes() { return _eyes;}
    public void SetEyes(int eyes) { _eyes = eyes; }

    public void SetColour(string colour) { _colour = colour; }
    public string GetColour() { return _colour; }

    public void SetDiceType(string type) { _type = type; }
    public string GetDiceType() { return _type; }

    public bool IsBlocked() { return _blocked; }
    public void SetBlocked(bool blocked) {  _blocked = blocked; }   

    public int GetCurrentEyes() { return _currentEyes;}
    public void SetCurrentEyes(int currentEyes) { _currentEyes = currentEyes; }
}
