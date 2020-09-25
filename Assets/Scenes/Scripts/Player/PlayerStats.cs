using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class PlayerStats : MonoBehaviour
    {
        // seperation of concerns
        // status of the player in game
        // status effects will be stored here
        // stats of the player will be stored here
        // so that it is seperated from other scripts
        [SerializeField] private int currentHealth;
        [SerializeField] private int currentResource;
        [SerializeField] private int baseDamage;
        [SerializeField] private int modifiedDamage;
        public List<BuffMultiplyDamage> listOfBuffs = new List<BuffMultiplyDamage>(); // list of active buffs

        public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public int CurrentResource { get => currentResource; set => currentResource = value; }
        public int BaseDamage { get => baseDamage; set => baseDamage = value; }

        // Start is called before the first frame update

        void Awake()
        {

            modifiedDamage = baseDamage;
        }

        // Update is called once per frame
        void Update()
        {
            modifiedDamage = ModifiedDamage();
            // Debug.Log("Modifiers count: " + listOfBuffs.Count);
        }
        public void RemoveModifier(TimedBuff buff)
        {
            BuffMultiplyDamage tempBuff = (buff as BuffMultiplyDamage);
            //  Debug.Log(" removing buff");
            listOfBuffs.Remove(tempBuff);
            //  Debug.Log("Modifiers count: " + listOfBuffs.Count);
        }
        public int ModifiedDamage()
        {
            Debug.Log("Modifiers count: " + listOfBuffs.Count + " Damage: " + modifiedDamage);
            int computedDamage = BaseDamage;  // buffs give status effects (spirit buff, cursed status effect)
                                              // buffs also 
                                              // damage += for each (status effects that modify damage)
                                              // for each status effect add/subtract damage
                                              // ex. Spirit buff effect gives + 10 damage
                                              // cursed buffect gives -5 damage
                                              // compute 10 + -5 + baseDamage
            if (listOfBuffs.Count == 0)
                return computedDamage;
            else
            {

                foreach (var buffer in listOfBuffs)
                {
                    computedDamage += buffer.extraDamage;
                }
                return computedDamage;
            }
        }
    }
}