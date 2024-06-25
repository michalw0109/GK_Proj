using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy3Behaviour : enemyBehaviour
{
    private float timer = 0;
    private float counter = 0;
    private float cooldown = 0.6f;
    private int tick = 1;

    private void Awake()
    {
        dmg = 20;
        speed = 25;
        health = 5;
        gravityForce = 200;
        goldReward = 5;
        distance = 2.3f;
    }

    // Start is called before the first frame update
    void Start()
    {
        setup();

    }


    // Update is called once per frame
    new void Update()
    {
        timer += Time.deltaTime;

        if (timer > counter)
        {
            counter += cooldown;
            tick *= -1;

        }

        if(tick == 1)
        {
            base.Update();
        }
        else
        {
            rb.velocity -= rb.velocity;
        }

    }
}
