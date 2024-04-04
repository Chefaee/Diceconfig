using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Major Class/Script for the Game Scene. Controls all buttons and dice displays.
/// </summary>
public class GameScript : MonoBehaviour
{
    private int _diceAmount;
    [SerializeField] List<GameObject> diceObjects;
    [SerializeField] List<GameObject> miniDiceStatistics;
    [SerializeField] TextMeshProUGUI roundTMP;
    [SerializeField] TextMeshProUGUI sumTMP;

    [SerializeField] Button newThrowButton;
    [SerializeField] Button backToMenuButton;

    private int _round = 0;

    private Dictionary<int, int> _counts;

    /// <summary>
    /// Start-Function to set default dice images.
    /// </summary>
    private void Start()
    {
        backToMenuButton.onClick.AddListener(SwitchSceneToMenu);
        newThrowButton.onClick.AddListener(NewThrow);

        _diceAmount = InformationController.diceAmount;

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

        for (int i = 0; i < _diceAmount; i++)
        {
            diceObjects[i].SetActive(true);
        }
    }

    /// <summary>
    /// Switches to Menu Scene after button was clicked/pressed.
    /// </summary>
    private static void SwitchSceneToMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Function to start a new roll/throw after the button was clicked/pressed.
    /// Respects chained/blocked dice.
    /// </summary>
    private void NewThrow()
    {
        _counts = new Dictionary<int, int>(); ;
        _round++;
        int sum = 0;
        for (int i = 0; i < _diceAmount; i++)
        {
            if (!InformationController.DiceInfos[i].IsBlocked())
            {
                int max = InformationController.DiceInfos[i].GetEyes();
                // https://stackoverflow.com/questions/3975290/produce-a-random-number-in-a-range-using-c-sharp
                System.Random rnd = new System.Random();
                int dice = rnd.Next(1, max + 1);
                sum += dice;
                InitGameDice initGameDiceScript = diceObjects[i].GetComponent<InitGameDice>();
                if (initGameDiceScript != null)
                {
                    initGameDiceScript.SetNewEye(dice);
                }
                InformationController.DiceInfos[i].SetCurrentEyes(dice);

                if (!_counts.TryAdd(dice, 1)) _counts[dice]++;
            } 
            else
            {
                int blockedEyes = InformationController.DiceInfos[i].GetCurrentEyes();
                Debug.Log("Blocked Eyes/ current Eyes: " + blockedEyes);

                sum += blockedEyes;
                if (!_counts.TryAdd(blockedEyes, 1)) _counts[blockedEyes]++;
            }

        }
        sumTMP.text = sum.ToString();
        roundTMP.text = _round.ToString();

        foreach (GameObject obj in miniDiceStatistics)
        {
            obj.SetActive(false);
        }

        // edit statistics here
        List<KeyValuePair<int, int>> keyValuePairs = new List<KeyValuePair<int, int>>(_counts);
        for (int i = 0; i < keyValuePairs.Count; i++)
        {
            int augenzahl = keyValuePairs[i].Key;
            int anzahl = keyValuePairs[i].Value;

            InitGameDice initGameDiceScript = miniDiceStatistics[i].GetComponent<InitGameDice>();
            if (initGameDiceScript == null) continue;
            miniDiceStatistics[i].SetActive(true);
            initGameDiceScript.SetNewEye(augenzahl);
            initGameDiceScript.SetRollAmountTMP(anzahl.ToString());
        }
    }
}
