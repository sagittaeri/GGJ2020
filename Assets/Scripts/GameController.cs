using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour
{

    public Transform selectedUI;
    public UIDropZone selectedUIDropZone;

   // public UIDocument inspected1;
   // public UIDocument inspected2;

    public BriefPhrase briefPhraseSelected;
    public Phrase phraseSelected;

    public List<UIDocument> evidenceDocs = new List<UIDocument>();

    public UIDocument activeDocument;
    int docStage; // 0 = first doc on screen

    public RectTransform gameCanvasRect;
    public float canvasScaleRatio;

    int gameStage = 1;

    // For Stage 2 of the game
    public Transform stage2phraseCanvas;
    int stage2examinedBriefPhrase;
    UIDocument manuscript;
    BriefPhrase examinedBriefPhrase; // The phrase the player is choosing the 'true' fact for
    List<Phrase> foundPhrases = new List<Phrase>(); // The matching phrases that will be moved on screen. 



    // Start is called before the first frame update
    void Start()
    {
        stage2phraseCanvas.gameObject.SetActive(false);
        manuscript = GameObject.Find("Manuscript").GetComponent<UIDocument>();
        gameCanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();

        print(gameCanvasRect.sizeDelta);
        print(Screen.width + " x " + Screen.height);

        canvasScaleRatio = Screen.width / gameCanvasRect.sizeDelta.x;

        NewDocOnScreen();
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

    public void SelectPhrase(Phrase selectedPhrase)
    {
        if (gameStage == 1 && selectedPhrase.isBriefPhrase)
        {
            // Pick or cycle the Brief Phrase
            if (briefPhraseSelected == null)
            {
                briefPhraseSelected = selectedPhrase as BriefPhrase;
            }
            else if (briefPhraseSelected == selectedPhrase as BriefPhrase)
            {
                briefPhraseSelected.ReturnToNormal(false);
                briefPhraseSelected = null;
                return;
            }
            else
            {
                briefPhraseSelected.ReturnToNormal(false);
                briefPhraseSelected = null;
                SelectPhrase(selectedPhrase);
            }
        }

        // Pick or cycle the evidence/doc Phrase
        if (!selectedPhrase.isBriefPhrase)
        {
            // Pick or cycle the Brief Phrase
            if (phraseSelected == null)
            {
                phraseSelected = selectedPhrase;
            }

            else if (phraseSelected == selectedPhrase)
            {
                phraseSelected.ReturnToNormal(false);
                phraseSelected = null;
                return;
            }
            else
            {
                phraseSelected.ReturnToNormal(false);
                phraseSelected = null;
                SelectPhrase(selectedPhrase);
            }

        }

        if (briefPhraseSelected != null && phraseSelected != null)
        {
            if (gameStage == 1)
                ComparePhrases();
            else if (gameStage == 2)
                Stage2CompareAnswer();
        }

    }


    public void ComparePhrases()
    {
        if (briefPhraseSelected != null && phraseSelected != null)
        {
            print("Comparing...");
            if (briefPhraseSelected.conflictingPhrases.Contains(phraseSelected))
            {
                // Match works! 
                briefPhraseSelected.ReturnToNormal(true);
                phraseSelected.ReturnToNormal(true);
                print("A match!");
            }
            else
            {
                // not a match
                briefPhraseSelected.ReturnToNormal(false);
                phraseSelected.ReturnToNormal(false);
                print("Not a match");
            }
        }

        // Return phrases to normal
        briefPhraseSelected = null;
        phraseSelected = null;

        for (int i = 0; i < activeDocument.matchablePhrases.Count; i++)
        {
            print("i = " + i + " count = " + activeDocument.matchablePhrases.Count);
            if (activeDocument.matchablePhrases[i].playerCorrectlyMatched == false)
                break;
            else if (i == activeDocument.matchablePhrases.Count - 1)
                CompleteDoc();
        }
        
    }

    public void CompleteDoc() // Called when the GameController has deemed all phrases in the doc to be matched
    {
        print("Doc complete");
        GetComponent<Supervisor>().NewProgressionMessage();

        DOVirtual.DelayedCall(3f, () =>
        {
            NewDocOnScreen();
        });
    }



    void NewDocOnScreen()
    {
        docStage++;

        manuscript.ResetPhrases(true);

        if (docStage == 3) // No more new docs! time for Stage 2
        {
            activeDocument.HideDoc();
            manuscript.GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, 0.5f).SetDelay(1f);
            manuscript.transform.DOScale(Vector3.one, 0.5f).SetDelay(1f);
            DOVirtual.DelayedCall(1.5f, Stage2);
            return;
        }

        // Remove the old doc. Skip this if it is the first doc
        if (docStage == 1)
        {
            activeDocument = evidenceDocs[0];
            activeDocument.ShowDoc(new Vector2( gameCanvasRect.sizeDelta.x / 4, 0));
        }
        else
        {
            activeDocument.HideDoc();
            activeDocument.ResetPhrases();
            // increase the doc stage
            // show the new doc
            DOVirtual.DelayedCall(1f, () =>
            {
                activeDocument = evidenceDocs[docStage];
                activeDocument.ShowDoc(new Vector2(gameCanvasRect.sizeDelta.x / 4, 0));
            });
        }
    }


    void Stage2() // 2nd phase of the game where the player picks the 'true' statement for each section
    {
        gameStage = 2;
        print("Stage 2 now");
        stage2phraseCanvas.gameObject.SetActive(true);

    }

    void Stage2NextBriefPhrase()
    {
        // Empty the canvas
        foreach (Transform t in stage2phraseCanvas)
        {
            // this isn't working for osme reason
            t.SetParent(null);
            t.gameObject.SetActive(false);
        }


        examinedBriefPhrase = manuscript.matchablePhrases[stage2examinedBriefPhrase] as BriefPhrase;

        briefPhraseSelected = examinedBriefPhrase;
        briefPhraseSelected.Highlight();

        // Put the found phrases on screen
        foreach (Phrase p in examinedBriefPhrase.conflictingPhrases)
        {
            p.transform.SetParent(stage2phraseCanvas);
            //   p.transform.position = Vector3.zero;
        }



        stage2examinedBriefPhrase++;
    }

    void Stage2CompareAnswer()
    {
        if (phraseSelected.isCorrectPhrase)
        {
            print("Correct Phrase found!");
            //Hide the other phrases, empty the Stage 2 Canvas transform

            Stage2NextBriefPhrase();
        }
    }


    // OLD CODE from when we would inspect multiple docs. This is mostly handled from UIDocument now

    //public void TryInspectDoc(UIDocument uiDoc)
    //{
    //    if (inspected1 == null)
    //    {
    //        InspectDoc1(uiDoc);
    //    }
    //    else if (inspected2 == null)
    //    {
    //        InspectDoc2(uiDoc);
    //    }
    //    else
    //        return;
    //}


    //void InspectDoc1(UIDocument doc1)
    //{
    //    RectTransform doc1Rect = doc1.GetComponent<RectTransform>();

    //    doc1Rect.anchoredPosition = new Vector2(gameCanvasRect.sizeDelta.x / 4 * -1, 0);
    //    doc1Rect.localScale = Vector3.one;

    //    doc1Rect.SetAsLastSibling();

    //    inspected1 = doc1;
    //}

    //void InspectDoc2(UIDocument doc2)
    //{
    //    RectTransform doc2Rect = doc2.GetComponent<RectTransform>();

    //    doc2Rect.anchoredPosition = new Vector2(gameCanvasRect.sizeDelta.x / 4, 0);
    //    doc2Rect.localScale = Vector3.one;

    //    doc2Rect.SetAsLastSibling();

    //    inspected2 = doc2;
    //}

    //public void ExitInspect(UIDocument uiDoc)
    //{
    //    uiDoc.GetComponent<RectTransform>().anchoredPosition = uiDoc.offscreenPos;
    //    uiDoc.transform.localScale = uiDoc.idleScale;

    //    if (uiDoc == inspected1)
    //        inspected1 = null;

    //    if (uiDoc == inspected2)
    //        inspected2 = null;
    //}
}
