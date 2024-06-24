using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leczymy : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Destroy(gameObject, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
