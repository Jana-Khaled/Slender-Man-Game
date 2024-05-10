using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logiclevel6 : MonoBehaviour
{
    public GameObject counter;

    public int pageCount;
     public string winscreen;


    void Start()
    {
        pageCount = 0;
        
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/10";

        if(pageCount == 10)
        {
            SceneManager.LoadScene(winscreen);
        }


    }
}
