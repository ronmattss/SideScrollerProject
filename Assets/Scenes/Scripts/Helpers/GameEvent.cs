using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static GameEvent instance;
    bool waiting;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public event Action<int> onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(int id)
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter(id);
        }
    }

    public event Action<int> onDoorwayTriggerExit;

    public void DoorwayTriggerExit(int id)
    {
        if (onDoorwayTriggerExit != null)
        {
            onDoorwayTriggerExit(id);
        }
    }
    public event Action<int> onCameraSwitch;
    public void OnCameraSwitch(int id)
    {
        if (onCameraSwitch != null)
        {
            onCameraSwitch(id);
        }
    }

    public  event Action<int,int> onKeyInteract;
    public void OnKeyInteract(int id, int key)
    {
        if(onCameraSwitch != null)
        {
            onKeyInteract(id,key);
        }
    }



    public event Action<int> onSpikeThrust;

    public void OnSpikeThrust(int id)
    {
        if (onSpikeThrust != null)
        {
            onSpikeThrust(id);
            
        }
    }
    public void HitFreeze(float duration)
    {
        if (waiting) return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }


    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
}
