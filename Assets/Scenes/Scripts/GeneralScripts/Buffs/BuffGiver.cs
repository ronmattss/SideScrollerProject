﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BuffGiver
// MonoBehaviour class which sits on skills/ attacks/ etc...
// gives buffs to player or enemies
// 
namespace SideScrollerProject
{
    public class BuffGiver : MonoBehaviour
    {
        public List<Buff> buffsToInit = new List<Buff>();
        [SerializeField] public List<TimedBuff> buffsToGive = new List<TimedBuff>();
        public GameObject receiver;
        public TimedBuff t;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void InitialiazeBuffs()
        {
            foreach (var b in buffsToInit)
            {
                buffsToGive.Add(b.InitializeBuff(receiver));
            }
        }

        public void GiveBuff(Buff _buff)
        {
            t = _buff.InitializeBuff(receiver);
            Debug.Log("Buff Receiever: " + t.obj.name + " "+receiver.name);
            t.obj = receiver;
            buffsToGive.Add(t);
            if (buffsToGive.Count > 0)
                foreach (var buff in buffsToGive)
                {
                    receiver.GetComponent<BuffReceiver>().AddBuff(buff);
                }
        }


    }

}