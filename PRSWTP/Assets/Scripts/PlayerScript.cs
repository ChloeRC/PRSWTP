using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class PlayerScript : MonoBehaviour {
    //640 x 480 aspect ratio to start the game!!!
 
    private Rigidbody rb;
    private float gameTicks; //This is for something else
    private float gameTicks2; //This is for enemy collisions
    private float flibbityfish; //This is for invincibility
    private float thingy; //This if for info

    public float horizSpeed;
    public float jumpSpeed;
    public float gravity;
	private Vector3 freezePos;

    public LayerMask groundLayers;
    public GameObject bullet;
    public GameObject bucket;
    public GameObject player;
    public GameObject chargeBarDisplay;
    public GameObject gunBarDisplay;
    public GameObject Clone;

    public GameObject gameLight;

    public GameObject[] checkpoints;
    public Vector3 currentCheckPoint;

    private Vector3 direction;
    public static readonly Vector3 DIR_RIGHT = new Vector3(0, 90, 0);
    public static readonly Vector3 DIR_LEFT = new Vector3(0, 270, 0);

    public int health;

    private IEnumerator coroutine;

    //If hasShot is shotCooldown, you can shoot. Otherwise, you can't.
    public float hasShot;
    public int shotCooldown;

    //If hasSword is 0, you can toggle the sword. Otherwise, you can't.
    private float hasSword;
    public float swordCooldown;

    private static readonly string RIGHT = "right";
    private static readonly string LEFT = "left";
    private static readonly string JUMP = "jump";
    private static readonly string SUICIDE = "suicide";
    private static readonly string SHOOT = "shoot";
    private static readonly string SWORD = "sword";
    private static readonly string FILL_CHARGES = "fill charges";
    private static readonly string INFO = "info";
	private static readonly string INVINCIBLE = "invincible";
    private static readonly string PAUSE = "pause";
    private static readonly string MUSIC = "music";

    private bool isGrounded = false;
	private bool inv = false;
    public bool pause = false;
  //  private Vector3 spawnLocation;
    private Timer currentTime;
    private static Texture2D pauseRectTexture;
    private static GUIStyle pauseRectStyle;

    private int charges;
    public GameObject healthDisplayer;
    //public GameObject gunReloadDisplayer;
    //public GameObject time;
    public GameObject nonPlayerObjects;
    private GameObject info; //stores information across time travel

    //These two store when you shoot/stab something so it can be replicated by the past self
    public bool didSword;
    public bool didShoot;

    private AudioSource[] soundEffects;

    // Use this for initialization
    void OnLevelWasLoaded()
    {
        Start();
    }

    void Start ()
    {
        soundEffects = GetComponents<AudioSource>();
        //var gunShot = soundEffects[0];
        //var chargeSound = soundEffects[1];
        //3 checkpoint = soundEffects[2];
        //enemy death = soundEffects[3]
        //SPAWNS YOU AT THE CORRECT CHECKPOINT
        //The checkpoints are indexed from 1, the locations are indexed from 0
        Vector3 newPos = checkpoints[ValueHolder.checkpointNumber].transform.position;
        this.transform.position = checkpoints[ValueHolder.checkpointNumber].transform.position;

        //INITIALIZES REFERENCES
        //PlayerInfo info = nonPlayerObjects.GetComponent<PlayerInfo>();
        //health = info.getHealth();
        health = 3;
        rb = GetComponent<Rigidbody>();
        
        //INITIALIZES ALL THE GAMETICKS TIMERS
        gameTicks = 0.0f;
        gameTicks2 = 0.0f;
        thingy = 0.0f;
        flibbityfish = 0.0f;
        rb.freezeRotation = true;
        direction = DIR_RIGHT;
        charges = 0;

        //INITIALIZES THE COOLDOWN TIMERS
        hasShot = shotCooldown;
        hasSword = 0;

        //INITIALIZES THE SWORD/GUN MONITORS
        didSword = false;
        didShoot = false;
        
        gunBarDisplay.GetComponent<GunBarDisplay>().UpdateText();
    }
	
	// Update is called once per frame
    void Update () {
		//PlayerInfo info = nonPlayerObjects.GetComponent<PlayerInfo>();
		flibbityfish += Time.deltaTime;	//please keep me as flibbityfish

        //freezes the game
        if (Input.GetButton(PAUSE) == true && flibbityfish > 0.3f)
        {
            flibbityfish = 0.0f;    //i am repurposing flibbityfish because i don't want to use another variable
            pause = !pause;
			freezePos = transform.position;
			//info.setPause(pause);
        }

		if (pause) {
			transform.position = freezePos;
		}
        if (!pause)
        {
			//UPDATES ALL THE GAMETICKS TIMERS
			gameTicks += Time.deltaTime;
			gameTicks2 += Time.deltaTime;
			thingy += Time.deltaTime;


            //RIGHT (d)
            if (Input.GetButton(RIGHT) == true)

            {
                //Move you right at the correct speed, accounting for different lengths of frames
                transform.Translate(Vector2.right * horizSpeed * Time.deltaTime);
                player.GetComponent<PlayerRotate>().Rotate(DIR_RIGHT);
                direction = DIR_RIGHT;
            }

            //LEFT (a)
            if (Input.GetButton(LEFT) == true)
            {
                transform.Translate(Vector2.left * horizSpeed * Time.deltaTime);
                player.GetComponent<PlayerRotate>().Rotate(DIR_LEFT);
                direction = DIR_LEFT;
            }

            //SUICIDE (x)
            if (Input.GetButton(SUICIDE) == true)
            {
                kill();
            }

            //UPDATE COOLDOWN TIMERS
            //0 indicates that the sword is usable. So while the cooldown isn't reloaded, increment it upwards.
            SwordScript sword = GetComponentInChildren<SwordScript>();
            if (hasSword != 0)
            {
                hasSword += Time.deltaTime;
                if (hasSword > swordCooldown)
                {
                    hasSword = 0;
                    sword.drawn = false;
                }
            }
            //ShotCoolDown indicates that the gun is usable. While the cooldown isn't reloaded, increment.
            if (hasShot != shotCooldown)
            {
                hasShot += Time.deltaTime;
                if (hasShot > shotCooldown)
                {
                    hasShot = shotCooldown;
                }
                //Update the gun display.
                gunBarDisplay.GetComponent<GunBarDisplay>().UpdateText();
            }

            //SWORD (Space)
            if (Input.GetButton(SWORD) == true && hasSword == 0)
            {
                //The sword increments upwards from 1 to swordCooldown, then gets set to 0. 0 is usable.
                hasSword = 1;
                sword.drawn = true;

                //Marks the monitor as used
                didSword = true;
            }

            //REDRAW THE SWORD TO ACCOUNT FOR RECENT CHANGES
            bool drawn = sword.drawn;
            if (drawn)
            {
                sword.swordUp(direction);
            }
            else
            {
                sword.swordDown(direction);
            }

            //SHOOT (L shift)
            if (Input.GetButton(SHOOT) == true && hasShot == shotCooldown)
            {

                //The gun increments from .1 to shotCooldown, then stops. ShotCooldown is usable.
                hasShot = .1f;

                //This value will be added to the position on the Y axis so the bullet starts to the side of the player
                float toAdd = 0;

                //The amount that the bullet will be rotated on the Z axis (so it's facing the correct direction)
                float rotation = 0;

                //The direction the bullet will go in
                Vector3 force;

                if (direction == DIR_LEFT)
                {
                    toAdd = -.5f;
                    rotation = 90;
                    force = Vector3.left;
                }
                else //direction is right
                {
                    toAdd = .5f;
                    rotation = -90;
                    force = Vector3.right;
                }
                //Make the bullet face the correct direction, appear on the correct side of the player, and move in the correct direction.
                Vector3 pos = new Vector3(transform.position.x + toAdd, transform.position.y, transform.position.z);
                var newBullet = Instantiate(bullet, pos, Quaternion.Euler(0, 0, rotation));
                var rbBullet = newBullet.GetComponent<Rigidbody>();
                rbBullet.velocity = newBullet.GetComponent<BulletScript>().speed * force;

                //Marks the monitor as used
                didShoot = true;
            }

            //Tell if there is anything in a sphere shape below the player
            RaycastHit hitInfo;
            isGrounded = Physics.SphereCast(rb.position, 0.75f, Vector3.down, out hitInfo, GetComponent<Collider>().bounds.size.y / 2, groundLayers);

            //If there's something beneath you that you can jump from and you push the JUMP key (w), you jump
            if (Input.GetButtonDown(JUMP) == true && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }

            //FILL_CHARGES (t)
            if (Input.GetButton(FILL_CHARGES) == true && charges > 0)
            {
                charges = GetComponent<TrackMovement>().currLevelCharges();
            }

            //Apply gravity relative to the player's mass
            rb.AddForce(Vector2.down * gravity * rb.mass);

            //GET INFORMATION (i) - contains lots of debugs
            if (Input.GetButton(INFO) == true && thingy > 0.2f)
            {
		        Debug.Log ("isGrounded: " + isGrounded);
		    	Debug.Log ("inv: " + inv);
                thingy = 0.0f;
            }

            //If you're invincible, toggle invincibility. (KEY: C)
            //FlibbityFish is there so that the invincibility doesn't toggle super fast when you press and hold a key.
            if (Input.GetButton(INVINCIBLE) == true && flibbityfish > 0.3f)
            {
                inv = !inv;
                gameObject.GetComponentInParent<PlayerScript>().health = 3;
                flibbityfish = 0.0f;
            }

            //If you've fallen below -25 or your health is 0, you die
            if (health <= 0 || GetComponent<Transform>().position.y <= -25f)
            {
                Debug.Log("Health: " + health);
                Debug.Log("Position" + GetComponent<Transform>().position.y);
                Debug.Log("bop bop bop to the top");
                instaKill();
            }
        }
 
		//press the m button
		if (Input.GetButton (MUSIC) == true && flibbityfish > 0.3f) 
		{
			ValueHolder.music = !ValueHolder.music;
            flibbityfish = 0.0f;
		}
    }

    void OnCollisionEnter(Collision col)
    {
        SwordScript sword = GetComponentInChildren<SwordScript>();
        //ENEMY HURTS PLAYER
        //Under 0.5 for gameTicks2 = player is temporarily invincible
        //This way the enemy doesn't instantly remove all the player's health.
        if (col.gameObject.tag == "Enemy" && gameTicks2 > 0.5 && !sword.drawn && !inv)
        {
            gameObject.GetComponentInParent<PlayerScript>().health--;
            healthDisplayer.GetComponent<HealthBarDisplay>().UpdateText();
            gameTicks2 = 0.0f;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //COLLECT CHARGES
        if (col.gameObject.tag == "Charge")
        {
            ChargeScript chargeScript = col.GetComponent<ChargeScript>();
            //Makes sure that the charge isn't collected twice on accident
            if (chargeScript.isCollected == false)
            {
                chargeScript.isCollected = true;
                if (ValueHolder.music)
                    soundEffects[0].Play();
              
                Destroy(col.gameObject);
                charges++;
                //Update the charge bar to reflect the new charge.
                chargeBarDisplay.GetComponent<ChargeBarDisplay>().UpdateText();
            }
        }

        //KNOCK THE BUCKET OFF
        if (col.gameObject.tag == "Bucket Trigger")
        {
            bucket.GetComponent<BucketScript>().activate();
        }

        //TELEPORT VIA THE PORTALS
        if (col.gameObject.tag == "Portal")
        {
            PortalScript portal = col.gameObject.GetComponent<PortalScript>();
            gameObject.GetComponent<Transform>().position = new Vector3(portal.targetX, portal.targetY, portal.targetZ);
        }

        //ACTIVATE A CHECKPOINT
        if (col.gameObject.tag == "Checkpoint")
        {
            soundEffects[1].Play();

            Checkpoint ch = col.gameObject.GetComponent<Checkpoint>();

            if (ch.number > ValueHolder.checkpointNumber)
            {
                ValueHolder.checkpointNumber = ch.number;
                Debug.Log("CHECKPOINT " + ValueHolder.checkpointNumber);
            }

            for (int i = 0; i <= ch.number; i++) 
            //for every checkpoint less than or equal to the checkpoint just passed through, light it up
            {
                GameObject check = checkpoints[i];
                check.GetComponent<Checkpoint>().lightOn();
            }

            //Update the charge bar to reflect the new level - a different number of charges is required to time travel
            chargeBarDisplay.GetComponent<ChargeBarDisplay>().UpdateText();

        }

        //COLLECT RUM
        if (col.gameObject.tag == "Rum")
        {
            Destroy(col.gameObject);
			if (health < 3) {
				gameObject.GetComponentInParent<PlayerScript>().health++;
				healthDisplayer.GetComponent<HealthBarDisplay>().UpdateText();
			}
        }
    }
	
    //SET HEALTH TO NEW VALUE
    public void setHealth(int thisHealth)
    {
        health = thisHealth;
    }

    //KILL THE PLAYER WITHOUT THE DRAMATIC LIGHT DIMMING
    public void instaKill()
    {
        TimeTravelIndicator TimeTravelIndicator = GetComponent<TimeTravelIndicator>();
        TimeTravelIndicator.setFlash("black");
        soundEffects[2].Play();
        Application.LoadLevel("DeathScene");
    }

    //KILL THE PLAYER WITH DRAMA
    //Literally the most satisfying function in this entire project.
    public void kill()
    {
		TimeTravelIndicator TimeTravelIndicator = GetComponent<TimeTravelIndicator>();
		TimeTravelIndicator.setFlash("black");

        soundEffects[2].Play();
        Debug.Log("deathhhhh");
        //ValueHolder.currentTime = time;

        //Dim the lights dramatically
        coroutine = LightChange();
        StartCoroutine(coroutine);
    }

    //DIMS THE LIGHTS DRAMATICALLY
    private IEnumerator LightChange()
    {
        int numberOfIterations = 10;
        //Ten times...
        for (int i = 0; i < numberOfIterations; i++) {
            yield return new WaitForSeconds(.25f); //wait a quarter a second
            //Then, for every light...
            foreach (Transform child in gameLight.transform)
            {
                Light childL = child.GetComponent<Light>();
                if (childL == null)
                {
                    //...and every sublight...
                    foreach (Transform childOfChild in child.transform)
                    {
                        Light childOfChildL = childOfChild.GetComponent<Light>();
                        if (childOfChildL != null && childOfChildL.intensity > 0f)
                        {
                            //...drop the intensity by a quarter.
                            childOfChildL.intensity -= .25f;
                        }
                    }
                }
                else if (childL.intensity > 0f)
                {
                    childL.intensity -= .25f;
                }
            }
        }
        //And when all the lights are gone, switch the scene.
        Application.LoadLevel("DeathScene");
    }

    public int getCharges()
    {
        return charges;
    }

    public int getHealth()
    {
        return health;
    }

    public void resetCharges()
    {
        charges = 0;

        chargeBarDisplay.GetComponent<ChargeBarDisplay>().UpdateText();
    }

    //records the time since you last hit an enemy
    public float getCollisionTime()
    {
        return gameTicks2;
    }
    
    //resets the time since you were last hit by an enemy
    public void resetCollisionTime()
    {
        gameTicks2 = 0.0f;
    }

	public bool getInv() {
		return inv;
	}

    public bool getPause()
    {
        return pause;
    }
}
