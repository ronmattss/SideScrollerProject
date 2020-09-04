using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SideScrollerProject
{
    public class ResponseHolder : MonoBehaviour
    {

        public BranchingDialogue currentDialogue;
        public bool isShowing;
        private Regex regex = new Regex(@"^[0-9-]*$");
        [SerializeField] List<TMP_Text> listOfResponsesText = new List<TMP_Text>();
        void Start()
        {

        }


        // Update is called once per frame
        void Update()
        {
            //when enabled
            //either on Button Press or key stroke
            if (isShowing)
            {
                // you can input now
                var input = Input.inputString;
                Debug.Log("Inputting: " + input);
                Debug.Log("is input a digit: " + regex.IsMatch(input));
                if (!string.IsNullOrWhiteSpace(input) && regex.IsMatch(input))
                {
                    int index = Convert.ToInt16(input);
                    if (index > currentDialogue.respondDialogue.Count)
                        index = 0;
                    ImprovedDialogueManager.instance.currentReader.dialogue = currentDialogue.respondDialogue[index];

                    if (currentDialogue.respondEvent[index] != null)
                        currentDialogue.respondEvent[index].Invoke(); // Invoke next Dialogue Event or current?
                    ImprovedDialogueManager.instance.currentReader.DisplayNextSentence(); //invoke UnityEvent

                    this.gameObject.SetActive(false);
                }
            }
            //

        }
        public void DisplayResponses(bool show, BranchingDialogue d)
        {

            ReadResponses(d);
            ShowHideResponses(show);
        }

        public void ReadResponses(BranchingDialogue d)
        {
            currentDialogue = d;
            for (var i = 0; i < d.responses.Count; i++)
            {


                listOfResponsesText[i].gameObject.SetActive(true);
                Debug.Log("response: " + d.responses[i]);
                listOfResponsesText[i].text = d.responses[i];
                Debug.Log("response Display: " + listOfResponsesText[i].text);

            }
        }

        public void ShowHideResponses(bool showHide) =>
            listOfResponsesText.ForEach(x =>
            {
                if (!string.IsNullOrWhiteSpace(x.text))
                {
                    x.gameObject.SetActive(showHide);
                }
            });

        void OnEnable()
        {

        }


    }
}