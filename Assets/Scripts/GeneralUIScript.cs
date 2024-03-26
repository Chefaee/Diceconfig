using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUIScript : MonoBehaviour
{
    [SerializeField] InformationController informationController;

    public int numberOfDice;
    public int numberOfEyes;
    public string diceType;

    [SerializeField] List<string> colourList;

    private List<DiceScript> diceScritpList;

    [SerializeField] TextMeshProUGUI roundStaticTMP;
    [SerializeField] TextMeshProUGUI roundNumberTMP;
    [SerializeField] Button newThrow;
    [SerializeField] TextMeshProUGUI eyesSumStaticTMP;
    [SerializeField] TextMeshProUGUI eyesSumNumberTMP;

    [SerializeField] List<GameObject> diceList;

    private DiceScript dice1Script;
    private DiceScript dice2Script;
    private DiceScript dice3Script;
    private DiceScript dice4Script;
    private DiceScript dice5Script;
    private DiceScript dice6Script;

    // Start is called before the first frame update
    void Start()
    {
        SetUpLists();
        SetUpDice();
    }

    private void SetUpLists()
    {
        diceScritpList = new List<DiceScript>();
        diceScritpList.Add(dice1Script);
        diceScritpList.Add(dice2Script);
        diceScritpList.Add(dice3Script);
        diceScritpList.Add(dice4Script);
        diceScritpList.Add(dice5Script);
        diceScritpList.Add(dice6Script);
    }

    private void SetUpDice()
    {
        // Get to know the number of dice first to enable/disable...
        for (int i = 0; i < numberOfDice; i++)
        {
            // Make the dice visible
            diceList[i].SetActive(true);

            // Access dice scripts
            // https://www.youtube.com/watch?v=Y7pp2gzCzUI
            diceScritpList[i] = diceList[i].GetComponent<DiceScript>();

            // Init. dice Colours
            diceScritpList[i].spriteName = colourList[i];
        }
    }
}
