using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2Behaviour : enemyBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        speed = 0.04f;
        health = 10;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
