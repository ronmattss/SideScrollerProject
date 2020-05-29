using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(menuName = "Abilities/Player/BlinkStrike")]
    public class AlphaStrike : Ability
    {
        //how does this ability works?
        //Ready State
        //first scan all enemies and breakables in range (could be a raycast)
        // 2nd  lock enemies in position 

        // sheathe State
        // 3rd execute animation then blink to the last enemy
        // 4th last pose animation, damage all enemies

        public float range = 4;
        public int damage = 20;
        PlayerStatus playerStatus;
        GameObject parent;
        List<GameObject> listOfDamagables = new List<GameObject>();
        public LayerMask whatToHit;
        public GameObject splash;
        public override void Initialize(GameObject obj)
        {
            parent = obj;
            playerStatus = obj.GetComponent<PlayerStatus>();
        }

        public override void TriggerAbility()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Select all enemies in range
        // will be called in the Ready State
        #region ReadyState
        public void LockOnEnemies()
        {// linecast to a direction, then lock on all targets
            Vector2 direction = parent.transform.localScale.x == -1 ? Vector2.left : Vector2.right;
            Debug.DrawLine(parent.transform.position, new Vector2(direction.x * range + parent.transform.position.x, parent.transform.position.y), Color.red, 5);
            RaycastHit2D[] hit = Physics2D.LinecastAll(parent.transform.position, new Vector2(direction.x * range + parent.transform.position.x, parent.transform.position.y), whatToHit);
            foreach (RaycastHit2D enemy in hit)
            {
                if (enemy.collider == null) return;
                listOfDamagables.Add(enemy.collider.gameObject);

            }
            Debug.Log(listOfDamagables.Count);
            //   RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, new Vector3(finalTargetPosition.x, finalTargetPosition.y, 0) - raycastOrigin.position, Mathf.Infinity, playerLayer);
        }

        // will be called in the Sheathe weapon Exit State

        public void FreezeEnemyPositions()
        {
            foreach (GameObject enemy in listOfDamagables)
            {
                enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (enemy.TryGetComponent(out Animator anim))
                {
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isMoving", false);
                    anim.SetBool("playerOnSight", false);
                    anim.SetBool("playerInRange", false);
                }
            }
        }
        // Refactor this, Blink to the last enemy

        public bool r(GameObject g)
        {
            if (g.CompareTag("Enemy")) return true;
            else return false;
        }
        public void BlinkPlayer()
        {
            Vector3 direction = parent.transform.localScale.x == -1 ? Vector2.left : Vector2.right;
            direction *= 2;
            Vector3 pos;
            try
            {
                pos = listOfDamagables[listOfDamagables.FindLastIndex(g => g.CompareTag("Enemy"))].transform.position + direction;
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                Debug.Log(e.ActualValue);
                try
                {
                    pos = listOfDamagables[listOfDamagables.Count - 1].gameObject.transform.position + direction;
                }
                catch (System.ArgumentOutOfRangeException ex)
                {
                    // do nothing
                    Debug.Log(ex.ActualValue);
                    pos = parent.transform.position;

                }
            }



            parent.transform.position = pos + direction;
            // parent.transform.position = listOfDamagables[listOfDamagables.FindLastIndex(g => g.CompareTag("Enemy"))].transform.position;

            LeanTween.move(this.parent, pos, 0.2f);
//            Debug.Log("What is: " + listOfDamagables[listOfDamagables.Count - 1].transform.name);
            LeanTween.move(Camera.main.gameObject, pos + direction, 0.2f);
        }
        #endregion
        #region StrikeState
        public void StrikeEnemies()
        {
            foreach (GameObject enemy in listOfDamagables)
            {
                if (enemy.TryGetComponent(out Status enemyStatus))
                    enemyStatus.TakeDamage(damage, false);

                if (enemy.TryGetComponent(out Breakable b))
                {
                    b.hit = 0;
                }


            }
            listOfDamagables.Clear();
        }
        #endregion
        public void SetSplash()
        {
            GameObject splashObject = Instantiate(splash, this.parent.transform.position, Quaternion.identity);
            splashObject.transform.parent = Camera.main.transform;
            splashObject.SetActive(true);
        }

        // Update is called once per frame
    }
}