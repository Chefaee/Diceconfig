using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class/Script for the Menu Scene.
/// Controlls the behaviour of buttons and gives commands to the dice objects.
/// </summary>
public class MenuScript : MonoBehaviour
{
    // https://stackoverflow.com/questions/52376881/how-to-access-a-unitys-textmesh-pro-dropdown-component-via-code
    [SerializeField] TMP_Dropdown diceAmountDropDown;
    private int diceAmount;
    [SerializeField] List<GameObject> diceObjects;

    [SerializeField] Button throwDiceButton;
    [SerializeField] Button quitButton;

    /// <summary>
    /// Starts the GameScene.
    /// </summary>
    private void switchSceneToGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Quits the Game.
    /// </summary>
    private void quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    /// <summary>
    /// Start-Function to add listeners to buttons/dice amount dropdown.
    /// Deactivates the dice objects.
    /// </summary>
    private void Start()
    {
        // https://docs.unity3d.com/2019.1/Documentation/ScriptReference/UI.Button-onClick.html
        throwDiceButton.onClick.AddListener(switchSceneToGame);
        quitButton.onClick.AddListener(quit);

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

    /// <summary>
    /// Function that gets called when the dice amount dropdown's value changes.
    /// Activates the dice objects respectively to the amount.
    /// </summary>
    /// <param name="amountDropDown"></param>
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
        InformationController.diceAmount = diceAmount;

        for (int i = 0; i < diceAmount; i++)
        {
            diceObjects[i].SetActive(true);
        }
    }
}
