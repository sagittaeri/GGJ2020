using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefPhrase : Phrase
{
    public List<Phrase> conflictingPhrases = new List<Phrase>();
    public Phrase correctPhrase;

    public void Start()
    {
        base.Start();
        isBriefPhrase = true;

        foreach (Phrase p in conflictingPhrases)
            p.matchingPhrase = this;
    }

    
}
