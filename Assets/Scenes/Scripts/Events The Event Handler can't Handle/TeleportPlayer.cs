using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{       // gonna be a transition to another level
    public GameObject player;
    public GameObject location;
    public bool ChangeCamera = false;
    void Start()
    {
    }
public void Teleport()
{
    player.transform.position = location.transform.position;
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
