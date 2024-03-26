using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] Button throwingDiceButton;
    [SerializeField] GameObject diceAmountTextField;
    [SerializeField] GameObject eyesAmountTextField;
    [SerializeField] Toggle multiColoured;
    [SerializeField] GameObject colourDiceDropDownFirst;
    [SerializeField] GameObject colourDropDownSecond;
    [SerializeField] Button quitButton;

    [SerializeField] InformationController informationController;

    public void startThrowing()
    {
        // give Info of dice amount, eyes amount, colours to second scene
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
                informationController.diceAmount = amountInt;
                informationController.eyesAmount = eyesInt;

                switch (eyesInt)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        {
                            informationController.diceType = "Dice";
                            informationController.diceAmount = amount;
                            break;
                        }
                    case 7:
                    case 8:
                        {
                            informationController.diceType = "Prism";
                            informationController.diceAmount = amount;
                            break;
                        }
                    default:
                        {
                            informationController.diceType = "Multi";
                            informationController.diceAmount = amount;
                            break;
                        }
                }

                // load scene
                // https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
                SceneManager.LoadScene(1);
            } else
            {
                // stop everything and display error message somewhere...

            }
        }
    }

    public void Stop()
    {
        Debug.Log("quitting...");
    }
}
