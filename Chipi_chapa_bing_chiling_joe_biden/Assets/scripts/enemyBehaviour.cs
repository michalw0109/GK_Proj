using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyBehaviour : MonoBehaviour
{


    public float speed = 1;
    public float health = 30;
    public float gravityForce = 10;

    public UnityEngine.Rigidbody rb;

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        health = 30;
        rb = GetComponent<Rigidbody>();


    }

    
    
    

    // Update is called once per frame
    public void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        UnityEngine.Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        rb.velocity = direction * speed;


        rb.AddForce(transform.up * -gravityForce);

        //Debug.Log(direction);


    }
}
