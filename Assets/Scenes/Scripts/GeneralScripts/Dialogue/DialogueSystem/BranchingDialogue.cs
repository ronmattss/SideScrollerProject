﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] // TODO: make a model class wrapper of list list string
[CreateAssetMenu(fileName = "new Branching Dialogue", menuName = "Dialogues/Types of Dialogues/Branching Dialogue")]

public class BranchingDialogue : OneTimeDialogue
{

    //public List<SentenceWrapper> listofResponse;
    public List<string> responses = new List<string>();
    public List<BaseDialogue> respondDialogue = new List<BaseDialogue>();
}
