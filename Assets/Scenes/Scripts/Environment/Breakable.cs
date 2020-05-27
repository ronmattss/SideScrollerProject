using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{

    // Script that makes Objects breakables
    public class Breakable : MonoBehaviour
    {
        // Start is called before the first frame update
        // 
        public int hit = 1; // how many hits before being destroyed
        List<GameObject> parts = new List<GameObject>();
        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                parts.Add(transform.GetChild(i).gameObject);
                parts[i].SetActive(false);
            }

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (hit <= 0)
            {
                foreach (GameObject part in parts)
                {
                    part.transform.parent = null;
                    part.SetActive(true);
                    RandomForce(part);
                    Destroy(part.gameObject, 2);
                }

                Destroy(this.gameObject, 0);
            }
        }
        void OnDestroy()
        {
            gameObject.SetActive(false);

            // Spawn some of its parts
        }
        public void RandomForce(GameObject obj)
        {
            obj.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(Random.Range(10, 50), Random.Range(10, 500)), obj.transform.position);
        }
    }

}