using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{

    // we can show the first dialogue
    // but the next sentences will be triggered by the player
    public class DialogueTrigger : Interactable
    {
        // Start is called before the first frame update


        public bool firstTimeInteract;
        public bool questInteract;
        public bool triggerInteract;
        public Dialouge normalDialogue;
        public Dialouge firstTimeDialogue;
        public Dialouge questDialogue;
        public Dialouge postQuestDialogue;

        public override void Interact(bool interactable)
        {
            Debug.Log("Calling Interactable");
            if (interactable)
            {
                // trigger something here
                
                DialogueManager.instance.DisplayNextSentence();
            }
        }

        public void ChooseDialogue()
        {// there will be types of dialogue

        }
        
        public void interactDialogue()
        {
            
        }

        public void TriggerDialogue()
        {
            // what dialogue to Trigger
            DialogueManager.instance.StartDialogue(normalDialogue);
        }

        private void OnTriggerEnter2D(Collider2D other)
        { // activate when player
            if (other.CompareTag("Player"))
            {
                if (other.GetType() == typeof(CircleCollider2D))
                {
                    interactable = true;
                    TriggerDialogue();
                    Debug.Log("Started Dialogue");
                    other.GetComponent<Actions>().isInteracting = interactable;
                    other.GetComponent<Actions>().interactableObject = this;
                }

            }
        }



        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetType() == typeof(CircleCollider2D))
                {
                    interactable = false;
                    TriggerDialogue();
                    Debug.Log("Started Dialogue");
                    other.GetComponent<Actions>().isInteracting = interactable;

                    DialogueManager.instance.EndDialogue();
                }

            }
        }


    }
}
