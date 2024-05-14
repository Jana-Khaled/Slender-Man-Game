using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logiclevel3 : MonoBehaviour
{
    public GameObject counter;
    public GameObject counter2;

    public int pageCount;
    public string level4;
    public int maxGhostsCountForLevel;
    public int parrotCounts;


    void Start()
    {
        pageCount = 0;
        parrotCounts = 0;
        maxGhostsCountForLevel = 3;
        Pathfinding.ghostCount = maxGhostsCountForLevel;
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/7";
        counter2.GetComponent<Text>().text = parrotCounts + "/3";

        if (pageCount == 7)
        {
            SceneManager.LoadScene(level4);
        }
    }
}
