using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    // Start is called before the first frame update
    // invoke something when Player pick up the object
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            // Invoke something
            //Placeholder destroy 
            Destroy(this.gameObject);
        }
    }
}
