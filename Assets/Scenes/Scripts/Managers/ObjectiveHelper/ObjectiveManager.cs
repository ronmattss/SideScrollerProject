using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 
public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;
    [SerializeField] List<Objective> objectives = new List<Objective>();
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var obj in objectives)
        {
            if (!obj.isComplete)
                if (obj.isFinished())
                {
                    obj.isComplete = true;
                    obj.objectiveCompleteEvent.Invoke();
                }
        }
    }
}
