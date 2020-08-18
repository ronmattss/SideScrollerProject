using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class ComponentAdder : MonoBehaviour
    {
        public string abilityNameToBeAdded;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void AddComponent(Object component)
        {
            
            PlayerManager.instance.player.AddComponent(component.GetType());
        }
        public void AddAbility(Ability ability)
        {
            PlayerManager.instance.GetPlayerAbility(abilityNameToBeAdded).Initialize(ability);
        }
    }
}