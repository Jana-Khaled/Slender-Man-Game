using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logiclevel5 : MonoBehaviour
{
    public GameObject counter;

    public int pageCount;
    public string level6;


    void Start()
    {
        pageCount = 0;
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/9";

        if (pageCount == 9)
        {
            SceneManager.LoadScene(level6);
        }
    }
}
