using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Transform selectedUI;
    public UIDropZone selectedUIDropZone;

    public UIDocument inspected1;
    public UIDocument inspected2;

    RectTransform gameCanvasRect;

    // Start is called before the first frame update
    void Start()
    {
        gameCanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedUI != null) selectedUI.transform.position = Input.mousePosition;



        // Zoom in / out
        //if (inspect)
        //{
        //    transform.position = Vector3.Lerp(transform.position, inspectPos, Time.deltaTime / 10);
        //    transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime / 10);
        //}
        //else
        //{
        //    transform.position = Vector3.Lerp(transform.position, idlePos, Time.deltaTime / 10);
        //    transform.localScale = Vector3.Lerp(transform.localScale, idleScale, Time.deltaTime / 10);
        //}
    }

    public void TryInspectDoc(UIDocument uiDoc)
    {
        if (inspected1 == null)
        {
            InspectDoc1(uiDoc);
        }
        else if (inspected2 == null)
        {
            InspectDoc2(uiDoc);
        }
        else
            return;
    }

    void InspectDoc1(UIDocument doc1)
    {
        RectTransform doc1Rect = doc1.GetComponent<RectTransform>();

        doc1Rect.anchoredPosition = new Vector2(gameCanvasRect.sizeDelta.x / 4 * -1, 0);
        doc1Rect.localScale = Vector3.one;

        inspected1 = doc1;
    }

    void InspectDoc2(UIDocument doc2)
    {
        RectTransform doc2Rect = doc2.GetComponent<RectTransform>();

        doc2Rect.anchoredPosition = new Vector2(gameCanvasRect.sizeDelta.x / 4, 0);
        doc2Rect.localScale = Vector3.one;

        inspected2 = doc2;
    }

    public void ExitInspect(UIDocument uiDoc)
    {
        uiDoc.GetComponent<RectTransform>().anchoredPosition = uiDoc.idlePos;
        uiDoc.transform.localScale = uiDoc.idleScale;

        if (uiDoc == inspected1)
            inspected1 = null;

        if (uiDoc == inspected2)
            inspected2 = null;

    }
}
