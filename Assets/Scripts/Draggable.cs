using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    GameController GC;

    void Start()
    {
        GC = FindObjectOfType<GameController>();
    }

    public void OnBeginDrag(PointerEventData _EventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData _EventData)
    {
        Debug.Log("OnDrag");
        transform.localPosition = _EventData.position;
    }

    public void OnEndDrag(PointerEventData _EventData)
    {
        Debug.Log("OnEndDrag");

        if (GC.selectedUIDropZone != null)
        {
            this.transform.SetParent (GC.selectedUIDropZone.transform);
            this.transform.localPosition = Vector3.zero;
        }
    }
}
