using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{
    [Serializable]
    public class Item : ScriptableObject
    {
        public string title { get; set; }
        public string description { get; set; }
        public bool isUnique { get; set; }
        public int maxStack { get; set; }
        public int stack { get; set; }

        
    }
}