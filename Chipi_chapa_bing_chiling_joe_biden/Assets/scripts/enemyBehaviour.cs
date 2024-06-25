using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyBehaviour : MonoBehaviour
{

    public int dmg; 
    public float speed;
    public float health;
    public float gravityForce;
    public int goldReward;
    public float distance;


    public UnityEngine.Rigidbody rb;

    public GameObject player;
    public NewBehaviourScript playerScript;

    private UnityEngine.Vector3 direction;


    private void Awake()
    {
        dmg = 10;
        speed = 15;
        health = 30;
        gravityForce = 200;
        goldReward = 10;
        distance = 2.3f;
    }

    // Start is called before the first frame update
    void Start()
    {
        setup();
    }


    public void setup()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        playerScript = player.GetComponent<NewBehaviourScript>();
    }


    // Update is called once per frame
    public void Update()
    {
        if(health <= 0)
        {
            rewardPlayer();
            Destroy(gameObject);
        }

        direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        rb.velocity = direction * speed;

        RaycastHit hitInfo;
        if (!Physics.Raycast(transform.position + (UnityEngine.Vector3.up * 0.1f), UnityEngine.Vector3.down, out hitInfo, distance))
        {
            rb.AddForce(transform.up * -gravityForce);
        }



        //Debug.Log(direction);


    }

    public void rewardPlayer()
    {
        playerScript.gold += playerScript.combo / 5 + goldReward;
        playerScript.points += playerScript.combo / 5 + goldReward;
    }
}
