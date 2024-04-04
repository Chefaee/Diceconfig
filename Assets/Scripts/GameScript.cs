using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Major Class/Script for the Game Scene. Controlls all buttons and dice displays.
/// </summary>
public class GameScript : MonoBehaviour
{
    private int diceAmount;
    [SerializeField] List<GameObject> diceObjects;
    [SerializeField] List<GameObject> miniDiceStatistics;
    [SerializeField] TextMeshProUGUI roundTMP;
    [SerializeField] TextMeshProUGUI sumTMP;

    [SerializeField] Button newThrowButton;
    [SerializeField] Button backToMenuButton;

    private int round = 0;

    private Dictionary<int, int> counts;

    /// <summary>
    /// Start-Function to set default dice images.
    /// </summary>
    private void Start()
    {
        backToMenuButton.onClick.AddListener(switchSceneToMenu);
        newThrowButton.onClick.AddListener(newThrow);

        diceAmount = InformationController.diceAmount;

        // default
        // https://stackoverflow.com/questions/18863187/how-can-i-loop-through-a-listt-and-grab-each-item
        foreach (GameObject obj in diceObjects)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in miniDiceStatistics)
        {
            obj.SetActive(false);
        }

        for (int i = 0; i < diceAmount; i++)
        {
            diceObjects[i].SetActive(true);
        }
    }

    /// <summary>
    /// Switches to Menu Scene after button was clicked/pressed.
    /// </summary>
    private void switchSceneToMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Funciton to start a new roll/throw after the button was clicked/pressed.
    /// Respects chained/blocked dice.
    /// </summary>
    private void newThrow()
    {
        counts = new Dictionary<int, int>(); ;
        round++;
        int sum = 0;
        for (int i = 0; i < diceAmount; i++)
        {
            if (!InformationController.diceInfos[i].isBlocked())
            {
                int max = InformationController.diceInfos[i].eyes;
                // https://stackoverflow.com/questions/3975290/produce-a-random-number-in-a-range-using-c-sharp
                System.Random rnd = new System.Random();
                int dice = rnd.Next(1, max + 1);
                sum += dice;
                InitGameDice initGameDiceScript = diceObjects[i].GetComponent<InitGameDice>();
                if (initGameDiceScript != null)
                {
                    initGameDiceScript.setNewEye(dice);
                }
                InformationController.diceInfos[i].setCurrentEyes(dice);

                if (counts.ContainsKey(dice)) counts[dice]++;
                else counts[dice] = 1;
            } 
            else
            {
                int blockedEyes = InformationController.diceInfos[i].getCurrentEyes();
                Debug.Log("Blocked Eyes/ current Eyes: " + blockedEyes);

                sum += blockedEyes;
                if (counts.ContainsKey(blockedEyes)) counts[blockedEyes]++;
                else counts[blockedEyes] = 1;
            }

        }
        sumTMP.text = sum.ToString();
        roundTMP.text = round.ToString();

        foreach (GameObject obj in miniDiceStatistics)
        {
            obj.SetActive(false);
        }

        // edit statistics here
        List<KeyValuePair<int, int>> keyValuePairs = new List<KeyValuePair<int, int>>(counts);
        for (int i = 0; i < keyValuePairs.Count; i++)
        {
            int augenzahl = keyValuePairs[i].Key;
            int anzahl = keyValuePairs[i].Value;

            InitGameDice initGameDiceScript = miniDiceStatistics[i].GetComponent<InitGameDice>();
            if (initGameDiceScript != null)
            {
                miniDiceStatistics[i].SetActive(true);
                initGameDiceScript.setNewEye(augenzahl);
                initGameDiceScript.setRollAmountTMP(anzahl.ToString());
            }
        }
    }
}
