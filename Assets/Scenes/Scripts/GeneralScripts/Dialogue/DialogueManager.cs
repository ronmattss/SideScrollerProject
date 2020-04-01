using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SideScrollerProject
{
    
    public class DialogueManager : MonoBehaviour
    {   
        public static DialogueManager instance;
        private DialogueManager _instance;
       public TMP_Text text;

        public Queue<string> sentences;

        private void Awake()
        {
            _instance = this;
            instance = _instance;
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
           // DisplayNextSentence();
            // does it have options?  if there is, execute the action on it : no proceed to the next sentence.

        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {   Debug.Log("No more Dialouge");
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            text.text = sentence;
            Debug.Log(sentence);
        }

        public void EndDialogue()
        {
            Debug.Log("Dialogue Ended");
            text.text = "";
        }

    }
}
