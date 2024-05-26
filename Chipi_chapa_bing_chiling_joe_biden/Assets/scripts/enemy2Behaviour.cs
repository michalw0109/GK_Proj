using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2Behaviour : enemyBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        playerScript = player.GetComponent<NewBehaviourScript>();
        distance = 1.3f;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
