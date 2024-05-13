using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logiclevel3 : MonoBehaviour
{
    public GameObject counter;

    public int pageCount;
    public string level4;


    void Start()
    {
        pageCount = 0;
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/7";

        if (pageCount == 7)
        {
            SceneManager.LoadScene(level4);
        }
    }
}
