using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDocument : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public Vector3 inspectPos;
    public Vector3 idlePos;
    public Vector3 idleScale;

    bool inspect;

    public void Start()
    {
        idlePos = transform.position;
        idleScale = transform.localScale;
    }


    public void Update()
    {
        if (inspect)
        {
            transform.position = Vector3.Lerp(transform.position, inspectPos, Time.deltaTime/10);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime/10);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, idlePos, Time.deltaTime/10);
            transform.localScale = Vector3.Lerp(transform.localScale, idleScale, Time.deltaTime/10);
        }

    }


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        transform.position = inspectPos;
        transform.localScale = Vector3.one;
        inspect = true;
    }

    public void OnPointerExit (PointerEventData pointerEventData)
    {
        transform.position = idlePos;
        transform.localScale = idleScale;
      //  inspect = false;
    }
}
