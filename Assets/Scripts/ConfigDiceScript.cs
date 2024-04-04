using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class/Script for the Menu Scene.
/// Controlls dice previews and settings.
/// </summary>
public class ConfigDiceScript : MonoBehaviour
{
    [SerializeField] TMP_Dropdown diceColourDropDown;
    [SerializeField] TMP_InputField diceEyesTextField;
    const int MinEyes = 1;
    const int MaxEyes = 120;
    [SerializeField] int diceNumber;

    // Default
    private string _diceType = "Dice";
    private string _diceColour = "Blue";
    const int DiceEyes = 6;

    [SerializeField] Image diceFormImage;
    [SerializeField] Image diceEyesImage;
    [SerializeField] TextMeshProUGUI diceMultiEye;

    /// <summary>
    /// Start-Function to initialize controller information and to add listeners.
    /// </summary>
    private void Start()
    {
        InformationController.DiceInfos[diceNumber] = new DiceInfo();

        // Setup Default
        diceFormImage.sprite = Resources.Load<Sprite>("Images/" + _diceType + "/" + _diceColour);
        InformationController.DiceInfos[diceNumber].SetColour(_diceColour);
        OnTextValueChanged(DiceEyes.ToString());

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
    void OnTextValueChanged(string newText)
    {
        diceMultiEye.gameObject.SetActive(false);
        diceEyesImage.gameObject.SetActive(true);

        // Change Image depending on String...
        // https://discussions.unity.com/t/how-to-get-text-from-textmeshpro-input-field/215584
        bool successDiceAmount = int.TryParse(newText, out int _);
        if (!successDiceAmount) return;
        int amountEyesInt = int.Parse(newText);
        // Warum neuer Parse statt von TryParse? -> In einem StackOverflow stand,
        //  dass bei "10" "1" oder so rauskommt, also lieber safe...
        if (amountEyesInt is > MaxEyes or < MinEyes)
        {
            Debug.Log("Not a valid input! Setting Default");
            InformationController.DiceInfos[diceNumber].SetEyes(6);
            _diceType = "Dice";
            diceEyesImage.sprite = Resources.Load<Sprite>("Images/" + _diceType + "/Eyes/6");
        } else
        {
            _diceType = amountEyesInt switch
            {
                > 6 and < 9 => "Prism",
                > 8 => "Multi",
                _ => "Dice"
            };

            InformationController.DiceInfos[diceNumber].SetDiceType(_diceType);

            // set preview image and dice info depending on value...
            InformationController.DiceInfos[diceNumber].SetEyes(amountEyesInt);

            if (_diceType != "Multi")
            {
                // Initialize eye image
                diceFormImage.sprite = Resources.Load<Sprite>("Images/" + _diceType + "/" + _diceColour);
                diceEyesImage.sprite = Resources.Load<Sprite>("Images/" + _diceType + "/Eyes/" + amountEyesInt);
            } 
            else
            {
                diceFormImage.sprite = Resources.Load<Sprite>("Images/" + _diceType + "/" + _diceColour);
                diceEyesImage.gameObject.SetActive(false);
                diceMultiEye.gameObject.SetActive(true);
                diceMultiEye.text = newText;
            }
        }
    }

    /// <summary>
    /// Function that gets called by the listener when the dropdown value is changed.
    /// Default for the dice is "Blue".
    /// </summary>
    /// <param name="colourDropDown">The Dropdown-Instance (Object)</param>
    void OnAmountDropDownValueChanged(TMP_Dropdown colourDropDown)
    {
         // Set preview image and dice info depending on dropdown value...
        int selectedIndex = colourDropDown.value;
        _diceColour = colourDropDown.options[selectedIndex].text;

        _diceColour = _diceColour switch
        {
            "Blau" => "Blue",
            "Rot" => "Red",
            "Grün" => "Green",
            "Gelb" => "Yellow",
            "Grau" => "Grey",
            // redundant, aber für die Vollständigkeit
            "Pink" => "Pink",
            "Weiß" => "White",
            _ => "Blue"
        };

        InformationController.DiceInfos[diceNumber].SetColour(_diceColour);

        // Initialize color
        diceFormImage.sprite = Resources.Load<Sprite>("Images/" + _diceType + "/" + _diceColour);
    }
}
