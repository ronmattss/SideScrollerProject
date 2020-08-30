using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
[Serializable]
[CreateAssetMenu(fileName = "new One Time Event Dialogue", menuName = "Dialogues/Types of Dialogues/Event Dialogue")]
public class EventDialogue : OneTimeDialogue
{
    public UnityEvent preDialogueEvent;
    public void InvokePreDialogueEvent() => preDialogueEvent.Invoke();

}
