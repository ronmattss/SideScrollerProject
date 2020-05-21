using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject wizard;
    void Start()
    {

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
}
