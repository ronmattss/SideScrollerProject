using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class WindSlashScript : MonoBehaviour
    {
        Status enemyStatus;
        int damage;
        float speed;
        float duration;
        int entitiesPassed; // number of enemies passed
        int maxTarget;
        float direction;
        Rigidbody2D rb;
        SpriteRenderer slashRenderer;
        Material material;
        public Buff instaKill;
        private BuffGiver b;
        private Scenes.Scripts.BossFSM.StateUser bossDamage;
        [SerializeField] private BuffInstaKill timed;
        void Start()
        {

            b = this.gameObject.AddComponent<BuffGiver>();


        }
        void Update()
        {
            if (entitiesPassed == maxTarget)
            {
                Destroy(this.gameObject);
            }
        }
        public void InitializeStats(int _damage, int _maxTarget, float _speed, float _duration, float _direction)
        {
            damage = _damage;
            speed = _speed;
            duration = _duration;
            maxTarget = _maxTarget;
            direction = _direction;
            transform.localScale = new Vector3(1 * direction, 1, 1);
        }
        public void Launch()
        {

            rb = this.gameObject.GetComponent<Rigidbody2D>();

            if (this.transform.localScale.x > 0)
            {
                rb.velocity = Vector2.right * speed;
            }
            else if (this.transform.localScale.x < 0)
            {
                rb.velocity = Vector2.left * speed;
            }
            Destroy(this.gameObject, duration);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {


                b.receiver = other.gameObject;

                // if (other.GetComponent<BuffReceiver>() == null)
                //     other.gameObject.AddComponent<BuffReceiver>();
                // // b.buffsToGive.Add(instaKill.InitializeBuff(b.receiver));
                //  other.gameObject.GetComponent<BuffReceiver>().AddBuff(instaKill.InitializeBuff(other.gameObject));
                // //b.GiveBuff(instaKill);
                // timed = (BuffInstaKill)b.t;
                enemyStatus = other.GetComponent<Status>();
                enemyStatus.TakeDamage(damage);
                entitiesPassed++;

            }
            else if (other.tag == "Boss")
            {
                if(other.TryGetComponent(out Scenes.Scripts.BossFSM.StateUser bossDamage))
                {
                    bossDamage.TakeDamage(damage);
                }
            }
            else if (other.tag == "Breakables")
            {
                if (other.TryGetComponent(out Breakable b))
                {
                    b.hit = 0;
                }
            }


        }
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                b.receiver = null;
            }


        }
    }
}
