using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName ="new Normal Dialogue", menuName = "Dialogues/Types of Dialogues/Normal Dialogue")]
public class RepeatingDialogue : BaseDialogue
{
    [SerializeField]bool dialogueRepeats;
    public BaseDialogue IsRepeating() => dialogueRepeats ? this : base.nextDialogueSequence;

}
