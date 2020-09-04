using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SideScrollerProject
{
    // this will handle if a dialogue is repeating or not 
    // sits in a gameobject
    public class DialogueReader : Interactable
    {
        public BaseDialogue dialogue;
        int indexType = -1;
        public Queue<string> sentences;
        public bool showResponses = false;



        // Ineract method to interact with the script
        // main Logic here
        // 
        public override void Interact(bool interactable)
        {
            if (!ImprovedDialogueManager.instance.dialogueHolder.activeSelf)
            {
                ImprovedDialogueManager.instance.dialogueHolder.SetActive(true);
            }
            DisplayNextSentence();

            // if (sentences.Count > 0)
            // {
            // }
            // else
            // {
            //     ImprovedDialogueManager.instance.dialogueHolder.SetActive(false);
            // }
        }
        public void DisplayNextSentence()
        {
            if (sentences.Count < 1 || sentences.Peek() == " ")
            {
                Debug.Log("end dialogue");
                //EndDialogue();
                //if (sentences.Count <= 0) // no more sentences to display
                // Invoke dialogue events
                switch (indexType)
                {
                    case 0: // if repeating Dialogue
                        dialogue = (dialogue as RepeatingDialogue).IsRepeating();
                        indexType = SetupDialogue(dialogue);
                        break;
                    case 1:

                        indexType = SetupDialogue(dialogue);
                        break;
                    case 2:
                    //one time events ends dialogues
                        if ((dialogue as EventDialogue).dialogueEvent != null) (dialogue as EventDialogue).InvokePostDialogueEvent();
                        break;
                    default:
                        break;
                }
                ImprovedDialogueManager.instance.dialogueHolder.SetActive(false);
                return;
            }
            string sentence = sentences.Dequeue();
            // int x = sentences.Count;
            // if branching Dialogue
            if (indexType == 1 && sentences.Count == 1)
            {
                (dialogue as BranchingDialogue).responses.ForEach(x => Debug.Log(x));
                showResponses = true; // call respond Dialogue
                ResponseDisplay();
                // pass all necessary info
            }

            //setup respond UI
            //setup respond buttons(that can be triggered)
            // show responses


            ImprovedDialogueManager.instance.text.text = sentence + " " + sentences.Count;
            Debug.Log(sentence);
            //coroutine for the type animation 
        }
        // accept a dialogue
        // check type of dialogue
        // Queue Sentences
        //
        // Start is called before the first frame update
        public void ResponseDisplay()
        {
                            ImprovedDialogueManager.instance.responseHolder.SetActive(true);
                ImprovedDialogueManager.instance.responseHolder.GetComponent<ResponseHolder>().ReadResponses((dialogue as BranchingDialogue));
                ImprovedDialogueManager.instance.responseHolder.GetComponent<ResponseHolder>().isShowing = true;
        }
        void Start()
        {
            sentences = new Queue<string>();
            if (dialogue == null) return;
            Debug.Log("Dialogue Type: " + dialogue.ToString());
        }
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            //load the dialogue

        }
        /// <summary>
        /// LateUpdate is called every frame, if the Behaviour is enabled.
        /// It is called after all Update functions have been called.
        /// </summary>
        void LateUpdate()
        {
            // enable when displaying responses
            if (showResponses)
            {
                SetupResponses(dialogue as BranchingDialogue);

            }

        }
        void SetupResponses(BranchingDialogue d)
        {

            DisplayNextSentence();
            showResponses = false;

        }
        public void StartDialogue(BaseDialogue d)
        {
            sentences.Clear();

            foreach (string sentence in d.sentences) sentences.Enqueue(sentence);
        }

        public int SetupDialogue(BaseDialogue d)
        {
            // check type of dialogue
            // unload dialogue data
            // each type uses different UI 
            // consider continueing multiple dialogues and exiting after one dialogue
            // Load Initial Dialogue sentence
            StartDialogue(d);
            switch (d.GetType().Name)
            {
                case "RepeatingDialogue":       // Dialogue with no events and repeats the dialogue over and over again
                    return 0;
                case "BranchingDialogue":       // branching dialogues that has respond events, can also invoke event
                    return 1;
                case "EventDialogue":           // Dialogue type that invokes events before or after a dialogue is fired
                    return 2;
                default:
                    return 0;
            }

        }
        public void ReadRepeatingDialogue()
        {// queue sentences in dialogue Manager
         // display it from dialogue Manager
         // end of sentences?
         // check if repeating or not in both cases load this dialogue again or load a new dialogue
         // Setup Dialogue Again
         // 
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter2D(Collider2D other)
        { // activate when player
            if (other.CompareTag("Player"))
            {

                if (other.GetType() == typeof(CircleCollider2D))
                {
                    ImprovedDialogueManager.instance.currentReader = this;
                    indexType = SetupDialogue(dialogue);
                    other.GetComponent<Actions>().isInteracting = true;
                    other.GetComponent<Actions>().interactables = this;
                    Debug.Log("Dialogue Setup");
                }

            }
        }


        private void OnTriggerExit2D(Collider2D other)
        { // activate when player
            if (other.CompareTag("Player"))
            {

                if (other.GetType() == typeof(CircleCollider2D))
                {
                    other.GetComponent<Actions>().isInteracting = false;
                    other.GetComponent<Actions>().interactables = null;
                }

            }
        }
    }


}