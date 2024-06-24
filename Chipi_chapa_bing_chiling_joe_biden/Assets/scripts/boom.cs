using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    public float dmg;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
