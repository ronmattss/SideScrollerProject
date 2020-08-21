using System.Collections;
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
    // REFACTOR THIS
    // this should activate in the
    public class DialogueTrigger : Interactable
    {
        // Start is called before the first frame update


        public GameObject interactText;
        public bool queueNextDialogue;
        public bool loopDialogue;

        public DialogueGraph dialogue;      // dialogue(Lore or first time interactions)
        public DialogueGraph nextDialogue; // Triggers quest triggers?
        public UnityEvent exitDialogueEvent;

        // looping Dialogue

        public bool playerInProximity = false;
        //  public Chat chatGlobal;
        public NodePort port = null;

        public override void Interact(bool interactable)
        {
            PlayerManager.instance.GetPlayerAction().canMove = false;
            PlayerManager.instance.GetPlayerAction().horizontalMovement = 0;
            Debug.Log("Calling Interactable");
            if (!interactable) return;
            // first Dialogue
            if (dialogue.current != null)
            {
                DialogueManager.instance.interactText.GetComponent<TMP_Text>().alpha = 0;
                DialogueManager.instance.dialoguePlaceHolder.SetActive(true);
                DialogueManager.instance.DisplaySentence(dialogue.current.text);

                if (dialogue.current.ListofSpawnables != null)
                {
                    foreach (Spawnable entity in dialogue.current.ListofSpawnables)
                    {
                        SpawnSomething(entity);
                    }
                }
                dialogue.current = ChatDialogue(dialogue.current);
            }
            // after dialogue is finished
            else
            {
                DialogueManager.instance.DisplaySentence(" ");
                // Something wrong here // if next dialogue is null then loop or exit dialogue?
                if (nextDialogue != null)
                {
                    dialogue = nextDialogue;
                    queueNextDialogue = true;
                    nextDialogue = null;
                    // interactable = false;
                }
                else // if null then loop or exit
                {

                    queueNextDialogue = loopDialogue;

                    if (exitDialogueEvent != null)
                    {
                        exitDialogueEvent.Invoke();
                    }
                }
                // if it is a trigger
                // trigger function
                PlayerManager.instance.GetPlayerAction().canMove = true;
            }

        }
        void Start()
        {
            interactText = GameObject.Find("Interact");
            if (interactText == null)
                return;
            SetUpDialogue(dialogue);
        }
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnEnable()
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

        private void OnTriggerEnter2D(Collider2D other)
        { // activate when player
            if (other.CompareTag("Player"))
            {

                if (other.GetType() == typeof(CircleCollider2D))
                {
                    if (!playerInProximity)
                    {
                        //DialogueManager.instance.interactText.GetComponent<TMP_Text>().alpha = 255;
                        DialogueManager.instance.interactText.SetActive(true);
                        DialogueManager.instance.SetTransparency(false);
                        //  DialogueManager.instance.SetTransparency(false);
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
                    DialogueManager.instance.interactText.GetComponent<TMP_Text>().alpha = 0;
                    DialogueManager.instance.interactText.SetActive(false);
                    //                    interactText.SetActive(false);
                    interactable = false;
                    // TriggerDialogue();
                    Debug.Log("Started Dialogue");
                    other.GetComponent<Actions>().isInteracting = interactable;
                    // DialogueManager.instance.SetTransparency(true);
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
