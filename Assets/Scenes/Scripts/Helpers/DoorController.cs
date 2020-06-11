using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    public int id;
    void Start()
    {
        GameEvent.instance.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvent.instance.onDoorwayTriggerExit += OnDoorwayExit;
        GameEvent.instance.onKeyInteract += OnDoorwayUnlock;
    }

    // Update is called once per frame
    private void OnDoorwayOpen(int id)
    {
        if (this.id == id)
            this.gameObject.transform.position = new Vector3(this.transform.position.x, 2.5f, 0);
    }
    private void OnDoorwayExit(int id)
    {
        if (this.id == id)
            this.gameObject.transform.position = new Vector3(this.transform.position.x, 0, 0);
        Debug.Log("Am I working?");
    }

    private void OnDoorwayUnlock(int id, int key)
    {
        if (this.id == id)
            if (id != key)
            {
                // UI Manager Display something
                // prolly pick a dialogue
            }
            else
            {
                Debug.Log("WTF DOOR");// disable this or trigger something
                this.gameObject.SetActive(false);
            }
    }

    private void OnDestroy()
    {
        GameEvent.instance.onDoorwayTriggerEnter -= OnDoorwayOpen;
        GameEvent.instance.onDoorwayTriggerExit -= OnDoorwayExit;
        GameEvent.instance.onKeyInteract -= OnDoorwayUnlock;
    }
}
