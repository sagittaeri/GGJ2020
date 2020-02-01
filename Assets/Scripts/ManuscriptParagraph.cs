using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManuscriptParagraph : MonoBehaviour
{
    //[TextArea]
    //public string before;
    [TextArea]
    public string changedParagraph;

    Text paragraphTextElement;

    public void Start()
    {
        paragraphTextElement = GetComponent<Text>();
    }

    public void ChangeText()
    {
        paragraphTextElement.text = changedParagraph;
    }
    
}
