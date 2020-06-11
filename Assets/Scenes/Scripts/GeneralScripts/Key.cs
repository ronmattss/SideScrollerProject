using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int ID;
    public bool isDoorUnlocked = false;
    void Update()
    {
        if (this.gameObject.transform.parent != null)
            if (this.gameObject.transform.parent.name == "Player")
                isDoorUnlocked = true;

        if (isDoorUnlocked)
            GameEvent.instance.DoorwayTriggerEnter(ID);

    }
}
