﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using XNode;
using TMPro;
using UnityEngine.Events;

namespace SideScrollerProject
{

    // we can show the first dialogue
    // but the next sentences will be triggered by the player
    public class DialogueTrigger : Interactable
    {
        // Start is called before the first frame update


        public bool queueNextDialogue;

        public DialogueGraph dialogue;      // dialogue(Lore or first time interactions)
        public DialogueGraph nextDialogue; // Triggers quest triggers?
        public UnityEvent exitDialogueEvent;

        public bool playerInProximity = false;
        //  public Chat chatGlobal;
        public NodePort port = null;

        public override void Interact(bool interactable)
        {
            Debug.Log("Calling Interactable");
            if (!interactable) return;
            if (dialogue.current != null)
            {
                DialogueManager.instance.interactText.SetActive(false);
                DialogueManager.instance.dialoguePlaceHolder.SetActive(true);
                DialogueManager.instance.DisplaySentence(dialogue.current.text);

                if (dialogue.current.ListofSpawnables.Count != 0)
                {
                    foreach (Spawnable entity in dialogue.current.ListofSpawnables)
                    {
                        SpawnSomething(entity);
                    }
                }
                dialogue.current = ChatDialogue(dialogue.current);
            }
            else
            {
                DialogueManager.instance.DisplaySentence(" ");
                if (nextDialogue != null)
                {
                    dialogue = nextDialogue;
                    queueNextDialogue = true;
                }
                else
                {
                    queueNextDialogue = false;
                    if (exitDialogueEvent != null)
                    {
                        exitDialogueEvent.Invoke();
                    }
                }
                // if it is a trigger
                // trigger function
            }

        }
        void Start()
        {
            SetUpDialogue(dialogue);
        }

        /// <summary>This function accepts a DialogueGraph </summary>
        void SetUpDialogue(DialogueGraph currDialogue)
        {
            // get dialogue graph, list all nodes, select a Chat Node//prolly first Node
            var localDialogueGraph = currDialogue;
            List<Node> dialogueNodes = currDialogue.nodes;
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
            spawnObject = Instantiate(spawnable.listOfGameobjects, spawnable.listofPositions, Quaternion.identity);
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
            //    DialogueManager.instance.StartDialogue(normalDialogue);
        }

        private void OnTriggerEnter2D(Collider2D other)
        { // activate when player
            if (other.CompareTag("Player"))
            {

                if (other.GetType() == typeof(CircleCollider2D))
                {
                    if (!playerInProximity)
                    {
                        DialogueManager.instance.interactText.SetActive(true);
                        DialogueManager.instance.SetTransparency(false);
                        playerInProximity = true;
                        Debug.Log(other.transform.name);
                        interactable = true;
                        // TriggerDialogue();

                        Debug.Log("Started Dialogue");
                        other.GetComponent<Actions>().isInteracting = interactable;
                        other.GetComponent<Actions>().interactables = this;
                    }
                    else
                    {

                    }
                }

            }
        }



        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {

                if (other.GetType() == typeof(CircleCollider2D))
                {
                    // DialogueManager.instance.interactText.SetActive(false);
                    interactable = false;
                    // TriggerDialogue();
                    Debug.Log("Started Dialogue");
                    other.GetComponent<Actions>().isInteracting = interactable;
                    DialogueManager.instance.SetTransparency(true);
                    //DialogueManager.instance.dialoguePlaceHolder.SetActive(false);
                    DialogueManager.instance.EndDialogue();
                    playerInProximity = false;
                    if (queueNextDialogue)
                    {
                        SetUpDialogue(dialogue);
                    }
                }

            }
        }


    }
}
