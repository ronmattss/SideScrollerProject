using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{
    public class SpikeController : MonoBehaviour
    {
        bool canDamage = true;
        public Vector2 hidePosition;
        public int damage = 10;
        public Transform teleportPosition;
        private Vector2 origPosition;
        private Vector2 nextPos;
        private Vector2 pA;
        private Vector2 pB;
        public bool isOnOrigin = true;
        public float waitTime = 1;
        public float timeMoving = 0.5f;
        public float repeatTime = 2f;
        public float invokeTime = 1f;
        public bool teleportToSafePlace = false;
        public bool moveVertical = true;
        public float position = 3;
        public bool spikeMove = true;

        //        LeanTween tween = new LeanTween();
        // Start is called before the first frame update
        void Start()
        {
            origPosition = this.transform.position;
            //hidePosition = new Vector2(this.transform.position.x, this.transform.position.y - 3);
            pA = origPosition;
            pB = hidePosition;
            nextPos = pA;

            if (spikeMove)
                InvokeRepeating("StartCoroutineMethod", invokeTime, repeatTime);



        }

        // Update is called once per frame
        void Update()
        {




        }
        void ChangeDirection()
        {
            nextPos = nextPos != pA ? pA : pB;
        }
        void StartCoroutineMethod()
        {
            if (moveVertical)
                StartCoroutine(SpikeyMoveVertical());
            else
                StartCoroutine(SpikeyMoveHorizontal());
        }

        IEnumerator SpikeyMoveVertical()
        {

            if (isOnOrigin)
            {
                LeanTween.moveY(this.gameObject, this.transform.position.y - position, timeMoving).setEaseInOutQuad();
                StartCoroutine(Wait());
                isOnOrigin = false;
            }
            else
            {
                LeanTween.moveY(this.gameObject, origPosition.y, timeMoving).setEaseInOutQuad();
                StartCoroutine(Wait());
                isOnOrigin = true;
            }
            if (isOnOrigin)
                yield return new WaitForSecondsRealtime(waitTime);
            //   StartCoroutine(Spikey());


        }

        IEnumerator SpikeyMoveHorizontal()
        {
            if (isOnOrigin)
            {
                LeanTween.moveX(this.gameObject, this.transform.position.x - position, timeMoving).setEaseInOutQuad();
                StartCoroutine(Wait());
                isOnOrigin = false;
            }
            else
            {
                LeanTween.moveX(this.gameObject, origPosition.x, timeMoving).setEaseInOutQuad();
                StartCoroutine(Wait());
                isOnOrigin = true;
            }
            yield return new WaitForSecondsRealtime(waitTime);

        }
        IEnumerator Wait()
        {

            yield return new WaitForSeconds(1);
            canDamage = true;
            yield return new WaitForSeconds(1);
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
                    other.GetComponent<PlayerStatus>().TakeDamage(damage);
                    StartCoroutine(Wait());
                    canDamage = false;
                    if (teleportToSafePlace)
                        other.transform.position = teleportPosition.position;

                }
            }
        }
    }
}
