using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyBehaviour : MonoBehaviour
{

    public int dmg = 10;
    public float speed = 1;
    public float health = 30;
    public float gravityForce = 10;
    public int goldReward = 10;
    public float distance = 1.3f;


    public UnityEngine.Rigidbody rb;

    public GameObject player;
    public NewBehaviourScript playerScript;

    // Start is called before the first frame update
    void Start()
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

        UnityEngine.Vector3 direction = (player.transform.position - transform.position).normalized;
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
        playerScript.gold += goldReward;
    }
}
