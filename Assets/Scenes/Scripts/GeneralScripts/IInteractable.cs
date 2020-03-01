using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// how does interactable occur
// if player is in range of an interactable object (extends IInteractable)
// 
public interface IInteractable
{
    public void OnInteract();

}
