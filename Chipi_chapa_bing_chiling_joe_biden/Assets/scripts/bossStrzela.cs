using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossStrzela : MonoBehaviour
{
    public int dmg = 20;
    private float yVelocity;
    private int lifeTime = 300;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
        yVelocity = Random.value * 0.20f + 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        

        transform.Translate(0, yVelocity, 0.1f);
        yVelocity -= 0.0005f;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindWithTag("Player");
            NewBehaviourScript playerScript = player.GetComponent<NewBehaviourScript>();
            if (playerScript != null)
            {
                playerScript.hp -= dmg;
            }
            

            Destroy(gameObject);
        }

    }
}
