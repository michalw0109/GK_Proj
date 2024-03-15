using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemy_behaviour : MonoBehaviour
{


    public float speed = 0.01f;
    public float health = 30;



    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        health = 30;


    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * speed);

        
        
    }



}
