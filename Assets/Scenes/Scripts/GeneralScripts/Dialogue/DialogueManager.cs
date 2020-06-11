using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SideScrollerProject
{
    // UI manager?
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;
        private DialogueManager _instance;
        public TMP_Text text;
        public GameObject dialoguePlaceHolder;
        public GameObject interactText;

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
            if (sentences.Count == 0 || sentences.Peek() == " ")
            {

                Debug.Log("No more Dialouge");
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            text.text = sentence;
            Debug.Log(sentence);
        }

        public void DisplaySentence(string chatDialogue)
        {
            if (chatDialogue == " ")
            {
                EndDialogue();
            }
            text.text = chatDialogue;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(chatDialogue));
        }
        IEnumerator TypeSentence(string sentence)
        {
            text.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                text.text += letter;
                yield return new WaitForSecondsRealtime(0.015f);
            }
        }
        public void SetTransparency(bool fade)
        {
            StartCoroutine(FadeInText(fade));
        }
        public IEnumerator FadeInText(bool fade)
        {
            StopCoroutine("FadeInText");
            if (fade)
            {   //to Transparent
                // for (float i = 1; i >= 0; i -= Time.deltaTime)
                // {
                //     byte x = (byte)i;
                //     interactText.GetComponent<TMP_Text>().alpha = i * 2;
                //     Debug.Log("TEXTShit: " + i);
                interactText.GetComponent<TMP_Text>().alpha = 0;
                yield return null;
                // }
            }
            else
            {   //to Opaque
                for (float i = 0; i <= 0.5; i += Time.deltaTime)
                {
                    byte x = (byte)i;
                    interactText.GetComponent<TMP_Text>().alpha = i * 2;
                    yield return null;
                }
            }
        }
        public void DisableInteractText()
        {
            if (interactText.GetComponent<TMP_Text>().alpha <= 255)
                interactText.GetComponent<TMP_Text>().alpha = 0;
            else
                interactText.GetComponent<TMP_Text>().alpha = 255;

        }



        public void EndDialogue()
        {
            dialoguePlaceHolder.SetActive(false);
            Debug.Log("Dialogue Ended");
            text.text = "";
        }

        Vector4 hexColor(float r, float g, float b, float a)
        {
            Vector4 color = new Vector4(r * 255, g * 255, b * 255, a * 255);
            return color;
        }
    }
}
