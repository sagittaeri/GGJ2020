﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class UIDocument : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 offscreenPos;
    public Image background;

    GameController GC;
    Button closeButton;

    public bool inspecting; // controlled by GameController
    public Action clickCallback;

    [HideInInspector]
    public List<Phrase> matchablePhrases = new List<Phrase>();
    [HideInInspector]
    public List<BriefPhrase> briefPhrases = new List<BriefPhrase>();


    public void Start()
    {
        offscreenPos = GetComponent<RectTransform>().anchoredPosition;
        background = GetComponentInChildren<Image>();

        // Scan all Phrases in this doc for the ones that match with something
        foreach (Phrase p in GetComponentsInChildren<Phrase>())
        {
            if (p.matchingPhrase != null)
                matchablePhrases.Add(p);
        }

        foreach (BriefPhrase p in GetComponentsInChildren<BriefPhrase>())
        {
            briefPhrases.Add(p);
        }

    }

    public void CloseButton()
    {
        //GC.ExitInspect(this);
    }

    public void ShowDoc(Vector2 onScreenPos)
    {
        print(GetComponent<RectTransform>().name);
        //transform.GetComponent<RectTransform>().anchoredPosition = onScreenPos;
        transform.GetComponent<RectTransform>().DOAnchorPos(onScreenPos, 0.3f, true);
    }

    public void HideDoc()
    {
        //transform.GetComponent<RectTransform>().anchoredPosition = offscreenPos;
        transform.GetComponent<RectTransform>().DOAnchorPos(offscreenPos, 0.3f, true);
    }

    public void ResetPhrases(bool forceReset=false)
    {
        foreach (Phrase p in matchablePhrases)
        {
            p.ReturnToNormal(false, forceReset);
        }

        foreach (Phrase p in briefPhrases)
        {
            p.ReturnToNormal(false, forceReset);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (clickCallback != null)
        {
            background.DOColor(Color.yellow, 0.3f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (clickCallback != null)
        {
            background.DOColor(Color.white, 0.3f);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (clickCallback != null)
        {
            background.DOColor(Color.yellow, 0.3f);
            clickCallback?.Invoke();
            clickCallback = null;
        }
    }
}
