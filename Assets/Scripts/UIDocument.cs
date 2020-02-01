using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDocument : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public Vector2 idlePos;
    public Vector3 idleScale;

    GameController GC;
    Button closeButton;

    public bool inspecting; // controlled by GameController

    public void Start()
    {
        GC = FindObjectOfType<GameController>();
        closeButton = GetComponentInChildren<Button>();

        closeButton.onClick.AddListener(CloseButton);


        idlePos = GetComponent<RectTransform>().anchoredPosition;
        idleScale = transform.localScale;
    }

    public void CloseButton()
    {
        GC.ExitInspect(this);
    }


    public void Update()
    {
      

    }


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GC.TryInspectDoc(this);
    }

    public void OnPointerExit (PointerEventData pointerEventData)
    {
    //
    }
}
