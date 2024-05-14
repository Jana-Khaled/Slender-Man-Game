using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logiclevel2 : MonoBehaviour
{
    public GameObject counter;
    public GameObject counter2;

    public int pageCount;
    public string level3;
    public int maxGhostsCountForLevel;
    public int parrotCounts;

    void Start()
    {
        pageCount = 0;
        parrotCounts = 0;
        maxGhostsCountForLevel = 4;
        Pathfinding.ghostCount = maxGhostsCountForLevel;
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/6";
        counter2.GetComponent<Text>().text = parrotCounts + "/4";

        if (pageCount == 6)
        {
            SceneManager.LoadScene(level3);
        }
    }
}
