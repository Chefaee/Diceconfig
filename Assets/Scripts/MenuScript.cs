using System.Collections.Generic;
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
    private int _diceAmount;
    [SerializeField] List<GameObject> diceObjects;

    [SerializeField] Button throwDiceButton;
    [SerializeField] Button quitButton;

    /// <summary>
    /// Starts the GameScene.
    /// </summary>
    private static void SwitchSceneToGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Quits the Game.
    /// </summary>
    private static void Quit()
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
        throwDiceButton.onClick.AddListener(SwitchSceneToGame);
        quitButton.onClick.AddListener(Quit);

        // default
        // https://stackoverflow.com/questions/18863187/how-can-i-loop-through-a-listt-and-grab-each-item
        foreach (GameObject obj in diceObjects)
        {
            obj.SetActive(false);
        }

        int selectedIndex = diceAmountDropDown.value;
        _diceAmount = selectedIndex + 1;
        InformationController.diceAmount = _diceAmount;

        for (int i = 0; i < _diceAmount; i++)
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
    void OnAmountDropDownValueChanged(TMP_Dropdown amountDropDown)
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
        _diceAmount = selectedIndex+1;
        InformationController.diceAmount = _diceAmount;

        for (int i = 0; i < _diceAmount; i++)
        {
            diceObjects[i].SetActive(true);
        }
    }
}
