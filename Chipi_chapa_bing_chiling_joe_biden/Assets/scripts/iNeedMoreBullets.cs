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
<<<<<<< HEAD
        Destroy(gameObject, 3f);
=======
        Destroy(gameObject, 20f);

>>>>>>> 4aa0fe340859903df58ec01b3dd4573ad43c7ae5
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

            Destroy(gameObject);
        }

    }
}
