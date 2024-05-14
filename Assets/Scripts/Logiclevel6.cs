using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logiclevel6 : MonoBehaviour
{
    public GameObject counter;
    public GameObject counter2;

    public int pageCount;
    public string winscreen;
    public int maxParrotCountForLevel;
    public int parrotCounts;


    void Start()
    {
        pageCount = 0;
        parrotCounts = 0;
        maxParrotCountForLevel = 0;
        Pathfinding.parrotCount = maxParrotCountForLevel;
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/10";
        counter2.GetComponent<Text>().text = parrotCounts + "/0";

        if (pageCount == 10)
        {
            SceneManager.LoadScene(winscreen);
        }
    }
}
