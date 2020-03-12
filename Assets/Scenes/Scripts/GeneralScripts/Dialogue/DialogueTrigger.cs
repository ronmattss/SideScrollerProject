using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{

    // we can show the first dialogue
    // but the next sentences will be triggered by the player
    public class DialogueTrigger : MonoBehaviour
    {
        // Start is called before the first frame update



        public Dialouge dialouges;


        public void TriggerDialogue()
        {
            DialogueManager.instance.StartDialogue(dialouges);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                TriggerDialogue();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                DialogueManager.instance.EndDialogue();
            }
        }
    }
}
