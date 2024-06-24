using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    public float movespeed; // Speed of movement
    public float jumpspeed; // Force applied when jumping
    public float rotationspeed;
    public float gravityForce;
    private GameObject camera;
    public GameObject cameraPreset;

    public float caleraZOffset;
    public float cameraYOffset;
    public float cameraXRotation;
    public GameObject bullet;
    public GameObject sword;
    public GameObject dagger;
    public GameObject plasma;
    public GameObject healing;
    public GameObject dashing;
    public GameObject bomb;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    private GameObject box;
    public GameObject boxPreset;

    public Material czerwony;
    public Material czarny;
    private GameObject musicBox;
    public GameObject musicBoxPreset;



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
    private float enemySpawnScaling = 0.98f;
    private float minEnemyCooldown = 0.5f;
    private float enemyHealthScaling = 2f;
    private float enemyHealthMult = 1;


    private bool can_jump = true;


    private int counter = 0;
    private int songSetupCounter = 0;

    public int combo = 0;
    public int specialCombo = 0;

    private float multipleHitTimer;
    private bool alreadyHitForMultiple = false;

    private float noHitTimer;
    private bool alreadyHitForNoHit = false;



    public int hp = 100;
    public int hpMax = 100;
    public int gold = 0;

    public int hitCooldown = 2;
    private float hitTimer = 0;

    private int shootLvl = 1;
    private int slashLvl = 1;
    private int spinLvl = 1;
    private int healLvl = 1;
    private int piercingLvl = 1;
    private int dashLvl = 1;
    private int AOELvl = 1;

    public int points = 0;



    // Start is called before the first frame update
    void Start()
    {
        camera = Instantiate(cameraPreset);
        
        musicBox = Instantiate(musicBoxPreset);

        UnityEngine.Vector3 startPos = new UnityEngine.Vector3(10, -50, 3);
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

        box = Instantiate(boxPreset);
        //box.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        updateTimer();

        checkGround();

        toTheBeat();

        songSetup();

        enemySpawn();


        attacksAndCombo();

        upgradeHub();

        movement();


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyBehaviour script = collision.gameObject.GetComponent<enemyBehaviour>();
            if (script != null)
            {
                if(hitTimer > hitCooldown)
                {
                    hp -= script.dmg;
                    if (hp < 0)
                    {
                        Destroy(gameObject);
                    }
                    hitTimer = 0;
                }
                
            }

        }

    }

    private void updateTimer()
    {
        timer += Time.deltaTime;
        multipleHitTimer += Time.deltaTime;
        noHitTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        hitTimer += Time.deltaTime;
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
            if (songSetupCounter % 4096 == 3)
            {
                musicBox.GetComponent<AudioSource>().Play();
            }
            if (songSetupCounter % 4096 == 2)
            {
                musicBox.GetComponent<AudioSource>().Stop();
            }
            nextSxtnhBeat += bpm / 16f;
            songSetupCounter++;
        }
    }

    private void enemySpawn()
    {
        if (timer > nextEnemy)
        {
            spawnEnemy(1);
            spawnEnemy(2);
            spawnEnemy(3);


            nextEnemy += enemyCooldown;
            enemyCooldown *= enemySpawnScaling;
            enemyHealthMult *= enemyHealthScaling;
            if(enemyCooldown < minEnemyCooldown)
            {
                enemyCooldown = minEnemyCooldown;
            }
        }
    }


    private void spawnEnemy(int nr)
    {

        int choice = (int)(Random.value * 4 + 1);
        GameObject newEnemy;
       
        if(nr == 1)
        {
            newEnemy = Instantiate(enemy1);
            enemyBehaviour enemyScript = newEnemy.GetComponent<enemyBehaviour>();
            enemyScript.health *= enemyHealthMult;
        }
        else if(nr == 2)
        {
            newEnemy = Instantiate(enemy2);
            enemy2Behaviour enemyScript = newEnemy.GetComponent<enemy2Behaviour>();
            enemyScript.health *= enemyHealthMult;
        }
        else if(nr == 3)
        {
            newEnemy = Instantiate(enemy3);
            enemy3Behaviour enemyScript = newEnemy.GetComponent<enemy3Behaviour>();
            enemyScript.health *= enemyHealthMult;
        }
        else
        {
            newEnemy = Instantiate(enemy1);
            enemyBehaviour enemyScript = newEnemy.GetComponent<enemyBehaviour>();
            enemyScript.health *= enemyHealthMult;
        }

        

        if (choice == 1)
        {
            newEnemy.transform.position = new UnityEngine.Vector3(-212, -50, -10);
        }
        else if (choice == 2)
        {
            newEnemy.transform.position = new UnityEngine.Vector3(72, -50, -55);
        }
        else if (choice == 3)
        {
            newEnemy.transform.position = new UnityEngine.Vector3(138, -50, -54);
        }
        else
        {
            newEnemy.transform.position = new UnityEngine.Vector3(35, -50, 122);
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
        


        // reset timer
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
        spin();
        piercing();
        AOE();
        heal();
        dash();
 

        // czy w tamtym bicie aby nie bylo ataku
        if (nextBeat - timer < bpm - timing && nextBeat - timer > timing && !alreadyHitForNoHit)
        {

            combo = 0;
            specialCombo = 0;
        }

    }

    private void checkAttackInBeat(char source)
    {
        // check czy trafienie dobrze siad³o
        if (nextBeat - timer > bpm - timing || nextBeat - timer < timing)
        {
            // czy w tym bicie juz byl atak
            if (alreadyHitForMultiple)
            {
                combo = 0;
                specialCombo = 0;
            }

            checkSpecialCombo(source);

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
            specialCombo = 0;

        }
    }

    private void checkSpecialCombo(char source)
    {
        if (specialCombo == 0 || specialCombo == 1)
        {
            if (source == 'R')
            {
                specialCombo++;
            }
            else
            {
                specialCombo = 0;
            }
        }
        else if (specialCombo == 2 || specialCombo == 3)
        {
            if (source == 'T')
            {
                specialCombo++;
            }
            else
            {
                specialCombo = 0;
            }
        }
        else if (specialCombo == 4 || specialCombo == 5)
        {
            if (source == 'Y')
            {
                specialCombo++;
            }
            else
            {
                specialCombo = 0;
            }
        }
        else if (specialCombo == 6 || specialCombo == 7)
        {
            if (source == 'U')
            {
                specialCombo++;
            }
            else
            {
                specialCombo = 0;
            }
        }
        else if (specialCombo == 8)
        {
            if (source == 'I')
            {
                specialCombo++;
            }
            else
            {
                specialCombo = 0;
            }
        }
        else if (specialCombo == 9)
        {
            if (source == 'O')
            {
                specialCombo++;
            }
            else
            {
                specialCombo = 0;
            }
        }
        else if (specialCombo == 10 || specialCombo == 11)
        {
            if (source == 'Y')
            {
                specialCombo++;
            }
            else
            {
                specialCombo = 0;
            }
        }
        else if (specialCombo == 12 || specialCombo == 13 || specialCombo == 14 || specialCombo == 15)
        {
            if (source == 'P')
            {
                specialCombo++;
            }
            else
            {
                specialCombo = 0;
            }
        }
        else if (specialCombo == 16)
        {
            combo += 50;
            gold += 300;
            points += 2000;
            specialCombo = 0;
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


            // tu sprawdzenie do comba
            checkAttackInBeat('Y');

            //Debug.Log(specialCombo);

            UnityEngine.Vector3 vec = transform.forward;
            vec.y += 0.5f;
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = transform.position + vec * 2.5f;
            newBullet.transform.rotation = transform.rotation;
            iNeedMoreBullets script = newBullet.GetComponent<iNeedMoreBullets>();
            script.speed = 0.2f;
            script.dmg = 3 * shootLvl + combo / 5;
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

            //Debug.Log(combo);



            // tu sprawdzenie do comba
            checkAttackInBeat('U');
            //Debug.Log(specialCombo);


            UnityEngine.Vector3 vec = 1.9f * transform.forward + 0.8f * transform.right;
            vec.y += 0.5f;
            GameObject newSword = Instantiate(sword);
            newSword.transform.position = transform.position + vec * 2.5f;
            newSword.transform.rotation = transform.rotation;
            newSword.transform.Rotate(90, 0, 0);

            ciachanieMieczem script = newSword.GetComponent<ciachanieMieczem>();
            script.dmg = 20 + 4 * slashLvl + combo / 5;

        }
    }

    private void spin()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // check do spamownia
            if (attackTimer < attackCooldown)
            {
                return;
            }
            attackTimer = 0;


            // tu sprawdzenie do comba
            checkAttackInBeat('I');
            //Debug.Log(specialCombo);


            UnityEngine.Vector3 vec = 1.5f * transform.forward;
            vec.y += 0.5f;
            GameObject newDagger = Instantiate(dagger);
            newDagger.transform.position = transform.position + vec * 2.5f;
            newDagger.transform.rotation = transform.rotation;
            newDagger.transform.Rotate(90, 0, 0);

            spinToWin script = newDagger.GetComponent<spinToWin>();
            script.dmg = 30 + 5 * spinLvl + combo / 5;

        }
    }

    private void heal()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            // check do spamownia
            if (attackTimer < attackCooldown)
            {
                return;
            }
            attackTimer = 0;


            // tu sprawdzenie do comba
            checkAttackInBeat('O');
            //Debug.Log(specialCombo);


            GameObject newHeal = Instantiate(healing);

            hp += 2 * healLvl + combo / 5;
            if ( hp > hpMax)
            {
                hp = hpMax;
            }

        }
    }

    private void piercing()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // check do spamownia
            if (attackTimer < attackCooldown)
            {
                return;
            }
            attackTimer = 0;


            // tu sprawdzenie do comba
            checkAttackInBeat('P');

            //Debug.Log(specialCombo);

            UnityEngine.Vector3 vec = transform.forward;
            vec.y += 0.5f;
            GameObject newPlasma = Instantiate(plasma);
            newPlasma.transform.position = transform.position + vec * 2.5f;
            newPlasma.transform.rotation = transform.rotation;
            Piercing script = newPlasma.GetComponent<Piercing>();
            script.speed = 0.12f;
            script.dmg = 2 * piercingLvl + combo / 10;
        }
    }

    private void dash()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // check do spamownia
            if (attackTimer < attackCooldown)
            {
                return;
            }
            attackTimer = 0;


            // tu sprawdzenie do comba
            checkAttackInBeat('R');
            //Debug.Log(specialCombo);

            //UnityEngine.Vector3 vec = transform.forward;
            //vec.y += 0.5f;

            float direction = Input.GetAxis("Vertical");
            if(direction != 0)
            {
                GameObject newDash = Instantiate(dashing);
                newDash.transform.position = transform.position;
                newDash.transform.rotation = transform.rotation;

                transform.position = transform.position + (direction * transform.forward * 3 * dashLvl);
            }
            
            //UnityEngine.Vector3 planeVelocity = -transform.forward * 2000 * dashLvl;
            //rb.velocity = new UnityEngine.Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z) + planeVelocity;

        }
    }

    private void AOE()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // check do spamownia
            if (attackTimer < attackCooldown)
            {
                return;
            }
            attackTimer = 0;


            // tu sprawdzenie do comba
            checkAttackInBeat('T');

            //Debug.Log(specialCombo);
            UnityEngine.Vector3 vec2 = transform.forward;

            UnityEngine.Vector3 vec = transform.up;
            GameObject newBomb = Instantiate(bomb);
            newBomb.transform.position = transform.position + vec * 4f + vec2 * 2f;
            newBomb.transform.rotation = transform.rotation;
            AOE script = newBomb.GetComponent<AOE>();
            script.dmg = 3 * AOELvl + combo / 5;
        }
    }
    private void upgradeHub()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            upgrade('Y');
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            upgrade('U');
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            upgrade('I');
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            upgrade('O');
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            upgrade('P');
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            upgrade('R');
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            upgrade('T');
        }
    }
    private void upgrade(char attackType)
    {
        switch (attackType)
        {
            case 'Y':
                if(gold >= 100 * shootLvl)
                {
                    gold -= 100 * shootLvl;
                    shootLvl++;
                }
                break;
            case 'U':
                if (gold >= 100 * slashLvl)
                {
                    gold -= 100 * slashLvl;
                    slashLvl++;
                }
                break;
            case 'I':
                if (gold >= 100 * spinLvl)
                {
                    gold -= 100 * spinLvl;
                    spinLvl++;
                }
                break;
            case 'O':
                if (gold >= 100 * healLvl)
                {
                    gold -= 100 * healLvl;
                    healLvl++;
                }
                break;
            case 'P':
                if (gold >= 100 * piercingLvl)
                {
                    gold -= 100 * piercingLvl;
                    piercingLvl++;
                }
                break;
            case 'R':
                if (gold >= 100 * dashLvl)
                {
                    gold -= 100 * dashLvl;
                    dashLvl++;
                }
                break;
            case 'T':
                if (gold >= 100 * AOELvl)
                {
                    gold -= 100 * AOELvl;
                    AOELvl++;
                }
                break;

        }
    }

    private void checkGround()
    {
        RaycastHit hitInfo;
        float distance = 0.3f;
        can_jump = Physics.Raycast(transform.position + (UnityEngine.Vector3.up * 0.1f), UnityEngine.Vector3.down, out hitInfo, distance);
    }
}