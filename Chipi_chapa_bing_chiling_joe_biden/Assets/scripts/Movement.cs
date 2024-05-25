using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager;
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
    public GameObject sword;
    public GameObject enemy;
    public GameObject box;
    public Material czerwony;
    public Material czarny;
    public GameObject musicBox;

    private List<GameObject> bullets = new List<GameObject>();
    private List<GameObject> swords = new List<GameObject>();


    private UnityEngine.KeyCode forward = KeyCode.W;
    private UnityEngine.KeyCode backwards = KeyCode.S;
    private UnityEngine.KeyCode left = KeyCode.A;
    private UnityEngine.KeyCode right = KeyCode.D;
    private UnityEngine.KeyCode jump = KeyCode.Space;

    private UnityEngine.Rigidbody rb;

    private float z_velocity = 0;

    private float y_rotation = 0;


    public float attackCooldown = 0.1f;
    private float attackTimer;

    private float timer = 0f;

    public float timing = 0.05f;

    private float bpm;
    private float nextSxtnhBeat = 0;
    private float nextBeat = 0;


    public float enemyCooldown = 5;
    private float nextEnemy = 0;

  

    private bool can_jump = true;


    private int counter = 0;
    private int songSetupCounter = 0;

    private int combo = 0;
    private float multipleHitTimer;
    private bool alreadyHitForMultiple = false;

    private float noHitTimer;
    private bool alreadyHitForNoHit = false;



    // Start is called before the first frame update
    void Start()
    {

        UnityEngine.Vector3 startPos = new UnityEngine.Vector3(10, 4, 3);
        UnityEngine.Vector3 startRot = new UnityEngine.Vector3(0, 0, 0);

        transform.position = startPos;
        transform.rotation = UnityEngine.Quaternion.Euler(startRot);
        camera.transform.rotation = UnityEngine.Quaternion.Euler(startRot);

        startPos.z += caleraZOffset;
        startPos.y += cameraYOffset;
        camera.transform.position = startPos;
        camera.transform.Rotate(cameraXRotation, 0, 0);

        rb = GetComponent<Rigidbody>();

        bpm = 1 / 2.5f;

        musicBox.GetComponent<AudioSource>().Play();
        musicBox.GetComponent<AudioSource>().Stop();


    }

    // Update is called once per frame
    void Update()
    {

        updateTimer();

        checkGround();

        toTheBeat();

        songSetup();

        someOtherTimer();

        attacksAndCombo();

        movement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        

    }

    private void updateTimer()
    {
        timer += Time.deltaTime;
        multipleHitTimer += Time.deltaTime;
        noHitTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
    }
    private void toTheBeat()
    {
        // tu timer do bitu
        if (timer > nextBeat)
        {
            nextBeat += bpm;
            counter++;
            if (counter % 2 == 0)
            {
                box.GetComponent<MeshRenderer>().material = czerwony;
            }
            else
            {
                box.GetComponent<MeshRenderer>().material = czarny;
            }
        }
    }

   

    private void songSetup()
    {
        // tylko do puszczania piosenki w dobry bit
        if (timer > nextSxtnhBeat)
        {
            if (songSetupCounter % 4096 == 5)
            {
                musicBox.GetComponent<AudioSource>().Play();
            }
            if (songSetupCounter % 4096 == 4)
            {
                musicBox.GetComponent<AudioSource>().Stop();
            }
            nextSxtnhBeat += bpm / 16f;
            songSetupCounter++;
        }
    }

    private void someOtherTimer()
    {
        if (timer > nextEnemy)
        {
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.transform.position = new UnityEngine.Vector3(Random.value * 30 - 15, 10, Random.value * 30 - 15);

            nextEnemy += enemyCooldown;
        }
    }

    private void movement()
    {
        // jump
        if (Input.GetKeyDown(jump) && can_jump)
        {
            rb.velocity = new UnityEngine.Vector3(rb.velocity.x, jumpspeed, rb.velocity.z);
        }

        // input przod tyl i obrot
        z_velocity = Input.GetAxis("Vertical");
        z_velocity *= movespeed;
        y_rotation = Input.GetAxis("Horizontal");
        y_rotation *= rotationspeed / 100;

        // takie rozne proby
        //UnityEngine.Vector3 velocity = new UnityEngine.Vector3(0, y_velocity, z_velocity);
        //rb.velocity = transform.InverseTransformDirection(transform.forward * z_velocity);
        //UnityEngine.Vector3 upVelocity = new UnityEngine.Vector3(0, y_velocity, 0); ;
        //UnityEngine.Vector3 velocity = transform.forward * z_velocity + new UnityEngine.Vector3(0, y_velocity, 0);

        // dodawanie predkosci do poruszania
        UnityEngine.Vector3 planeVelocity = transform.forward * z_velocity;
        rb.velocity = new UnityEngine.Vector3(0, rb.velocity.y, 0) + planeVelocity;

        // dodatkowa grawitacja
        if (!can_jump)
        {
            rb.AddForce(transform.up * -gravityForce);
        }

        // translacja kamery do gracza
        camera.transform.position = transform.position;
        camera.transform.Translate(0, cameraYOffset, caleraZOffset);

        // rotacja gracza i kamery
        transform.Rotate(0, y_rotation, 0);
        camera.transform.RotateAround(transform.position, UnityEngine.Vector3.up, y_rotation);
    }

    private void attacksAndCombo()
    {

        // reset timerow
        if (multipleHitTimer > 2 * timing)
        {
            alreadyHitForMultiple = false;
        }
        if (noHitTimer > bpm)
        {
            alreadyHitForNoHit = false;
        }



        // setup do atakow
        shoot();
        slash();

        // czy w tamtym bicie aby nie bylo ataku
        if (nextBeat - timer < bpm - timing && nextBeat - timer > timing && !alreadyHitForNoHit)
        {
            combo = 0;
        }

    }

    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // check do spamownia
            if (attackTimer < attackCooldown)
            {
                return;
            }
            attackTimer = 0;

            Debug.Log(combo);

            // tu sprawdzenie do comba
            checkAttackInBeat();

            
            UnityEngine.Vector3 vec = transform.forward;
            vec.y += 0.5f;
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = transform.position + vec * 2.5f;
            newBullet.transform.rotation = transform.rotation;
            iNeedMoreBullets script = newBullet.GetComponent<iNeedMoreBullets>();
            script.dmg = 10;
            script.speed = 0.05f;
            bullets.Add(bullet);
        }
    }

    private void slash()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            // check do spamownia
            if (attackTimer < attackCooldown)
            {
                return;
            }
            attackTimer = 0;

            Debug.Log(combo);

            // tu sprawdzenie do comba
            checkAttackInBeat();

            
            UnityEngine.Vector3 vec = transform.forward + transform.right;
            vec.y += 0.5f;
            GameObject newSword = Instantiate(sword);
            newSword.transform.position = transform.position + vec * 2.5f;
            newSword.transform.rotation = transform.rotation;
            newSword.transform.Rotate(90, 0, 0);

            ciachanieMieczem script = newSword.GetComponent<ciachanieMieczem>();
            script.dmg = 10;
            swords.Add(sword);

        }
    }

    private void checkAttackInBeat()
    {
        // check czy trafienie dobrze siad³o
        if (nextBeat - timer > bpm - timing || nextBeat - timer < timing)
        {
            // czy w tym bicie juz byl atak
            if (alreadyHitForMultiple)
            {
                combo = 0;
            }
            combo++;
            alreadyHitForMultiple = true;
            alreadyHitForNoHit = true;
            multipleHitTimer = 0;
            noHitTimer = 0;
        }
        else
        {
            // nie trafilismy
            combo = 0;
        }
    }

    private void checkGround()
    {
        RaycastHit hitInfo;
        float distance = 0.3f;
        can_jump = Physics.Raycast(transform.position + (UnityEngine.Vector3.up * 0.1f), UnityEngine.Vector3.down, out hitInfo, distance);
    }


}