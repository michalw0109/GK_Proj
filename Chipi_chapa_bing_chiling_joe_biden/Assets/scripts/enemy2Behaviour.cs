
public class enemy2Behaviour : enemyBehaviour
{
    private void Awake()
    {
        dmg = 5;
        speed = 3;
        health = 60;
        gravityForce = 10;
        goldReward = 20;
        distance = 1.3f;
    }

    // Start is called before the first frame update
    void Start()
    {
        setup();

    }
    

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
