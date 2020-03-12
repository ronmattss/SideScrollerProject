﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public Queue<string> sentences;

        private void Awake()
        {

            sentences = new Queue<string>();

        }

        public void StartDialogue(Dialouge dialouge)
        {
            // iterate 
            sentences.Clear();
            foreach (string sentence in dialouge.sentences)
            {
                sentences.Enqueue(sentence);
            }
            // execute sentences
            DisplayNextSentence();
            // does it have options?  if there is, execute the action on it : no proceed to the next sentence.

        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            Debug.Log(sentence);
        }

        public void EndDialogue()
        {
            Debug.Log("Dialogue Ended");
        }

    }
}