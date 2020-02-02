using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Choic : Phrase
{
    [TextArea]
    public string choiceText;

    public bool isCorrect;

    Text textBox;

    public void Start()
    {
        textBox = GetComponentInChildren<Text>();
        textBox.text = choiceText;

        base.Start();
    }
}
