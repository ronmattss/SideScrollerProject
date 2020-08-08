using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderTrigger : MonoBehaviour
{

    public event EventHandler OnPlayerEnterTrigger;
    // Start is called before the first frame updateOn
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            OnPlayerEnterTrigger?.Invoke(this,EventArgs.Empty); // subscribed objects will invoke // same as GameEvent in DoorController
        }
    }
}
