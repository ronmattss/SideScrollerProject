using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    // Start is called before the first frame 
    public int id;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetType() == typeof(CapsuleCollider2D))
            GameEvent.instance.DoorwayTriggerEnter(id);
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.GetType() == typeof(CapsuleCollider2D))
            GameEvent.instance.DoorwayTriggerExit(id);
    }
}
