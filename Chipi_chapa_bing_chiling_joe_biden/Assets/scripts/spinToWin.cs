using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinToWin : MonoBehaviour
{
    public float dmg;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new UnityEngine.Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        transform.RotateAround(player.transform.position, UnityEngine.Vector3.up, 8);
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
