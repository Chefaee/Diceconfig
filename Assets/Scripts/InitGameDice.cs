using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class for GameScene.
/// Changes the display of dice images (normal, statistic).
/// </summary>
public class InitGameDice : MonoBehaviour
{
    [SerializeField] Image diceFormImage;
    [SerializeField] Image diceEyesImage;
    [SerializeField] TextMeshProUGUI multiEyeTMP;
    [SerializeField] int diceNumber;
    [SerializeField] bool mini;

    // Nullable
    [SerializeField] TextMeshProUGUI? rollAmountTMP;
    [SerializeField] GameObject? chains;

    private string diceType;

    /// <summary>
    /// Start-Function to initialize the dice's display.
    /// </summary>
    void Start()
    {
        diceType = InformationController.diceInfos[diceNumber].type;
        setNewForm(InformationController.diceInfos[diceNumber].colour);
        setNewEye(InformationController.diceInfos[diceNumber].eyes);
    }

    /// <summary>
    /// Function to change the eye image.
    /// </summary>
    /// <param name="newEyes">int of the eye</param>
    public void setNewEye(int newEyes)
    {
        // change eye image
        diceEyesImage.sprite = Resources.Load<Sprite>("Images/" + diceType + "/Eyes/" + newEyes.ToString());
    }

    /// <summary>
    /// Function to set the dice colour.
    /// </summary>
    /// <param name="newColour">String for the new colour of the dice</param>
    public void setNewForm(string newColour)
    {
        if (mini)
        {
            newColour = "Blue";
        } 

        // change form image
        diceFormImage.sprite = Resources.Load<Sprite>("Images/" + diceType + "/" + newColour);

    }

    /// <summary>
    /// Function for the statistics. Sets the Text next to the images.
    /// </summary>
    /// <param name="rollAmount">string of the rolled amount (converted int to string)</param>
    public void setRollAmountTMP(string rollAmount)
    {
        rollAmountTMP.text = " x"+rollAmount;
    }

    /// <summary>
    /// Function to block/chain dice, to keep the current eyes.
    /// Activates the chains image.
    /// </summary>
    public void blockDice()
    {
        if (chains.activeSelf)
        {
            chains.gameObject.SetActive(false);
            // add info in Controller
            InformationController.diceInfos[diceNumber].setBlocked(false);
        } 
        else 
        {
            chains.gameObject.SetActive(true);
            // add info in controller
            InformationController.diceInfos[diceNumber].setBlocked(true);
        }
    }
}
