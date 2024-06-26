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
    public GameObject boss;
    private GameObject box;
    public GameObject boxPreset;

    public Material czerwony;
    public Material czarny;
    private GameObject musicBox;
    public GameObject musicBoxPreset;

    public GameObject soundEffectAssetAggressiveMiau;
    public GameObject soundEffectAssetAttackMiau;
    public GameObject soundEffectAssetBoulder;
    public GameObject soundEffectAssetDash;
    public GameObject soundEffectAssetElectricity;
    public GameObject soundEffectAssetFuse;
    public GameObject soundEffectAssetFireball;
    public GameObject soundEffectAssetUpgrade;
    public GameObject soundEffectAssetHeal;
    public GameObject soundEffectAssetHighMiau;
    public GameObject soundEffectAssetMiau;
    public GameObject soundEffectAssetSadMiau;
    public GameObject soundEffectAssetSword;

    private GameObject soundEffectAggressiveMiau;
    private GameObject soundEffectAttackMiau;
    private GameObject soundEffectBoulder;
    private GameObject soundEffectDash;
    private GameObject soundEffectElectricity;
    private GameObject soundEffectFuse;
    private GameObject soundEffectFireball;
    private GameObject soundEffectUpgrade;
    private GameObject soundEffectHeal;
    private GameObject soundEffectHighMiau;
    private GameObject soundEffectMiau;
    private GameObject soundEffectSadMiau;
    private GameObject soundEffectSword;


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

    public float healCooldown = 1f;
    private float healTimer;

    private float timer = 0f;

    public float timing = 0.05f;

    private float bpm;
    private float nextSxtnhBeat = 0;
    private float nextBeat = 0;


    public float enemyCooldown = 50;
    private float nextEnemy = 0;
    private float enemySpawnScaling = 0.99f;
    private float minEnemyCooldown = 0.5f;
    private float enemyHealthScaling = 1.02f;
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

    public float hitCooldown = 0.5f;
    private float hitTimer = 0;

    private int shootLvl = 1;
    private int slashLvl = 1;
    private int spinLvl = 1;
    private int healLvl = 1;
    private int piercingLvl = 1;
    private int dashLvl = 1;
    private int AOELvl = 1;

    public int points = 0;

    private int bossCounter = 0;
    private int bossCooldown = 20;

    // Start is called before the first frame update
    void Start()
    {
        camera = Instantiate(cameraPreset);

        musicBox = Instantiate(musicBoxPreset);

        soundEffectSadMiau = Instantiate(soundEffectAssetSadMiau);

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

        //box = Instantiate(boxPreset);
        //box.transform.position = transform.position;


        //GameObject newEnemy1;
        //newEnemy1 = Instantiate(enemy1);
        //enemyBehaviour enemyScript1 = newEnemy1.GetComponent<enemyBehaviour>();
        //newEnemy1.transform.position = new UnityEngine.Vector3(0, -40, 00);

        //GameObject newEnemy2;
        //newEnemy2 = Instantiate(enemy2);
        //enemyBehaviour enemyScript2 = newEnemy2.GetComponent<enemy2Behaviour>();
        //newEnemy2.transform.position = new UnityEngine.Vector3(3, -40, 3);

        //GameObject newEnemy3;
        //newEnemy3 = Instantiate(enemy3);
        //enemyBehaviour enemyScript3 = newEnemy3.GetComponent<enemy3Behaviour>();
        //newEnemy3.transform.position = new UnityEngine.Vector3(-3, -40, -3);

        //GameObject newBoss;
        //newBoss = Instantiate(boss);
        //enemyBehaviour enemyScript4 = newBoss.GetComponent<bossBehaviour>();
        //newBoss.transform.position = new UnityEngine.Vector3(3, -40, -3);


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
                        soundEffectHighMiau = Instantiate(soundEffectAssetHighMiau);
                        soundEffectHighMiau.GetComponent<AudioSource>().Play(); // umiera
                        Destroy(soundEffectHighMiau, 5f);
                        Destroy(gameObject);
                    }
                    soundEffectAggressiveMiau = Instantiate(soundEffectAssetAggressiveMiau);
                    soundEffectAggressiveMiau.GetComponent<AudioSource>().Play(); // obrazenia
                    Destroy(soundEffectAggressiveMiau, 3f);
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
        healTimer += Time.deltaTime;
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
                //box.GetComponent<MeshRenderer>().material = czerwony;
            }
            else
            {
                //box.GetComponent<MeshRenderer>().material = czarny;
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
            if(bossCounter == bossCooldown)
            {
                spawnEnemy(4);
                bossCounter = 0;
            }
            bossCounter++;


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
            newEnemy = Instantiate(boss);
            bossBehaviour enemyScript = newEnemy.GetComponent<bossBehaviour>();
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
            soundEffectMiau = Instantiate(soundEffectAssetMiau);
            soundEffectMiau.GetComponent<AudioSource>().Play(); // skok
            Destroy(soundEffectMiau, 3f);
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
        // check czy trafienie dobrze siad�o
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
            points += 300;
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

            soundEffectFireball = Instantiate(soundEffectAssetFireball);
            soundEffectFireball.GetComponent<AudioSource>().Play(); // fireball
            Destroy(soundEffectFireball, 5f);
            soundEffectAttackMiau = Instantiate(soundEffectAssetAttackMiau);
            soundEffectAttackMiau.GetComponent<AudioSource>().Play(); // attack
            Destroy(soundEffectAttackMiau, 3f);

            // tu sprawdzenie do comba
            checkAttackInBeat('Y');

            //Debug.Log(specialCombo);

            UnityEngine.Vector3 vec = transform.forward;
            vec.y += 0.5f;
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = transform.position + vec * 2.5f;
            newBullet.transform.rotation = transform.rotation;
            iNeedMoreBullets script = newBullet.GetComponent<iNeedMoreBullets>();
            script.speed = 0.3f;
            script.dmg = 10 + 10 * shootLvl + combo;
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

            soundEffectSword = Instantiate(soundEffectAssetSword);
            soundEffectSword.GetComponent<AudioSource>().Play(); // sword
            Destroy(soundEffectSword, 3f);
            soundEffectAttackMiau = Instantiate(soundEffectAssetAttackMiau);
            soundEffectAttackMiau.GetComponent<AudioSource>().Play(); // attack
            Destroy(soundEffectAttackMiau, 3f);
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
            script.dmg = 8 * slashLvl + combo / 2;

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
            soundEffectElectricity = Instantiate(soundEffectAssetElectricity);
            soundEffectElectricity.GetComponent<AudioSource>().Play(); // electricity
            Destroy(soundEffectElectricity, 5f);
            soundEffectAttackMiau = Instantiate(soundEffectAssetAttackMiau);
            soundEffectAttackMiau.GetComponent<AudioSource>().Play(); // attack
            Destroy(soundEffectAttackMiau, 3f);
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
            script.dmg = 20 + 10 * spinLvl + combo;

        }
    }

    private void heal()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            // check do spamownia
            if (healTimer < healCooldown)
            {
                return;
            }
            healTimer = 0;

            soundEffectHeal = Instantiate(soundEffectAssetHeal);
            soundEffectHeal.GetComponent<AudioSource>().Play(); // heal
            Destroy(soundEffectHeal, 3f);

            // tu sprawdzenie do comba
            checkAttackInBeat('O');
            //Debug.Log(specialCombo);


            GameObject newHeal = Instantiate(healing);

            hp += 2 * healLvl + combo;
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

            soundEffectBoulder = Instantiate(soundEffectAssetBoulder);
            soundEffectBoulder.GetComponent<AudioSource>().Play(); // boulder
            Destroy(soundEffectBoulder, 5f);
            soundEffectAttackMiau = Instantiate(soundEffectAssetAttackMiau);
            soundEffectAttackMiau.GetComponent<AudioSource>().Play(); // attack
            Destroy(soundEffectAttackMiau, 3f);
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
            script.dmg = 8 * slashLvl + combo / 2;
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

            soundEffectDash = Instantiate(soundEffectAssetDash);
            soundEffectDash.GetComponent<AudioSource>().Play(); // dash
            Destroy(soundEffectDash, 3f);
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

            soundEffectFuse = Instantiate(soundEffectAssetFuse);
            soundEffectFuse.GetComponent<AudioSource>().Play(); // explosion
            Destroy(soundEffectFuse, 5f);
            soundEffectAttackMiau = Instantiate(soundEffectAssetAttackMiau);
            soundEffectAttackMiau.GetComponent<AudioSource>().Play(); // attack
            Destroy(soundEffectAttackMiau, 3f);
            // tu sprawdzenie do comba
            checkAttackInBeat('T');

            //Debug.Log(specialCombo);
            UnityEngine.Vector3 vec2 = transform.forward;

            UnityEngine.Vector3 vec = transform.up;
            GameObject newBomb = Instantiate(bomb);
            newBomb.transform.position = transform.position + vec * 4f + vec2 * 2f;
            newBomb.transform.rotation = transform.rotation;
            AOE script = newBomb.GetComponent<AOE>();
            script.dmg = 10 + 8 * slashLvl + combo;
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
                    soundEffectUpgrade = Instantiate(soundEffectAssetUpgrade);
                    soundEffectUpgrade.GetComponent<AudioSource>().Play(); // upgrade
                    Destroy(soundEffectUpgrade, 3f);

                }
                break;
            case 'U':
                if (gold >= 100 * slashLvl)
                {
                    gold -= 100 * slashLvl;
                    slashLvl++;
                    soundEffectUpgrade = Instantiate(soundEffectAssetUpgrade);
                    soundEffectUpgrade.GetComponent<AudioSource>().Play(); // upgrade
                    Destroy(soundEffectUpgrade, 3f);
                }
                break;
            case 'I':
                if (gold >= 100 * spinLvl)
                {
                    gold -= 100 * spinLvl;
                    spinLvl++;
                    soundEffectUpgrade = Instantiate(soundEffectAssetUpgrade);
                    soundEffectUpgrade.GetComponent<AudioSource>().Play(); // upgrade
                    Destroy(soundEffectUpgrade, 3f);
                }
                break;
            case 'O':
                if (gold >= 100 * healLvl)
                {
                    gold -= 100 * healLvl;
                    healLvl++;
                    soundEffectUpgrade = Instantiate(soundEffectAssetUpgrade);
                    soundEffectUpgrade.GetComponent<AudioSource>().Play(); // upgrade
                    Destroy(soundEffectUpgrade, 3f);
                }
                break;
            case 'P':
                if (gold >= 100 * piercingLvl)
                {
                    gold -= 100 * piercingLvl;
                    piercingLvl++;
                    soundEffectUpgrade = Instantiate(soundEffectAssetUpgrade);
                    soundEffectUpgrade.GetComponent<AudioSource>().Play(); // upgrade
                    Destroy(soundEffectUpgrade, 3f);
                }
                break;
            case 'R':
                if (gold >= 100 * dashLvl)
                {
                    gold -= 100 * dashLvl;
                    dashLvl++;
                    soundEffectUpgrade = Instantiate(soundEffectAssetUpgrade);
                    soundEffectUpgrade.GetComponent<AudioSource>().Play(); // upgrade
                    Destroy(soundEffectUpgrade, 3f);
                }
                break;
            case 'T':
                if (gold >= 100 * AOELvl)
                {
                    gold -= 100 * AOELvl;
                    AOELvl++;
                    soundEffectUpgrade = Instantiate(soundEffectAssetUpgrade);
                    soundEffectUpgrade.GetComponent<AudioSource>().Play(); // upgrade
                    Destroy(soundEffectUpgrade, 3f);
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