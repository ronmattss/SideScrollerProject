using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class CollectTask : Task
    {
        // Start is called before the first frame update
        public bool triggerOnCollision = false;
        public bool finished;
        public bool disable = false;

        // Update is called once per frame
        /// <summary>
        /// Sent when another object enters a trigger collider attached to this
        /// object (2D physics only).
        /// </summary>
        /// <param name="other">The other Collider2D involved in this collision.</param>
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && triggerOnCollision)
            {
                finished = true;
                isFinished = finished;
                if (disable)
                    this.gameObject.SetActive(false);
            }
        }
        void Update()
        {
            if (triggerOnCollision)
                GetComponent<InteractableObject>().enabled = false;
            else
                GetComponent<InteractableObject>().enabled = true;
            if (finished)
            {
                isFinished = finished;
                if (disable)
                    this.gameObject.SetActive(false);
            }
        }
        public void Finish()
        {
            if (finished)
                finished = false;
            else{
                finished = true;
                isFinished = finished;
            }

        }
    }
}