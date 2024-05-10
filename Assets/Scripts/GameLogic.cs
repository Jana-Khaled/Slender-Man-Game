using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public GameObject counter;

    public int pageCount;


    void Start()
    {
        pageCount = 0;
        
    }



    void Update()
    {
        counter.GetComponent<Text>().text = pageCount + "/8";

    }
}
