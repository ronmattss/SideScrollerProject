using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{
    // Start is called before the first frame 
    public int id;

    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetType() == typeof(CapsuleCollider2D))
            if (onTriggerEnter != null)
                onTriggerEnter.Invoke(); // instead of GameEvent.instance.DoorwayTriggerEnter(id); 

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.GetType() == typeof(CapsuleCollider2D))
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


}

