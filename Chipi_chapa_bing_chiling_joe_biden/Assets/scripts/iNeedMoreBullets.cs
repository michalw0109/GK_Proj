using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class iNeedMoreBullets : MonoBehaviour
{

    public float speed;
    public float dmg;


    // Start is called before the first frame update
    void Start()
    {
        
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
            enemy_behaviour script = collision.gameObject.GetComponent<enemy_behaviour>();
            if (script != null)
            {
                script.health -= dmg;
            }
            collision.gameObject.GetComponent<Transform>().Translate(0, 1, 0);

            Destroy(gameObject);
        }

    }
}
