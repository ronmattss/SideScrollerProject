using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class SpiritOrb : MonoBehaviour
    {   //spawn an orb that can be absorbed by the player
        public int spiritAmount = 1;

        // Start is called before the first frame update
        void Start()
        {

        }
        private void OnEnable()
        {
            // Spawn this object
        }
        public void AbsorbSpirit()
        {
            PlayerManager.instance.GetPlayerStatus().playerIngameStats.CurrentResource += spiritAmount;
            Destroy(this.gameObject);
        }
        public void OnEntityDeath()
        {   Debug.Log("Spirit orb");
            this.transform.parent = null;
            this.gameObject.layer = LayerMask.NameToLayer("Interactable");
            this.GetComponent<InteractableObject>().enabled = true;
            this.GetComponent<CircleCollider2D>().enabled = true;
        }
        // Update is called once per frame  
    }
}