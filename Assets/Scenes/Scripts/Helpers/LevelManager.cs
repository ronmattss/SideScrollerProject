using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject player;
    public GameObject wizard;
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
    public void FreezeHit()
    {
        StartCoroutine(FreezeLevel());
    }

    IEnumerator FreezeLevel()
    {

        Time.timeScale = 0.00f;
        yield return new WaitForSecondsRealtime(0.01f);
        Time.timeScale = 1f;


    }
}
