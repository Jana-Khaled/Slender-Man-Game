using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logiclevel2 : MonoBehaviour
{
    public GameObject counter;

    public int pageCount;
     public string level3;


    void Start()
    {
        pageCount = 0;
        
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/6";

        if(pageCount == 6)
        {
            SceneManager.LoadScene(level3);
        }


    }
}
