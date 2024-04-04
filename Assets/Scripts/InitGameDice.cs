using TMPro;
using UnityEngine;
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

    private string _diceType;

    /// <summary>
    /// Start-Function to initialize the dice' display.
    /// </summary>
    void Start()
    {
        _diceType = InformationController.DiceInfos[diceNumber].GetDiceType();
        SetNewForm(InformationController.DiceInfos[diceNumber].GetColour());
        SetNewEye(InformationController.DiceInfos[diceNumber].GetEyes());
    }

    /// <summary>
    /// Function to change the eye image.
    /// </summary>
    /// <param name="newEyes">int of the eye</param>
    public void SetNewEye(int newEyes)
    {
        if (_diceType == "Multi")
        {
            diceEyesImage.gameObject.SetActive(false);
            multiEyeTMP.gameObject.SetActive(true);
            multiEyeTMP.text = newEyes.ToString();
        }
        else
        {
            multiEyeTMP.gameObject.SetActive(false);
            diceEyesImage.gameObject.SetActive(true);
            // change eye image
            diceEyesImage.sprite = Resources.Load<Sprite>("Images/" + _diceType + "/Eyes/" + newEyes);
        }
    }

    /// <summary>
    /// Function to set the dice colour.
    /// </summary>
    /// <param name="newColour">String for the new colour of the dice</param>
    void SetNewForm(string newColour)
    {
        if (mini) newColour = "Blue";

        // change form image
        diceFormImage.sprite = Resources.Load<Sprite>("Images/" + _diceType + "/" + newColour);

    }

    /// <summary>
    /// Function for the statistics. Sets the Text next to the images.
    /// </summary>
    /// <param name="rollAmount">string of the rolled amount (converted int to string)</param>
    public void SetRollAmountTMP(string rollAmount)
    {
        if (rollAmountTMP != null) rollAmountTMP.text = " x" + rollAmount;
    }

    /// <summary>
    /// Function to block/chain dice, to keep the current eyes.
    /// Activates the chains image.
    /// </summary>
    public void BlockDice()
    {
        if (chains != null && chains.activeSelf)
        {
            chains.gameObject.SetActive(false);
            // add info in Controller
            InformationController.DiceInfos[diceNumber].SetBlocked(false);
        } 
        else
        {
            if (chains != null) chains.gameObject.SetActive(true);
            // add info in controller
            InformationController.DiceInfos[diceNumber].SetBlocked(true);
        }
    }
}
