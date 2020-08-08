using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SideScrollerProject
{
    public class EffectsManager : MonoBehaviour
    {// List of FX that can be spawned and despawn on given time
     // Start is called before the first frame update
        public Effect[] effects;
        private Cinemachine.CinemachineImpulseSource shake;
        public static EffectsManager instance;
        void Awake()
        {   // Make sure 
            if (instance == null)
                instance = this;
            else
            {
                Destroy(this.gameObject);
                return;
            }
            shake = GetComponent<Cinemachine.CinemachineImpulseSource>();
            //

        }

        // Update is called once per frame


        public void Spawn(Vector3 position, string name)
        {

            Effect effect = Array.Find(effects, effects => effects.effectName == name);
            if (effect == null)
                return;

            GameObject effectInstance = Instantiate(effect.effectObject, position, Quaternion.identity);
            Destroy(effectInstance,1);
        }
        public void Shake()
        {
            shake.GenerateImpulse();
        }
    }

}