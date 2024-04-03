using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] Button throwingDiceButton;
    // https://stackoverflow.com/questions/52376881/how-to-access-a-unitys-textmesh-pro-dropdown-component-via-code
    [SerializeField] TMP_Dropdown diceAmountDropDown;
    [SerializeField] Button quitButton;
    private int diceAmount;
    [SerializeField] List<GameObject> diceObjects;

    private void Start()
    {
        // default
        // https://stackoverflow.com/questions/18863187/how-can-i-loop-through-a-listt-and-grab-each-item
        foreach (GameObject obj in diceObjects)
        {
            obj.SetActive(false);
        }

        int selectedIndex = diceAmountDropDown.value;
        diceAmount = selectedIndex + 1;
        InformationController.diceAmount = diceAmount;

        for (int i = 0; i < diceAmount; i++)
        {
            diceObjects[i].SetActive(true);
        }

        // https://docs.unity3d.com/2018.3/Documentation/ScriptReference/UI.Dropdown-onValueChanged.html
        diceAmountDropDown.onValueChanged.AddListener(
            delegate { OnAmountDropDownValueChanged(diceAmountDropDown);
            });
    }

    public void OnAmountDropDownValueChanged(TMP_Dropdown amountDropDown)
    {
        // default
        // https://stackoverflow.com/questions/18863187/how-can-i-loop-through-a-listt-and-grab-each-item
        foreach (GameObject obj in diceObjects)
        {
            obj.SetActive(false);
        }

        // Enable Objects depending on Dropdown value...
        int selectedIndex = amountDropDown.value;
        /*
         * eig müsste der Wert so ausgelesen werden:
         * string selectedOption = dropdown.options[selectedIndex].text;
         * aber da es bei mir eh nur statische integer sind, überspringe ich das mal
         */
        diceAmount = selectedIndex+1;
        Debug.Log("Dice amount:" + diceAmount);
        InformationController.diceAmount = diceAmount;

        for (int i = 0; i < diceAmount; i++)
        {
            diceObjects[i].SetActive(true);
        }
    }

    // doku - auslesen von Textfeld
    /*
     * // Change Image depending on String...
        // https://discussions.unity.com/t/how-to-get-text-from-textmeshpro-input-field/215584
        bool successDiceAmount = int.TryParse(newText, out var amounteyes);
        if (successDiceAmount) {
            int amountInt = int.Parse(newText);
            // Warum neuer Parse statt amounteyes? -> In einem StackOverflow stand, dass bei "10" "1" rauskommt, also lieber safe...
            switch (amounteyes) { 
                case 0:
                    Debug.Log("0 detected, no valid input");
                break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    Debug.Log("Valid input between ")

            }
        }
     */

    public void startThrowing()
    {
        // Switch Scenes...
        SceneManager.LoadScene(1);
    }

    public void Stop()
    {
        Debug.Log("quitting...");
    }

    // alt - Dokumentation
    /*
     * // give Info of dice amount, eyes amount, colours to second scene
        // https://discussions.unity.com/t/how-to-get-text-from-textmeshpro-input-field/215584
        string amountStr = diceAmountTextField.GetComponent<TMP_InputField>().text;
        bool successAmount = int.TryParse(amountStr, out var amount);

        string eyesStr = eyesAmountTextField.GetComponent<TMP_InputField>().text;
        bool successEyes = int.TryParse(eyesStr, out var amounteyes);

        if (successAmount && successEyes)
        {
            int amountInt = int.Parse(amountStr);
            int eyesInt = int.Parse(eyesStr);
            if (amountInt > 0 || eyesInt > 0)
            {
                InformationController.diceAmount = amountInt;
                InformationController.eyesAmount = eyesInt;

                switch (eyesInt)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        {
                            InformationController.diceType = "Dice";
                            InformationController.diceAmount = amount;
                            break;
                        }
                    case 7:
                    case 8:
                        {
                            InformationController.diceType = "Prism";
                            InformationController.diceAmount = amount;
                            break;
                        }
                    default:
                        {
                            InformationController.diceType = "Multi";
                            InformationController.diceAmount = amount;
                            break;
                        }
                }

                // load scene
                // https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
                SceneManager.LoadScene(1);

                Debug.Log("Dice Type:" + InformationController.diceType);
                Debug.Log("diceAmount:" + InformationController.diceAmount);
                Debug.Log("eyesAmount:" + InformationController.eyesAmount);
            } else
            {
                // stop everything and display error message somewhere...

            }
        } 
     */
}
