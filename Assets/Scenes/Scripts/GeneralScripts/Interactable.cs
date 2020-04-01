using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// how does interactable occur
// if player is in range of an interactable object (extends IInteractable)
// 
/*
basically this will be an abstract class that will be present to anything that is 
Interactable

*/
namespace SideScrollerProject
{
    public abstract class Interactable : MonoBehaviour
    {
        public bool interactable;
        public abstract void Interact(bool interactable);

    }
}
