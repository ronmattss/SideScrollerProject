using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public float origLocation;
    void Start()
    {
        origLocation = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           // other.transform.parent = this.gameObject.transform;
            StartCoroutine(CollapsePlatform());
        }
    }

    IEnumerator CollapsePlatform()
    {
        yield return new WaitForSeconds(0.3f);
        // collapse platform
        gameObject.GetComponent<Collider2D>().enabled = false;
        transform.LeanMoveY(origLocation-3,0.1f);
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Collider2D>().enabled = true;
        transform.LeanMoveY(origLocation,0.5f);

    }
}
