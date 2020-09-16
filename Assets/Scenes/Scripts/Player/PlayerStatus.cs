using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

namespace SideScrollerProject
{
    // seperate playerStats?
    // this now serves as the frontside of the script

    public class PlayerStatus : EntityStatus
    {
        // public Transform attackPoint;
        // public Transform searchPoint;
        // // public float searchRange = 0;
        // // public float attackRange = 0;
        //load base stats from scriptable objects
        public PlayerStats playerIngameStats;
        public DamageModifier damageModifier = new DamageModifier();
        public float knockbackForce = 250f;
        public Animator animator;
        public Slider healthSlider;
        public Slider resourceSlider;
        public SliderScript healthSliderScript;
        public SliderScript resourceSliderScript;
        public Actions actions;
        public float timeBeforeRegenerating = 3f;
        public int regenerateRate = 1;
        public bool canRegenerateResource = true;
        float countDown;
        public SpriteRenderer playerRenderer;
        public Material baseMaterial;
        public Material emissionMaterial;
        private CinemachineImpulseSource impulseSource;
        public CinemachineImpulseSource attackSource;


        // x+= (target -x) * .1) // go to target 10% of the value at the time fast -> slow
        void Start()
        {
            playerIngameStats = GetComponent<PlayerStats>();
            playerIngameStats.CurrentHealth = currentStatus.maxHealth;
            playerIngameStats.CurrentResource = (base.currentStatus as CharacterStatus).maxResource;
            playerIngameStats.BaseDamage = currentStatus.initialDamage;
            healthSliderScript.slider = healthSlider;
            resourceSliderScript.slider = resourceSlider;
            animator = this.GetComponent<Animator>();
            impulseSource = GetComponent<CinemachineImpulseSource>();
            animator.gameObject.SetActive(false);
            animator.gameObject.SetActive(true);
            // target = this.transform;
            healthSliderScript.SetMaxValue(base.currentStatus.maxHealth);
            resourceSliderScript.SetMaxValue((base.currentStatus as CharacterStatus).maxResource);
            countDown = timeBeforeRegenerating;

        }



        void Update()
        {
            RegenerateResource();
            // check current state
            // impulseSource.GenerateImpulse();
        }
        private void OnDrawGizmosSelected()
        {
            // if (searchPoint != null)
            //     Gizmos.DrawWireSphere(searchPoint.position, searchRange);
            // if (attackPoint != null)
            //     Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        public void TakeDamage(int damage, Animator enemyAnimator)
        {
            Debug.Log($"Damage:{damage}");
            playerIngameStats.CurrentHealth -= damage;
            healthSliderScript.SetValue(playerIngameStats.CurrentHealth);
            impulseSource.GenerateImpulse();
            CameraManager.instance.Shake(10, 0.2f);
            //  Knockback(enemyAnimator.GetComponentInParent<Transform>().localScale.x);
            animator.SetBool("IsHurt", true);
            ResetCountDown();
            EffectsManager.instance.Spawn(this.gameObject.transform.position, "PlayerHurt1");
            EffectsManager.instance.Spawn(this.gameObject.transform.position, "PlayerHurt2");
            LevelManager.instance.FreezeHit(0.25f);
            //PlayerManager.instance.GetPlayerAction().ApplyHitStop(null,0.5f);
            if (playerIngameStats.CurrentHealth <= 0)
            {

                this.gameObject.SetActive(false);
            }
            else
            {
                //Hurt
                Debug.Log(this.name + " " + playerIngameStats.CurrentHealth);
            }

        }
        public sealed override void TakeDamage(int baseDamage)
        {
            Debug.Log($"Damage:{baseDamage}");
            playerIngameStats.CurrentHealth -= baseDamage;
            healthSliderScript.SetValue(playerIngameStats.CurrentHealth);
            impulseSource.GenerateImpulse();
            CameraManager.instance.Shake(10, 0.2f);
            //  Knockback(enemyAnimator.GetComponentInParent<Transform>().localScale.x);
            animator.SetBool("IsHurt", true);
            ResetCountDown();
            EffectsManager.instance.Spawn(this.gameObject.transform.position, "PlayerHurt1");
            EffectsManager.instance.Spawn(this.gameObject.transform.position, "PlayerHurt2");
            LevelManager.instance.FreezeHit(0.25f);
            if (playerIngameStats.CurrentHealth <= 0)
            {

                this.gameObject.SetActive(false);
            }
            else
            {
                //Hurt
                Debug.Log(this.name + " " + playerIngameStats.CurrentHealth);
            }
        }
        // public void TakeDamage(int damage)
        // {
        //     Debug.Log($"Damage:{damage}");
        //     currentHealth -= damage;
        //     healthSliderScript.SetValue(currentHealth);
        //     // impulseSource.GenerateImpulse();
        //     CameraManager.instance.Shake(10, 0.2f);
        //     animator.SetBool("IsHurt", true);
        //     ResetCountDown();
        //     if (currentHealth <= 0)
        //     {

        //         this.gameObject.SetActive(false);
        //     }
        //     else
        //     {
        //         //Hurt
        //         Debug.Log(this.name + " " + currentHealth);
        //     }

        // }

        public void Die(Animator animator)
        {

        }
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnEnable()
        {
            playerIngameStats.CurrentHealth = base.currentStatus.maxHealth;
            playerIngameStats.CurrentResource = (base.currentStatus as CharacterStatus).maxResource;
            healthSliderScript.SetValue(playerIngameStats.CurrentHealth);
            resourceSliderScript.SetValue(playerIngameStats.CurrentHealth);
            this.gameObject.transform.position = LevelManager.instance.recentCheckpoint;
            Debug.Log("Ei im Alive");

            // teleport to latest shrine interacted with
        }
        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        void OnDisable()
        {

        }
        public void RegenerateResource()
        {
            if (canRegenerateResource)
            {
                if (countDown > 0)
                {
                    countDown -= Time.deltaTime;
                }
                else
                {
                    if (playerIngameStats.CurrentResource != (base.currentStatus as CharacterStatus).maxResource)
                    {
                        playerIngameStats.CurrentResource += regenerateRate;
                        resourceSliderScript.SetValue(playerIngameStats.CurrentResource);
                    }
                    else
                    {
                        ResetCountDown();
                    }

                }
            }
            else
            {
                ResetCountDown();
            }
        }
        public void ResetCountDown()
        {
            countDown = timeBeforeRegenerating;
        }


        public void Knockback(float direction)
        {
            Vector2 knockback = new Vector2(knockbackForce * direction, 100);
            this.gameObject.GetComponent<Rigidbody2D>().AddForce((knockback), ForceMode2D.Force);

        }
        public void DepleteResourceBar(int value)
        {
            playerIngameStats.CurrentResource -= value;
            resourceSliderScript.SetValue(playerIngameStats.CurrentResource);
        }
        public void ChangeHealthBar(int value)
        {
            if (value > 0)
            {
                playerIngameStats.CurrentHealth += value;
                healthSliderScript.SetValue(playerIngameStats.CurrentHealth);
            }
            else
            {
                playerIngameStats.CurrentHealth -= value;
                healthSliderScript.SetValue(playerIngameStats.CurrentHealth);
            }
        }



        public int GetHealth()
        {
            return playerIngameStats.CurrentHealth;
        }
        public int GetResource()
        {
            return playerIngameStats.CurrentResource;
        }





        // Seperate slider functions unique to the player

    }
}