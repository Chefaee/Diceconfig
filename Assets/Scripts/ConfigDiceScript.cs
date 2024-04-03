using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigDiceScript : MonoBehaviour
{
    [SerializeField] TMP_Dropdown diceColourDropDown;
    [SerializeField] TMP_InputField diceEyesTextField;
    private int minEyes = 1;
    private int maxEyes = 120;
    [SerializeField] int diceNumber;

    private string diceType = "Dice";
    private string diceColour = "Blue";

    [SerializeField] Image diceFormImage;
    [SerializeField] Image diceEyesImage;
    [SerializeField] TextMeshProUGUI diceMultiEye;

    private void Start()
    {
        InformationController.diceInfos[diceNumber].colour = diceColour;
        InformationController.diceInfos[diceNumber].eyes = minEyes;

        diceEyesTextField.onValueChanged.AddListener(OnTextValueChanged);

        diceColourDropDown.onValueChanged.AddListener(
            delegate {
                OnAmountDropDownValueChanged(diceColourDropDown);
            });
    }

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
            // Warum neuer Parse statt amounteyes? -> In einem StackOverflow stand, dass bei "10" "1" rauskommt, also lieber safe...
            if (amounteyes > maxEyes || amounteyes < minEyes)
            {
                Debug.Log("Not a valid input!");
            } else
            {
                Debug.Log("Input: " + newText);
                if (amounteyes > 6)
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
                Debug.Log("DiceType: " + diceType);

                // set preview image and dice info depending on value...
                InformationController.diceInfos[diceNumber].setEyes(amounteyes);

                if (diceType != "Multi")
                {
                    Debug.Log("Eye path: " + "Images/" + diceType + "/Eyes/" + amounteyes);

                    // Initialize eye image
                    Sprite spriteEyes = Resources.Load<Sprite>("Images/" + diceType + "/Eyes/" + amounteyes.ToString());
                    diceEyesImage.sprite = spriteEyes;
                } else
                {
                    diceEyesImage.gameObject.SetActive(false);
                    diceMultiEye.text = newText;
                    diceMultiEye.gameObject.SetActive(true);
                }
            }
          
        }
    }

    public void OnAmountDropDownValueChanged(TMP_Dropdown colourDropDown)
    {
         // Set preview image and dice info depending on dropdown value...
        int selectedIndex = colourDropDown.value;
        diceColour = colourDropDown.options[selectedIndex].text;
        Debug.Log("diceNumber: " + diceNumber);
        // InformationController.diceInfos[diceNumber].setColour(diceColour);

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

            // redundant aber für die Vollständigkeit ig
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

        // Initialize color
        Sprite spriteForm = Resources.Load<Sprite>("Images/" + diceType + "/" + diceColour);
        diceFormImage.sprite = spriteForm;
    }
}
