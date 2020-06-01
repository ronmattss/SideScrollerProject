using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New State", menuName = "ShadedGames/StateAbilityData/Attack")]
    public class AttackState : StateData
    {
        // Start is called before the first frame update
        private int attackCounter = 0;
        private int counter = 0;

        public Transform attackPoint;
        public float attackRange;
        public int attackDamage = 20;
        //    public GameObject gameObject;
        public LayerMask enemyLayer;
        public GameObject hitEffect;
        public bool flipEffectatX = false;
        bool isHit = false;
        AudioSource audio;
        public AudioClip sound;

        public void FreezeHit()
        {
            
        }

        public void RegisterAttack(Animator animator)
        {
            // Register enemies
            Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            // Damage enemies
            counter++;
            foreach (Collider2D enemy in enemyColliders)
            {
                Debug.Log($"Enemy hit: {enemy.name} {counter}{enemy.tag}");
                Status enemyStatus = enemy.gameObject.GetComponent<Status>();


                if (enemy.tag == "Breakables")
                {
                    if (enemy.TryGetComponent(out Breakable b))
                        b.hit--;
                    Debug.Log("eeeee");
                }
                if (enemyStatus == null)
                    return;
                else
                {
                    enemyStatus.TakeDamage(attackDamage);
                    if (hitEffect != null)
                    {
                        GameObject hit = Instantiate(hitEffect, enemy.transform.position, Quaternion.identity);
                        hit.transform.localScale = new Vector2(animator.transform.localScale.x, 1);
                        LeanTween.scaleY(hit, 3f, 0.3f).setEaseOutExpo();
                        if (!flipEffectatX)
                            LeanTween.scaleX(hit, 3f * animator.transform.localScale.x, 0.3f).setEaseOutExpo();
                        else
                            LeanTween.scaleX(hit, 3f * -animator.transform.localScale.x, 0.3f).setEaseOutExpo();
                    }
                    
                }
                if (enemy.tag == "Breakables")
                {
                    enemy.GetComponent<Breakable>().hit--;
                    Debug.Log("eeeee");
                }
                isHit = true;

            }
            LevelManager.instance.FreezeHit();



        }




        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            attackPoint = animator.gameObject.GetComponent<Transform>().GetChild(0);
            //  Debug.Log(attackPoint.name);

            attackCounter = animator.GetInteger(AnimatorParams.AttackCounter.ToString());
            audio = animator.GetComponent<AudioSource>();
            audio.clip = sound;
            audio.Play();
            RegisterAttack(animator);

            animator.SetBool(AnimatorParams.Attacking.ToString(), false);





        }
        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            //   
            //  Debug.Log("Attack Call");
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {


            // if (animator.GetInteger(AnimatorParams.AttackCounter.ToString()) > 1)
            //   {
            // animator.SetBool(AnimatorParams.Attacking.ToString(), true);
            //    animator.SetBool(AnimatorParams.Attacking.ToString(), false);
            //   animator.SetInteger(AnimatorParams.AttackCounter.ToString(), 0);
            //  animator.GetComponentInParent<InputManager>().attackCounter = 0;
            //    InputManager.instance.attackCounter = 0;
            // }
            //  animator.SetInteger(AnimatorParams.AttackCounter.ToString(), 2);
        }




    }
}

