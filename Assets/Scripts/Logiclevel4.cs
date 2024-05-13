using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logiclevel4 : MonoBehaviour
{
    public GameObject counter;

    public int pageCount;
    public string level5;


    void Start()
    {
        pageCount = 0;
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/8";

        if (pageCount == 8)
        {
            SceneManager.LoadScene(level5);
        }
    }
}
