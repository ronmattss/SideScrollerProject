﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{// Simply overrides list of Clip animation
    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; } //?????????
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name)); //????????????????
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value); //?????????????????????????????
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}