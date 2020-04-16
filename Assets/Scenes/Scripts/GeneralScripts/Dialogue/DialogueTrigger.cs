using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using XNode;

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
        public DialogueGraph dialogue;
        //  public Chat chatGlobal;
        public NodePort port = null;

        public override void Interact(bool interactable)
        {
            Debug.Log("Calling Interactable");
            if (interactable)
            {
                // check node before displaying 
                if (dialogue.current != null)
                {
                    DialogueManager.instance.DisplaySentence(dialogue.current.text);
                    
                    if(dialogue.current.ListofSpawnables.Count != 0)
                    {
                        foreach(Spawnable entity in dialogue.current.ListofSpawnables)
                        {
                            SpawnSomething(entity);
                        }
                    }
                    dialogue.current = ChatDialogue(dialogue.current);
                }
                else
                {
                    DialogueManager.instance.DisplaySentence(" ");
                    // if it is a trigger
                    // trigger function
                }
            }
        }

        void SetUpDialogue()
        {
            // get dialogue graph, list all nodes, select a Chat Node//prolly first Node
            var localDialogueGraph = dialogue;
            List<Node> dialogueNodes = dialogue.nodes;
            dialogue.current = (Chat)dialogueNodes[0];
            // dialogue.current = (Chat)CheckNode(dialogueNodes[0]);


        }

        public Node CheckNode(Node currentNode)
        {
            switch (currentNode.name)
            {
                case "Chat":
                    return ChatDialogue((Chat)currentNode);
                case "Branch":
                    return null;

                case "Event":
                    return null;
                default:
                    return null;

            }
        }

        public void SpawnSomething(Spawnable spawnable)
        {
            GameObject spawnObject;
            spawnObject = Instantiate(spawnable.listOfGameobjects,spawnable.listofPositions,Quaternion.identity);
        }


        public Chat ChatDialogue(Chat chat)
        {
            NodePort localPort = null;
            Debug.Log(chat.text); // show text // then go to next Node if there are no multiple answers
            if (chat.answers.Count == 0)
            {
                localPort = chat.GetOutputPort("output");

            }
            else if (chat.answers.Count > 0)
            {
                // display answers
                //  localPort = chat.GetOutputPort("answers "+dialogue.A)
                //  return (Chat)localPort.Connection.node;
            }
            else
            {
                // localPort = null;
                return null;
            }
            if (localPort.IsConnected)
                return (Chat)localPort.Connection.node;
            return null;


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
                    // TriggerDialogue();
                    Debug.Log("Started Dialogue");
                    SetUpDialogue();
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
