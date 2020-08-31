using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ImprovedDialogueManager : MonoBehaviour
{
    //handles displaying of dialogues
    // Queue sentences
    // 
    public static ImprovedDialogueManager instance;
    public GameObject dialogueHolder;
    public TMP_Text text;



    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
