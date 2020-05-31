using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class Projectile : MonoBehaviour
    {
        PlayerStatus playerStatus;
        [SerializeField] int damage;
        [SerializeField] float speed;
        [SerializeField] float duration;
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerStatus = other.GetComponent<PlayerStatus>();
                playerStatus.TakeDamage(damage);
                Destroy(this.gameObject);
            }
        }
    }

}