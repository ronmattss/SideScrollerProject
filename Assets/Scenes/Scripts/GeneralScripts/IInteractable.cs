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
    public class Interactable : MonoBehaviour
    {
        public bool isInteractable;

        List<Interaction> interactions = new List<Interaction>();

        // will be called when player/ or trigger is called
        public void OnInteract()
        {
            foreach (Interaction i  in interactions)
            {
                // call a function
            }
        }

    }
}
