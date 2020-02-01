using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Phrase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public bool isBriefPhrase;

    public BriefPhrase matchingPhrase; // Set this on the 'Evidence' Document phrases, not the Manuscript
    public bool isCorrectPhrase;

    GameController GC;

    [HideInInspector]
    public bool isSelected;

    [HideInInspector]
    public bool playerCorrectlyMatched;

    UIDocument parentDoc; // the doc that this Phrase belongs to. Doesn't apply to Brief Phrases

    public void Start()
    {
        GC = FindObjectOfType<GameController>();

        if (!isBriefPhrase)
            parentDoc = transform.parent.GetComponent<UIDocument>();


        // Link the Phrase with it's matching BriefPhrase
        if (matchingPhrase != null)
            matchingPhrase.conflictingPhrases.Add(this);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (!isSelected && !playerCorrectlyMatched)
        {
            //transform.localScale = Vector3.one * 1.1f;
            transform.DOScale(1.1f, 0.3f);
        }
    }

    public void OnPointerExit (PointerEventData pointerEventData)
    {
        if (!isSelected && !playerCorrectlyMatched)
        {
            //transform.localScale = Vector3.one;
            transform.DOScale(1f, 0.3f);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!playerCorrectlyMatched)
        {
            isSelected = !isSelected;
            Highlight();
            GC.SelectPhrase(this);
        }
    }

    public void Highlight()
    {
        GetComponent<Image>().DOColor(Color.yellow, 0.3f);
        transform.DOScale(1.5f, 0.3f);
    }

    public void ReturnToNormal(bool isMatched, bool forceReset=false)
    {
        if (playerCorrectlyMatched && !forceReset)
            return;

        isSelected = false;

        //transform.localScale = Vector3.one;
        transform.DOScale(1f, 0.3f);

        if (isMatched)
        {
            GetComponent<Image>().DOColor(Color.green, 0.3f);
            playerCorrectlyMatched = true;
        } else
            GetComponent<Image>().DOColor(Color.white, 0.3f);

        if (forceReset)
            playerCorrectlyMatched = isMatched;
    }
    
}
