using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    public GameObject shockWave;
    public float dmg;
    private float yVelocity = 0.07f;
    private int lifeTime = 300;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTime<=0)
        {
            GameObject newBoom = Instantiate(shockWave);
            newBoom.transform.position = transform.position;
            boom script = newBoom.GetComponent<boom>();
            script.dmg = dmg;
            Destroy(gameObject);
        }
        lifeTime--;
        transform.Translate(0, yVelocity, 0.1f);
        yVelocity -= 0.0005f;
    }
}
