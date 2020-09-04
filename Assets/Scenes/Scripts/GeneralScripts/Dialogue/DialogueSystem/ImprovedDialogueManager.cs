using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace SideScrollerProject
{
    public class ImprovedDialogueManager : MonoBehaviour
    {
        //handles displaying of dialogues
        // Queue sentences
        // 
        public static ImprovedDialogueManager instance;
        public GameObject dialogueHolder;
        public TMP_Text text;
        [SerializeField] public GameObject responseHolder;
        public DialogueReader currentReader;

        void Awake()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}