using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logiclevel5 : MonoBehaviour
{
    public GameObject counter;
    public GameObject counter2;

    public int pageCount;
    public string level6;
    public int maxGhostsCountForLevel;
    public int parrotCounts;


    void Start()
    {
        pageCount = 0;
        parrotCounts = 0;
        maxGhostsCountForLevel = 1;
        Pathfinding.ghostCount = maxGhostsCountForLevel;
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/9";
        counter2.GetComponent<Text>().text = parrotCounts + "/1";

        if (pageCount == 9)
        {
            SceneManager.LoadScene(level6);
        }
    }
}
