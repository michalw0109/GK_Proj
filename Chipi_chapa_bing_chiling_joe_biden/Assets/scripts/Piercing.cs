using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piercing : MonoBehaviour
{
    public float speed;
    public float dmg;


    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyBehaviour script = collision.gameObject.GetComponent<enemyBehaviour>();
            if (script != null)
            {
                script.health -= dmg;
            }
            collision.gameObject.GetComponent<Transform>().Translate(0, 1, 0);

        }

    }
}
