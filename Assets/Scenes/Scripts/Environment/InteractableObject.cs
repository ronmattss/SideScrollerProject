using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace SideScrollerProject
{
    // Refactor this
    public class InteractableObject : Interactable,IOnHoldInteract
    {

        public bool playerIsInProximity = false;
        public GameObject interactText;
        public GameObject someRandomShit;
        public UnityEvent interactEvent;
        public UnityEvent interactHoldEvent;
        public GameObject player;

        public void Start()
        {
            interactable = false;
            interactText = DialogueManager.instance.interactText;
            //interactText = GameObject.Find("Interact");
            if (interactText == null)
                return;
        }
        public void DebugInvoke()
        {
            interactEvent.Invoke();
        }
        public override void Interact(bool interactable)
        {
            if (this.interactable)
                this.interactable = false;
            else
                this.interactable = true;
            interactEvent.Invoke();
           // someRandomShit.SetActive(this.interactable);


        }
        public void InteractHold()
        {
           interactHoldEvent.Invoke();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
           Debug.Log(other.transform.name);
            if (other.transform.tag == "Player")
            {
                player = other.gameObject;
                if (interactText != null)
                {
                    interactText.SetActive(true);
                    DialogueManager.instance.SetTransparency(false);
                }
                if (other.GetType() == typeof(CircleCollider2D))
                {
                    other.GetComponent<Actions>().interactables = this;
                    other.GetComponent<Actions>().currentHoldTime = this.secondsHold;
                    other.GetComponent<Actions>().isInteracting = true;
                    playerIsInProximity = true;
                }
            }

        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform.tag == "Player")
            {
                interactText.SetActive(false);
                if (other.GetType() == typeof(CircleCollider2D))
                {
                    other.GetComponent<Actions>().interactables = null;
                    other.GetComponent<Actions>().isInteracting = false;
                    playerIsInProximity = false;
                }
            }
        }

    }
}