using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIDropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header ("We gotta set these ourselves")]
    public UIDraggable correctDraggable;
    public ManuscriptParagraph relevantParagraph;

    GameController GC;
    
    
    void Start()
    {
        GC = FindObjectOfType<GameController>();
    }

   public void OnPointerEnter(PointerEventData pointerEventData)
    {
        GC.selectedUIDropZone = this;
    }

    public void OnPointerExit (PointerEventData pointerEventData)
    {
        GC.selectedUIDropZone = null;
    }

    public void TryDropHere(UIDraggable incomingDraggable)
    {
        if (incomingDraggable == correctDraggable)
            relevantParagraph.ChangeText();
    }
}
