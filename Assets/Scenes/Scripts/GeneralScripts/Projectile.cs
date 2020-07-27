using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class Projectile : MonoBehaviour
    {

        public Vector2 target;
        [SerializeField] ParticleSystem explosion;
        PlayerStatus playerStatus;
        [SerializeField] int damage;
        [SerializeField] float speed;
        [SerializeField] float duration;
        [SerializeField] bool isTargeting;
        [SerializeField] Vector2 travelVelocity;
        int maxTarget;
        float direction;
        Rigidbody2D rb;
        // Start is called before the first frame update



        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //Launch(debugTarget.position);
        }
        private void FixedUpdate()
        {

        }
        public void Launch(float direction)
        {

            rb = this.gameObject.GetComponent<Rigidbody2D>();

            if (direction > 0)
            {
                rb.velocity = Vector2.right * speed;
            }
            else if (direction < 0)
            {
                rb.velocity = Vector2.left * speed;
                this.transform.localScale = new Vector2(direction, 1);
            }
            Destroy(this.gameObject, duration);
        }


        //  Rotate to the direction of the target
        //  Launch to that direction
        // optional Homing???
        public void Launch(Vector2 targetPosition, float direction)
        {
            rb = this.gameObject.GetComponent<Rigidbody2D>();
            //Find the Angle between two points
            Vector2 originVector = this.transform.position;
            float angle = Mathf.Rad2Deg * Mathf.Atan2(targetPosition.y - originVector.y, targetPosition.x - originVector.x) - 90f;
            Debug.Log("Angle between two vectors: " + angle);
            this.transform.LeanRotateZ(angle, 0);
            travelVelocity = targetPosition - originVector;//slows down lmao
            //rb.velocity = new Vector2(direction * 10,travelVelocity.y); // Change to a constant X(left of right ) and a dynamic 
            rb.AddForce(this.transform.up * speed, ForceMode2D.Impulse);

        }
        // will be used to rain fire idk
        public void ParabolicLaunch(Vector2 targetPosition)
        {

        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerStatus = other.GetComponent<PlayerStatus>();
                if (explosion != null)
                {
                    explosion.gameObject.transform.parent = null;
                    explosion.Play();
                    Destroy(explosion.gameObject, 1);
                }
                playerStatus.TakeDamage(damage);
                Destroy(this.gameObject, 1);
                this.gameObject.SetActive(false);

            }
        }
    }

}