using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    GameController GC;
    Transform originalParent;
    Vector3 originalPos;

    void Start()
    {
        GC = FindObjectOfType<GameController>();
        originalParent = transform.parent;
        originalPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData _EventData)
    {
        Debug.Log("OnBeginDrag");
        //transform.SetParent(GC.gameCanvasRect);
    }

    public void OnDrag(PointerEventData _EventData)
    {
        Debug.Log("OnDrag");
        //transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData _EventData)
    {
        Debug.Log("OnEndDrag");

        //if (GC.selectedUIDropZone != null)
        //{
        //    GC.selectedUIDropZone.TryDropHere(this);
        //}

        //transform.SetParent(originalParent);

    }
}
