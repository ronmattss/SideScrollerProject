using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// this mostly triggers events after finishing dialogues
public abstract class OneTimeDialogue : BaseDialogue
{
    public UnityEvent dialogueEvent;
    public virtual void InvokePostDialogueEvent() => dialogueEvent.Invoke();
}
