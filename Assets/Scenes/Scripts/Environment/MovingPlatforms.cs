using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject posA;
    public GameObject posB;
    private Vector3 pA;
    private Vector3 pB;
    Vector3 nextPos;
    public float speed;
    public float waitTime;
    public bool movesVertical;
    public Rigidbody2D rb;

    List<Vector3> locations = new List<Vector3>();
    Vector3 currentPosition;
    void Start()
    {
        pA = posA.transform.position;
        pB = posB.transform.position;
        currentPosition = this.transform.localPosition;
        nextPos = pB;
        posA.transform.parent = null;
        posB.transform.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlatform();

    }
    //       Vector3 currentPosition = this.transform.position;

    //      this.gameObject.transform.position = Vector3.Lerp(currentPosition, locations[locationCount], Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / secondsForOneLength, 1f)));
    void MovePlatform()
    {


        this.gameObject.transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, nextPos) <= 0.1)
            ChangeDirection();
        // StartCoroutine(Wait());
    }
    void ChangeDirection()
    {
        nextPos = nextPos != pA ? pA : pB;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.parent = this.gameObject.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.parent = null;
        }
    }



    // IENumerator

    IEnumerator Wait()
    {
        // StopCoroutine(MovePlatform());
        yield return new WaitForSeconds(waitTime);
    }

}
