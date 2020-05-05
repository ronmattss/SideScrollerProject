using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int waves = 1;
    public int numberToSpawn = 0;
    int waveCounter = 0;
    int numberToSpawnCounter = 0;
    public List<Vector3> transformPositions;
    public GameObject[] enemiesToBeSpawned;
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
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
            Destroy(this.gameObject);
        else
        {
            if (spawnedEnemies.Count == 0)
            {
                for (; numberToSpawnCounter > 0; numberToSpawnCounter--)
                {
                    Spawn();
                }
                numberToSpawnCounter = numberToSpawn;
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
    }


}
