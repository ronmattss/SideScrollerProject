using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject player;
    public GameObject wizard;
   // public float timeStopDuration = 0.025f;
    bool isFreeze;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || wizard == null)
        {
            StartCoroutine(ReloadLevel());
        }
    }
    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StopAllCoroutines();
    }
    public void FreezeHit(float duration =  0.150f)
    {
        if (isFreeze)
            return;
        StartCoroutine(FreezeLevel(duration));
    }

    IEnumerator FreezeLevel(float duration)
    {
        isFreeze = true;
        Time.timeScale = 0.00f;
      //  Debug.Log(System.DateTime.Now.ToLongTimeString() + " " + System.DateTime.Now.Millisecond + " Time Scale: " + Time.timeScale);
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
       // Debug.Log(System.DateTime.Now.ToLongTimeString() + " " + System.DateTime.Now.Millisecond + " Time Scale: " + Time.timeScale);

        isFreeze = false;


    }

    public void TeleportPlayer(Vector3 location)
    {
        player.transform.position = location;
    }
}
