using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{
    // Start is called before the first frame 
    // Note for using Observer pattern
    // Call the GameEvent.instance."Function()" of your event
    public int id;

    public UnityEvent onTriggerEnter;       // you can call GameEvent.instance.DoorwayTriggerEnter here(id)
    public UnityEvent onTriggerExit;
    public UnityEvent onDestroyEvent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetType() == typeof(CapsuleCollider2D)&& other.tag == "Player")
            if (onTriggerEnter != null)
                onTriggerEnter.Invoke(); // instead of GameEvent.instance.DoorwayTriggerEnter(id); 

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.GetType() == typeof(CapsuleCollider2D) && other.tag == "Player")
            if (onTriggerExit != null)
                onTriggerExit.Invoke();
    }



    IEnumerator Wait()
    {
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        yield return null;
    }
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        onDestroyEvent.Invoke();
    }


}

