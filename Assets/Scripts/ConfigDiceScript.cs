using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Class/Script for the Menu Scene.
/// Controlls dice previews and settings.
/// </summary>
public class ConfigDiceScript : MonoBehaviour
{
    [SerializeField] TMP_Dropdown diceColourDropDown;
    [SerializeField] TMP_InputField diceEyesTextField;
    private int minEyes = 1;
    private int maxEyes = 120;
    [SerializeField] int diceNumber;

    // Default
    private string diceType = "Dice";
    private string diceColour = "Blue";
    private int diceEyes = 6;

    [SerializeField] Image diceFormImage;
    [SerializeField] Image diceEyesImage;
    [SerializeField] TextMeshProUGUI diceMultiEye;

    /// <summary>
    /// Start-Function to initialize controller information and to add listeners.
    /// </summary>
    private void Start()
    {
        InformationController.diceInfos[diceNumber] = new DiceInfo();

        // Setup Default
        diceFormImage.sprite = Resources.Load<Sprite>("Images/" + diceType + "/" + diceColour);
        InformationController.diceInfos[diceNumber].colour = diceColour;
        OnTextValueChanged(diceEyes.ToString());

        diceEyesTextField.onValueChanged.AddListener(OnTextValueChanged);

        diceColourDropDown.onValueChanged.AddListener(
            delegate {
                OnAmountDropDownValueChanged(diceColourDropDown);
            });
    }

    /// <summary>
    /// Function that gets called by changing values of the eyes TextField.
    /// Changes the preview display of the dice.
    /// A default is set if the input is not valid.
    /// </summary>
    /// <param name="newText">New input in the TextField</param>
    public void OnTextValueChanged(string newText)
    {
        diceMultiEye.gameObject.SetActive(false);
        diceEyesImage.gameObject.SetActive(true);

        // Change Image depending on String...
        // https://discussions.unity.com/t/how-to-get-text-from-textmeshpro-input-field/215584
        bool successDiceAmount = int.TryParse(newText, out var amounteyes);
        if (successDiceAmount)
        {
            int amountInt = int.Parse(newText);
            Debug.Log("amountInt: " + amountInt.ToString());
            // Warum neuer Parse statt amounteyes? -> In einem StackOverflow stand, dass bei "10" "1" rauskommt, also lieber safe...
            if (amounteyes > maxEyes || amounteyes < minEyes)
            {
                Debug.Log("Not a valid input! Setting Default");
                InformationController.diceInfos[diceNumber].setEyes(6);
                diceType = "Dice";
                diceEyesImage.sprite = Resources.Load<Sprite>("Images/" + diceType + "/Eyes/6");
            } else
            {
                if (amounteyes > 6 && amounteyes < 9)
                {
                    diceType = "Prism";
                }
                else if (amounteyes > 8)
                {
                    diceType = "Multi";
                }
                else 
                {
                    diceType = "Dice";
                }

                InformationController.diceInfos[diceNumber].setType(diceType);

                // set preview image and dice info depending on value...
                InformationController.diceInfos[diceNumber].setEyes(amounteyes);

                if (diceType != "Multi")
                {
                    // Initialize eye image
                    diceFormImage.sprite = Resources.Load<Sprite>("Images/" + diceType + "/" + diceColour);
                    diceEyesImage.sprite = Resources.Load<Sprite>("Images/" + diceType + "/Eyes/" + amounteyes.ToString());
                } else
                {
                    diceEyesImage.gameObject.SetActive(false);
                    diceMultiEye.gameObject.SetActive(true);
                    diceMultiEye.text = newText;
                }
            }
        }
    }

    /// <summary>
    /// Function that gets called by the listener when the dropdown value is changed.
    /// Default for the dice is "Blue".
    /// </summary>
    /// <param name="colourDropDown">The Dropdown-Instance (Object)</param>
    public void OnAmountDropDownValueChanged(TMP_Dropdown colourDropDown)
    {
         // Set preview image and dice info depending on dropdown value...
        int selectedIndex = colourDropDown.value;
        diceColour = colourDropDown.options[selectedIndex].text;

        switch (diceColour)
        {
            case "Blau": 
                diceColour = "Blue";
                break;
            case "Rot":
                diceColour = "Red";
                break;
            case "Grün":
                diceColour = "Green";
                break;
            case "Gelb":
                diceColour = "Yellow";
                break;
            case "Grau":
                diceColour = "Grey";
                break;
            // redundant aber für die Vollständigkeit
            case "Pink":
                diceColour = "Pink";
                break;
            case "Weiß":
                diceColour = "White";
                break;
            // Dürfte nie auftreten
            default:
                diceColour = "Blue";
                break;
        }

        InformationController.diceInfos[diceNumber].setColour(diceColour);

        // Initialize color
        diceFormImage.sprite = Resources.Load<Sprite>("Images/" + diceType + "/" + diceColour);
    }
}
