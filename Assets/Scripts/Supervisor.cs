using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Supervisor : MonoBehaviour
{

    [TextArea]
    public List<string> supervisorProgressionMessages = new List<string>();

    [TextArea]
    public List<string> supervisorExtraMessages = new List<string>();


    public Transform scrollContentTransform;
    public GameObject textMessagePrefab;
    public InputField playerInput;
    public ScrollRect scrollRect;
    public Image scrollBackground;
    public Color normalColor;
    public Color notifyColor;

    Vector3 originalPos;

    int progMessage;
    int extraMessage;

    public void Awake()
    {
        scrollRect = scrollContentTransform.GetComponentInParent<ScrollRect>();
        scrollBackground = scrollRect.GetComponent<Image>();
        originalPos = scrollBackground.transform.parent.localPosition;
    }

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
        scrollRect.DOVerticalNormalizedPos(0f, 3f, true);

        scrollBackground.DOKill(true);
        scrollBackground.DOColor(notifyColor, 0.2f).OnComplete(()=>
        {
            scrollBackground.DOColor(normalColor, 0.2f).OnComplete(() =>
            {
                scrollBackground.DOColor(notifyColor, 0.2f).SetDelay(0.3f).OnComplete(() =>
                {
                    scrollBackground.DOColor(normalColor, 0.2f);
                });
            });
        });

        scrollBackground.transform.parent.DOKill(true);
        scrollBackground.transform.parent.DOLocalMoveY(originalPos.y + 20f, 0.2f).OnComplete(() =>
        {
            scrollBackground.transform.parent.DOLocalMoveY(originalPos.y, 0.2f).OnComplete(()=>
            {
                scrollBackground.transform.parent.DOLocalMoveY(originalPos.y + 20f, 0.2f).OnComplete(() =>
                {
                    scrollBackground.transform.parent.DOLocalMoveY(originalPos.y, 0.2f);
                });
            });
        });

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
        if (extraMessage >= supervisorExtraMessages.Count)
            return;
        GameObject newText = Instantiate(textMessagePrefab, scrollContentTransform);
        newText.GetComponent<Text>().text = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + "   " + 
            supervisorExtraMessages[extraMessage];
        scrollRect.DOVerticalNormalizedPos(0f, 3f, true);

        extraMessage++;

        if (extraMessage <= supervisorExtraMessages.Count)
            StartCoroutine(ExtraMessageDelay());
    }


}
