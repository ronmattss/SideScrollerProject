using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Events;


// SO this is the data that will be used to activate deactivate Objectives
// a monobehaviour will read the data, and pass it to the objective manager


[Serializable]
public class Objective 
{
    public string objectiveName;
    public bool isComplete;
    public List<Task> requirements = new List<Task>();
    public UnityEvent objectiveCompleteEvent;

    public bool isFinished()
    {
        return requirements.All(x => x.isFinished == true);
    }
    //return true if foreach requirement isFinished

}
