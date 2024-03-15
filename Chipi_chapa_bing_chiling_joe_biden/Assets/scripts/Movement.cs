using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float movespeed; // Speed of movement
    public float jumpspeed; // Force applied when jumping
    public float rotationspeed;
    public float gravityForce;
    public GameObject camera;
    public float caleraZOffset;
    public float cameraYOffset;
    public float cameraXRotation;
    public GameObject bullet;
    public GameObject enemy;

    private List<GameObject> bullets = new List<GameObject>();

    private UnityEngine.KeyCode forward = KeyCode.W;
    private UnityEngine.KeyCode backwards = KeyCode.S;
    private UnityEngine.KeyCode left = KeyCode.A;
    private UnityEngine.KeyCode right = KeyCode.D;
    private UnityEngine.KeyCode jump = KeyCode.Space;

    int timer = 0;
    int cooldown = 300;

    private float y_velocity = 0;
    private float z_velocity = 0;

    private float y_rotation = 0;


    private bool can_jump = true;

    private bool gravity = true;

 
    // Start is called before the first frame update
    void Start()
    {
        
        UnityEngine.Vector3 startPos = new UnityEngine.Vector3(10,4,3);
        UnityEngine.Vector3 startRot = new UnityEngine.Vector3(0, 0, 0);

        transform.position = startPos;
        transform.rotation = UnityEngine.Quaternion.Euler(startRot);
        camera.transform.rotation = UnityEngine.Quaternion.Euler(startRot);

        startPos.z += caleraZOffset;
        startPos.y += cameraYOffset;
        camera.transform.position = startPos;
        camera.transform.Rotate(cameraXRotation, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        timer++;
        if(timer == cooldown)
        {
            timer = 0;
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.transform.position = new UnityEngine.Vector3(Random.value * 30 - 15, 10, Random.value * 30 - 15);
        }



        z_velocity = 0;
        // Movement
        

        if (Input.GetKeyDown(jump) && can_jump)
        {
            y_velocity += jumpspeed / 100;
            can_jump = false;
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = transform.position;
            newBullet.transform.rotation = transform.rotation;
            iNeedMoreBullets script = newBullet.GetComponent<iNeedMoreBullets>();
            script.dmg = 10;
            script.speed = 0.05f;
            bullets.Add(bullet);
        }
        z_velocity = Input.GetAxis("Vertical");
        z_velocity *= movespeed / 100;
        y_rotation = Input.GetAxis("Horizontal");
        y_rotation *= rotationspeed / 100;
        
        if (gravity)
        {
            y_velocity -= gravityForce / 10000;
        }
        transform.Translate(0, y_velocity, z_velocity);
        if(transform.position.y < 2.95f)
        {
            //gravity = false;
            transform.position = new UnityEngine.Vector3(transform.position.x,3.05f,transform.position.z);
        }
       

        camera.transform.position = transform.position;
        camera.transform.Translate(0, cameraYOffset, caleraZOffset);

        
        transform.Rotate(0, y_rotation, 0);
        camera.transform.RotateAround(transform.position, UnityEngine.Vector3.up, y_rotation);

       

        // Jumping

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            can_jump = true;
            gravity = false;

        }
        y_velocity = 0;

    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Floor"))
    //    {
    //        can_jump = true;
    //        gravity = false;

    //    }
    //}

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            can_jump = false;
            gravity = true;
        }
        
        

    }
}