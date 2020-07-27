using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{

    // Script that makes Objects breakables
    // initialize breakable something
    // given a list of sprites
    // create child gameobjects with rigidbody sprite renderer collider2d
    public class Breakable : MonoBehaviour
    {
        // Start is called before the first frame update
        // 
        public int hit = 1; // how many hits before being destroyed
        [SerializeField] private Sprite damagedSprite; // if this is null then completely break the prefab else show damaged Sprite
        [SerializeField] private Sprite[] listOfSprites;
        List<GameObject> parts = new List<GameObject>();
        void Start()
        {
            if (this.transform.childCount > 0 || listOfSprites.Length == 0)
                for (int i = 0; i < transform.childCount; i++)
                {
                    parts.Add(transform.GetChild(i).gameObject);
                    parts[i].SetActive(false);
                }
            else
            {
                // count how many sprites
                // Instantiate GameObject
                // setactive false
                foreach (var sprite in listOfSprites)
                {
                    var temp = Instantiate(CreateDestroyedVersion(sprite), this.transform.position, Quaternion.identity);
                    parts.Add(temp);
                    temp.transform.parent = this.transform;
                    temp.SetActive(false);
                }
            }

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (hit <= 0 && parts.Count > 0)
            {
                try
                {

                    foreach (GameObject part in parts)
                    {

                        // part.transform.parent = null;
                        part.SetActive(true);
                        RandomForce(part);
                        Destroy(part.gameObject, 2);
                        parts.Remove(part);
                    }
                }
                catch (System.Exception)
                {

                }

                if (damagedSprite == null)              // destroy object when damaged
                    Destroy(this.gameObject, 0);
                else                                    // show show damaged Sprite 
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = damagedSprite;
            }
        }
        void OnDestroy()
        {
            gameObject.SetActive(false);

            // Spawn some of its parts
        }
        public void RandomForce(GameObject obj)
        {
            obj.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(PlayerManager.instance.GetPlayerTransform().localScale.x * Random.Range(100, 1000), Random.Range(10, 1000)), obj.transform.position);
        }
        public GameObject CreateDestroyedVersion(Sprite sprite)
        {
            GameObject destroyedObject = new GameObject();
            // destroyedObject.AddComponent<Transform>(); //????
            destroyedObject.AddComponent<Rigidbody2D>();
            //destroyedObject.AddComponent<PolygonCollider2D>();
            destroyedObject.AddComponent<SpriteRenderer>().sprite = sprite;
            destroyedObject.GetComponent<SpriteRenderer>().sortingLayerName = this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
            //destroyedObject.transform.parent = this.gameObject.transform;
            destroyedObject.tag = "Breakables";
            destroyedObject.layer = LayerMask.NameToLayer("Breakables");
            return destroyedObject;

        }
    }

}