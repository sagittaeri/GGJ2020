using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Supervisor : MonoBehaviour
{

    [TextArea]
    public List<string> supervisorProgressionMessages = new List<string>();

    [TextArea]
    public List<string> supervisorExtraMessages = new List<string>();


    public Transform scrollContentTransform;
    public GameObject textMessagePrefab;
    public InputField playerInput;

    int progMessage;
    int extraMessage;

    public void Start()
    {
        NewProgressionMessage();

        StartCoroutine("ExtraMessageDelay");
        
    }

    public void NewProgressionMessage()
    {
        print("New Progression Message");
        GameObject newText = Instantiate(textMessagePrefab, scrollContentTransform);
        newText.GetComponent<Text>().text = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + "   " +
            supervisorProgressionMessages[progMessage];

        progMessage++;
    }


    IEnumerator ExtraMessageDelay()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));
        NewExtraMessage();
        yield break;
    }


    public void NewExtraMessage()
    {
        print("New Extra Message");
        GameObject newText = Instantiate(textMessagePrefab, scrollContentTransform);
        newText.GetComponent<Text>().text = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + "   " + 
            supervisorExtraMessages[extraMessage];

        extraMessage++;

        if (extraMessage <= supervisorExtraMessages.Count)
            StartCoroutine(ExtraMessageDelay());
    }


}
