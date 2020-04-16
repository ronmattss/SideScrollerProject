using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace SideScrollerProject
{
    [Serializable]
    public class EntityManager : MonoBehaviour, ISpawn
    {
        public EntityManager instance;
        private EntityManager _instance;


        // what to spawn
        
        public void Start()
        {
            _instance = this;
            instance = _instance;

        }
        public void Spawn()
        {

        }

        
        



    }
}