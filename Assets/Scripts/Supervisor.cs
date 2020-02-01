using System;
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
    public List<string> supervisorProgressionMessages2 = new List<string>();
    [TextArea]
    public List<string> supervisorProgressionMessages3 = new List<string>();
    [TextArea]
    public List<string> supervisorProgressionMessages4 = new List<string>();

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
    bool currentlyInProgressionMessages;

    public void Awake()
    {
        scrollRect = scrollContentTransform.GetComponentInParent<ScrollRect>();
        scrollBackground = scrollRect.GetComponent<Image>();
        originalPos = scrollBackground.transform.parent.localPosition;
    }

    public void InitSupervisor()
    {
        NewProgressionMessage(()=>
        {
            StartCoroutine("ExtraMessageDelay");
        });
    }

    public void NewProgressionMessage(Action callback = null)
    {
        print("New Progression Message");

        List<string> listMessages = supervisorProgressionMessages;
        if (progMessage == 1)
            listMessages = supervisorProgressionMessages2;
        else if (progMessage == 2)
            listMessages = supervisorProgressionMessages3;
        else if (progMessage == 3)
            listMessages = supervisorProgressionMessages4;

        currentlyInProgressionMessages = true;

        AddOneNewProgressionMessage(0, listMessages, callback);
        progMessage++;
    }

    public void AddOneNewProgressionMessage(int index, List<string> listMessages, Action callback)
    {
        if (index >= listMessages.Count)
        {
            currentlyInProgressionMessages = false;
            callback?.Invoke();
            return;
        }

        string text = listMessages[index];

        GameObject newText = Instantiate(textMessagePrefab, scrollContentTransform);
        newText.GetComponent<Text>().text = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + "   " + text;
        scrollRect.DOVerticalNormalizedPos(0f, 1f, true).SetDelay(0.1f);

        scrollBackground.DOKill(true);
        scrollBackground.DOColor(notifyColor, 0.2f).OnComplete(() =>
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
            scrollBackground.transform.parent.DOLocalMoveY(originalPos.y, 0.2f).OnComplete(() =>
            {
                scrollBackground.transform.parent.DOLocalMoveY(originalPos.y + 20f, 0.2f).OnComplete(() =>
                {
                    scrollBackground.transform.parent.DOLocalMoveY(originalPos.y, 0.2f);
                });
            });
        });

        DOVirtual.DelayedCall(3f, () =>
        {
            AddOneNewProgressionMessage(index + 1, listMessages, callback);
        });
    }


    IEnumerator ExtraMessageDelay()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(15, 30));
        NewExtraMessage();
        yield break;
    }


    public void NewExtraMessage()
    {
        print("New Extra Message");
        if (!currentlyInProgressionMessages)
        {
            GameObject newText = Instantiate(textMessagePrefab, scrollContentTransform);
            newText.GetComponent<Text>().text = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + "   " +
                supervisorExtraMessages[extraMessage];
            scrollRect.DOVerticalNormalizedPos(0f, 3f, true);

            extraMessage++;
        }

        if (extraMessage < supervisorExtraMessages.Count)
            StartCoroutine(ExtraMessageDelay());
    }


}
