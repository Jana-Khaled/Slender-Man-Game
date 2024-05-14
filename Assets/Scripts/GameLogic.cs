using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject counter;
    public GameObject counter2;

    public int pageCount;
    public string level2;
    public int maxParrotCountForLevel;
    public int parrotCounts;

    void Start()
    {
        pageCount = 0;
        parrotCounts = 0;
        maxParrotCountForLevel = 5;
        Pathfinding.parrotCount = maxParrotCountForLevel;
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/5";
        counter2.GetComponent<Text>().text = parrotCounts + "/5";

        if (pageCount == 5)
        {
            SceneManager.LoadScene(level2);
        }


    }
}
