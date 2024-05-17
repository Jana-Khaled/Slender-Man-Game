using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject counter;
    public int pageCount;
    public string newlevel;
    public int maxpages;
    public int currentlevel;

//papers' places: inside the building, 2 on the oil tanks, on the tunnel, the logsite beside the oil tanks

    void Start()
    {
        pageCount = 0;    
    }

    void Update()
    {

        if(pageCount==maxpages){
            counter.GetComponent<Text>().text = "Level" + currentlevel + "\n" + pageCount + "/"+  maxpages ;
            SceneManager.LoadScene(newlevel);
        }

        counter.GetComponent<Text>().text = "Level" + currentlevel + "\n" + pageCount + "/"+  maxpages ;

    }


}
