using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class Spawner : MonoBehaviour
    {
        public GameObject template;
        public int waves = 1;
        public int numberToSpawn = 0;
        int waveCounter = 0;
        int numberToSpawnCounter = 0;
        public List<Vector3> transformPositions;
        public GameObject[] enemiesToBeSpawned;
        public List<Transform> patrolPoints;
        public List<GameObject> spawnedEnemies = new List<GameObject>();
        public bool lastSpawn = false;
        // Start is called before the first frame update
        void Start()
        {
            template = new GameObject();

            transformPositions.Capacity = this.transform.childCount;
            foreach (Transform child in this.transform)
            {
                transformPositions.Add(child.transform.position);
            }
            numberToSpawnCounter = numberToSpawn;

        }

        // Update is called once per frame
        void Update()
        {
            if (waveCounter > waves)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {

                if (spawnedEnemies.Count == 0)
                {
                    if (waveCounter < waves)
                    {
                        for (; numberToSpawnCounter > 0; numberToSpawnCounter--)
                        {
                            Spawn();
                        }
                        numberToSpawnCounter = numberToSpawn;
                    }
                    waveCounter++;
                }
                else
                {

                }
                try
                {
                    foreach (GameObject enemy in spawnedEnemies)
                    {
                        if (enemy == null)
                        {
                            spawnedEnemies.Remove(enemy);
                        }
                    }

                }
                catch (System.InvalidOperationException)
                {
                    Debug.Log("Collection is being Modified");
                }

            }


        }

        void Spawn()
        {
            int randNum = Random.Range(0, transformPositions.Count);
            Vector3 localPosition = transformPositions[randNum];

            GameObject spawnedEnemy = Instantiate(enemiesToBeSpawned[Random.Range(0, enemiesToBeSpawned.Length)], transformPositions[Random.Range(0, transformPositions.Count)], Quaternion.identity);
            spawnedEnemy.transform.parent = this.transform;
            spawnedEnemies.Add(spawnedEnemy);
            if (patrolPoints.Count > 0)
            {
                SetPatrolPoints();
            }
        }
        void SetPatrolPoints()
        {
            foreach (var enemy in spawnedEnemies)
            {
                enemy.transform.parent = null;
                GameObject x = Instantiate(template, new Vector3(patrolPoints[0].position.x, enemy.transform.position.y, 0), Quaternion.identity);
                GameObject y = Instantiate(template, new Vector3(patrolPoints[1].position.x, enemy.transform.position.y, 0), Quaternion.identity);
                x.name = "PosA" + enemy.GetInstanceID() + Random.Range(0, 100);
                y.name = "PosB" + enemy.GetInstanceID() + Random.Range(0, 100);
                enemy.GetComponent<Status>().pointA = x.transform;
                enemy.GetComponent<Status>().pointB = y.transform;
                enemy.GetComponent<Status>().isPatrolling = true;
                Destroy(x);
                Destroy(y);

            }
        }


    }
}