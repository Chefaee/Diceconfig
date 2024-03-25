using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;

public class DiceScript : MonoBehaviour
{
    [SerializeField] GameObject wholeDiceGameObject;
    [SerializeField] Image diceForm;
    [SerializeField] Image diceEyes;
    [SerializeField] TextMeshProUGUI eyesStaticTMP;
    [SerializeField] TextMeshProUGUI numberOfEyesTMP;
    [SerializeField] Toggle keepDice;

    // public string diceType; // get dicetype from generalUIScript
    public string spriteName;
    // public int possibleEyes; // get possible eyes from generalUIScript
    public string spriteEyesName;
    private int randomEye;

    [SerializeField] GameObject parentUI;
    private GeneralUIScript generalUIScript;

    // Start is called before the first frame update
    void Start()
    {
        generalUIScript = parentUI.GetComponent<GeneralUIScript>();
        throwDice();
    }

    public void throwDice()
    {
        // Testing purposes ~
        spriteName = "Green"; // remove this later, gets overwritten by generaluiscript

        string diceType = generalUIScript.diceType;
        int possibleEyes = generalUIScript.numberOfEyes;

        // Initialize color
        Sprite spriteForm = Resources.Load<Sprite>("Images/" + diceType + "/" + spriteName);
        diceForm.sprite = spriteForm;


        // Let the dice rotate 360° and maybe 3d rota
        // https://stackoverflow.com/questions/28648071/rotate-object-in-unity-3d
        // https://stackoverflow.com/questions/63194973/how-to-rotate-a-game-object-in-runtime-in-unity
        // diceForm.transform.Rotate(Vector3.right * 20f * Time.deltaTime); // x, y, z

        // Randomize eyes
        // https://stackoverflow.com/questions/2706500/how-do-i-generate-a-random-integer-in-c
        System.Random rnd = new System.Random();
        randomEye = rnd.Next(1, possibleEyes + 1);
        spriteName = randomEye.ToString();

        // Load the eye image...
        Sprite spriteEyes = Resources.Load<Sprite>("Images/" + diceType + "/Eyes/" + spriteName);
        diceEyes.sprite = spriteEyes;
    }
}
