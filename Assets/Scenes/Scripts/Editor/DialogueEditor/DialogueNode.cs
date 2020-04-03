using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;


namespace SideScrollerProject
{
    public class DialogueNode : Node
    {
        // Start is called before the first frame update
        public string GUID;

        public string DialougeText;
        public bool EntryPoint = false;
    }
}