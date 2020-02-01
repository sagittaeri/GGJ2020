using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefPhrase : Phrase
{
    public List<Phrase> conflictingPhrases = new List<Phrase>();

    [Space]
    public Phrase correctPhrase;

    public void Start()
    {
        base.Start();
        isBriefPhrase = true;
        print("is brief phrase?" + isBriefPhrase);
    }

    
}
