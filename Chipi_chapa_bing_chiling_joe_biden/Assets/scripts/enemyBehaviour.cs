using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyBehaviour : MonoBehaviour
{


    public float speed = 0.01f;
    public float health = 30;
    public GameObject scoreManager;


    //Wymienilismy na public :)
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        health = 30;
    }

    // Update is called once per frame
    public void Update()
    {
        if(health <= 0)
        {
            ScoreManager script = scoreManager.GetComponent<ScoreManager>();
            script.AddPoint();

            Destroy(gameObject);
        }

        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        transform.Translate(direction * speed);
        
    }
}
