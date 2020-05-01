using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
  public  GameObject whatTobeSpawned;
    void Start()
    {
       Instantiate(whatTobeSpawned);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
