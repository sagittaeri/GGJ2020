using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Phrase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public bool isBriefPhrase;

    [HideInInspector]
    public Phrase matchingPhrase;

    GameController GC;

    bool isSelected;
    bool playerCorrectlyMatched;

    public void Start()
    {
        GC = FindObjectOfType<GameController>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (!isSelected)
            transform.localScale = Vector3.one * 1.1f;
    }

    public void OnPointerExit (PointerEventData pointerEventData)
    {
        if (!isSelected)
            transform.localScale = Vector3.one;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        isSelected = !isSelected;

        if (!playerCorrectlyMatched)
        {
        Highlight();
        GC.SelectPhrase(this);
        }
    }

    public void Highlight()
    {
        GetComponent<Image>().color = Color.yellow;
        transform.localScale = Vector3.one * 1.5f;
    }

    public void ReturnToNormal(bool isMatched)
    {
        transform.localScale = Vector3.one;

        if (isMatched)
        {
            GetComponent<Image>().color = Color.green;
            playerCorrectlyMatched = true;
        }else
            GetComponent<Image>().color = Color.white;
        { }
    }

}
