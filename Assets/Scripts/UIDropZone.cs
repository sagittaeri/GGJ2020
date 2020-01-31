using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

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
}
