using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBehaviour : enemyBehaviour
{
    public GameObject pocisk;

    private int counter = 0;

    private void Awake()
    {
        dmg = 50;
        speed = 8;
        health = 600;
        gravityForce = 200;
        goldReward = 2000;
        distance = 5.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        setup();

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if(counter % 20 == 0)
        {
            UnityEngine.Vector3 vec = transform.up;
            GameObject newPocisk = Instantiate(pocisk);
            newPocisk.transform.position = transform.position + vec * 7f;
            newPocisk.transform.rotation = UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0, Random.value * 360, 0));
            bossStrzela script = newPocisk.GetComponent<bossStrzela>();
        }
        counter++;



    }
}
