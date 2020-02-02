using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Paragraph : MonoBehaviour
{
    Text myText;

    [TextArea]
    public string content;


    public List<Stage2Choic> matchingChoices = new List<Stage2Choic>();



}
