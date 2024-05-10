using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevel : MonoBehaviour
{
    public string sceneName;
    public float waitTime;

    void Start()
    {
        StartCoroutine(toNextScene());
    }
    IEnumerator toNextScene()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
    }
}