using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{
    public class SpikeController : MonoBehaviour
    {
        bool canDamage = true;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            StartCoroutine(Spike());
        }
        IEnumerator Wait()
        {
            canDamage = false;
            yield return new WaitForSeconds(1);
            canDamage = true;
        }
        IEnumerator Spike()
        {
            this.transform.LeanScaleY(0, 1);
            yield return new WaitForSeconds(1);
            this.transform.LeanScaleY(1, 1);
            yield return new WaitForSeconds(1);
        }

        /// <summary>
        /// Sent when another object enters a trigger collider attached to this
        /// object (2D physics only).
        /// </summary>
        /// <param name="other">The other Collider2D involved in this collision.</param>
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                if (canDamage)
                {
                    other.GetComponent<PlayerStatus>().TakeDamage(10);
                    StartCoroutine(Wait());
                }
            }
        }
    }
}
