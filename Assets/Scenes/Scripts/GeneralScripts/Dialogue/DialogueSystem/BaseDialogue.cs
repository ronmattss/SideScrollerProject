using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public abstract class BaseDialogue : ScriptableObject
{
    public string dialogueName;
    public string speaker;
    public List<string> sentences;
    public BaseDialogue nextDialogueSequence;
    
}

