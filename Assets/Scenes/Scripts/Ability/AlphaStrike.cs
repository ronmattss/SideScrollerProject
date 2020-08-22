using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

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
        public GameObject popUp;
        public GameObject omniHit;
        public float whenToTriggerSplash = 0;
        public float whenToTriggerFX = 0;
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
                if (!isInList(enemy.collider.gameObject.name))
                    listOfDamagables.Add(enemy.collider.gameObject);

            }
            Debug.Log(listOfDamagables.Count);
            parent.transform.GetChild(6).gameObject.SetActive(true);
            //   RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, new Vector3(finalTargetPosition.x, finalTargetPosition.y, 0) - raycastOrigin.position, Mathf.Infinity, playerLayer);
        }
        public bool isInList(string name)
        {
            foreach (GameObject e in listOfDamagables)
            {
                if (e.name == name)
                    return true;
            }
            return false;
        }

        // will be called in the Sheathe weapon Exit State

        public void FreezeEnemyPositions(int freeze = 0)
        {
            foreach (GameObject enemy in listOfDamagables)
            {
                if(freeze == 0 )
                enemy.GetComponent<Status>().moveSpeed = freeze;
                else
                enemy.GetComponent<Status>().moveSpeed =  enemy.GetComponent<Status>().speed;
                if (enemy.TryGetComponent(out Animator anim))
                {
                    anim.speed = freeze;
                    anim.SetBool("isBeingAttacked", true);
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
                if (enemy.TryGetComponent(out Animator anim))
                {
                    anim.SetBool("isBeingAttacked", false);
                }


            }
            listOfDamagables.Clear();
            //          Destroy(popUp.gameObject);
            //            Destroy(splash.gameObject);
        }
        #endregion
        public void SetSplash()
        {
            GameObject splashObject = Instantiate(splash, this.parent.transform.position, Quaternion.identity);
            splashObject.transform.parent = Camera.main.transform;
            splashObject.transform.localPosition = new Vector3(-0.029f, -0.054f, 10);
            splashObject.transform.localScale = new Vector3(1.265934f, 1.308018f, 1);

            // ScaleSplash(splashObject);
            splashObject.SetActive(true);
        }
        public void SpawnHit()
        {
            int last = listOfDamagables.Count;
            GameObject x = Instantiate(omniHit, listOfDamagables[0].transform.position, Quaternion.identity);
            x.transform.localScale = new Vector3(Vector3.Distance(listOfDamagables[0].transform.position, listOfDamagables[last - 1].transform.position), 3f, 1);


        }
        public void OpenPopUp()
        {
            Debug.Log("pop");
            GameObject pop = Instantiate(popUp, this.parent.transform.position, Quaternion.identity);
            pop.transform.parent = Camera.main.transform;
            pop.transform.localPosition = new Vector3(-2.51f, -1.44f, 10);
            if (parent.transform.localScale.x == -1)
            {
                pop.transform.localPosition = new Vector3(2.47f, -1.44f, 10);
                pop.transform.localScale = new Vector3(-1, 1, 1);
            }
            // ScaleSplash(pop);
            pop.SetActive(true);
        }

        public void ScaleSplash(GameObject obj)
        {
            Transform transform = obj.transform;
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr == null) return;

            transform.localScale = new Vector3(1, 1, 1);

            float width = sr.sprite.bounds.size.x;
            float height = sr.sprite.bounds.size.y;


            float worldScreenHeight = Camera.main.orthographicSize * 2f;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            Vector3 xWidth = transform.localScale;
            xWidth.x = worldScreenWidth / width;
            transform.localScale = xWidth;
            //transform.localScale.x = worldScreenWidth / width;
            Vector3 yHeight = transform.localScale;
            yHeight.y = worldScreenHeight / height;
            transform.localScale = yHeight;
            //transform.localScale.y = worldScreenHeight / height;

        }

        // Update is called once per frame
    }
}