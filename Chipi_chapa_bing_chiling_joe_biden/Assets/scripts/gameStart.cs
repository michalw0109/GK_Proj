using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameStart : MonoBehaviour
{

    public GameObject player;
    public GameObject hud;
    

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player);
        Instantiate(hud);

     
    }

    // Update is called once per frame
    void Update()
    {
       


    }
}
