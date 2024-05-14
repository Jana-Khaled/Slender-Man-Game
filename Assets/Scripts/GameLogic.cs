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
    public int maxGhostsCountForLevel;
    public int ghostCounts;

    void Start()
    {
        pageCount = 0;
        ghostCounts = 0;
        maxGhostsCountForLevel = 5;
        Pathfinding.ghostCount = maxGhostsCountForLevel;
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/5";
        counter2.GetComponent<Text>().text = ghostCounts + "/5";

        if (pageCount == 5)
        {
            SceneManager.LoadScene(level2);
        }


    }
}
