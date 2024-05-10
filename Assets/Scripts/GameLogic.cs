using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject counter;

    public int pageCount;
     public string level2;


    void Start()
    {
        pageCount = 0;
        
    }


    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/5";

        if(pageCount == 5)
        {
            SceneManager.LoadScene(level2);
        }


    }
}
