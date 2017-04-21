using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private Rigidbody rb;
    private float gameTicks; //This is for something else
    private float gameTicks2; //This is for enemy collisions
    private float flibbityfish; //This is for invincibility
    private float thingy; //This if for info

    public float horizSpeed;
    public float jumpSpeed;
    public float gravity;
    public LayerMask groundLayers;
    public GameObject bullet;
    public GameObject bucket;
    public GameObject player;
    public GameObject chargeBarDisplay;
    public Collider Checkpoint;
    public GameObject Timer;
    public GameObject Clone;

    public GameObject gameLight;

    public Vector3[] checkpointLocations;
    public Vector3 currentCheckPoint;

    //False is right, true is left
    private bool direction;
    public static readonly bool DIR_RIGHT = false;
    public static readonly bool DIR_LEFT = true;

    public int health;

    private IEnumerator coroutine;

    //If hasShot is 0, you can shoot. Otherwise, you can't.
    private float hasShot;
    public float shotCooldown;

    //If hasSword is 0, you can toggle the sword. Otherwise, you can't.
    private float hasSword;
    public float swordCooldown;

    private bool controllable;

    private static readonly string RIGHT = "right";
    private static readonly string LEFT = "left";
    private static readonly string JUMP = "jump";
    private static readonly string SUICIDE = "suicide";
    private static readonly string SHOOT = "shoot";
    private static readonly string SWORD = "sword";
    private static readonly string FILL_CHARGES = "fill charges";
    private static readonly string INFO = "info";
	private static readonly string INVINCIBLE = "invincible";

    private bool isGrounded = false;
	private bool inv = false;
  //  private Vector3 spawnLocation;
    private Timer currentTime;

    private int charges;
    public GameObject healthDisplayer;
    public GameObject gunReloadDisplayer;
    public GameObject time;
    public GameObject nonPlayerObjects;
    public GameObject info; //stores information across time travel
    private Vector3 rSpawnLocation;

    // Use this for initialization
    void Start ()
    {
        if (ValueHolder.isPastSelfSpawning == false)
        {
            Debug.Log("CHECKPOINT " + ValueHolder.checkpointNumber);
            //The checkpoints are indexed from 1, the locations are indexed from 0
            Vector3 newPos = checkpointLocations[ValueHolder.checkpointNumber];
            Debug.Log("Pos: " + newPos.x + ", " + newPos.y + ", " + newPos.z);
            this.transform.position = checkpointLocations[ValueHolder.checkpointNumber];
        }
        else
        {
            ValueHolder.isPastSelfSpawning = false;
        }

        //health = 3;
        PlayerInfo info = nonPlayerObjects.GetComponent<PlayerInfo>();
		health = info.getHealth();
        rb = GetComponent<Rigidbody>();
        
        gameTicks = 0.0f;
        gameTicks2 = 0.0f;
        thingy = 0.0f;
        flibbityfish = 0.0f;
        rb.freezeRotation = true;
        direction = DIR_RIGHT;
        charges = 0;
        
        controllable = true;

        hasShot = 0;
        hasSword = 0;
	}
	
	// Update is called once per frame
    void Update () {
        //Debug.Log(ValueHolder.isPastSelfSpawning);

        gameTicks += Time.deltaTime;
        gameTicks2 += Time.deltaTime;
        thingy += Time.deltaTime;
        flibbityfish += Time.deltaTime;

        if (controllable)
        {
            //If you push the button which is mapped to RIGHT (d), you go right
            if (Input.GetButton(RIGHT) == true)
            {
                transform.Translate(Vector2.right * horizSpeed * Time.deltaTime);
                player.GetComponent<PlayerRotate>().Rotate(DIR_RIGHT);
                direction = DIR_RIGHT;
            }
            //If you push the button which is mapped to LEFT (a), you go left
            if (Input.GetButton(LEFT) == true)
            {
                //Debug.Log("A key pressed.");
                transform.Translate(Vector2.left * horizSpeed * Time.deltaTime);
                player.GetComponent<PlayerRotate>().Rotate(DIR_LEFT);
                direction = DIR_LEFT;
            }

            //SUICIDE (x)
            if (Input.GetButton(SUICIDE) == true)
            {
                kill();
            }

            //Cooldowns
            if (hasSword != 0)
            {
                hasSword += Time.deltaTime;
                if (hasSword > swordCooldown)
                {
                    hasSword = 0;
                }
            }
            if (hasShot != 0)
            {
                hasShot += Time.deltaTime;
                if (hasShot > shotCooldown)
                {
                    hasShot = 0;
                }
            }

			if (direction == DIR_LEFT) {
				SwordScript sword = GetComponentInChildren<SwordScript> ();
				bool drawn = sword.drawn;
				if (drawn) {
					sword.swordUp (direction);
				} else {
					sword.swordDown (direction);
				}
			} else {
				SwordScript sword = GetComponentInChildren<SwordScript> ();
				bool drawn = sword.drawn;
				if (drawn) {                  
					sword.swordUp (direction);
				} else {
                    sword.swordDown (direction);
				}
			}

            //SWORD (Space)
            if (Input.GetButton(SWORD) == true && hasSword == 0)
            {
                hasSword = 1;

                SwordScript sword = GetComponentInChildren<SwordScript>();
                sword.toggleSword(direction);
            }

            //SHOOT (L shift)
            if (Input.GetButton(SHOOT) == true && hasShot == 0)
            {
                hasShot = 1;

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
                Vector3 pos = new Vector3(transform.position.x + toAdd, transform.position.y, transform.position.z);
                var newBullet = Instantiate(bullet, pos, Quaternion.Euler(0, 0, rotation));
                var rbBullet = newBullet.GetComponent<Rigidbody>();
                rbBullet.velocity = newBullet.GetComponent<BulletScript>().speed * force;
            }
				

            //Tell if there is anything in a sphere shape below the player
            RaycastHit hitInfo;
            isGrounded = Physics.SphereCast(rb.position, 0.75f, Vector3.down, out hitInfo, GetComponent<Collider>().bounds.size.y / 2, groundLayers);
            //ORIGINAL: isGrounded = Physics.SphereCast(rb.position, 0.2f, Vector3.down, out hitInfo, GetComponent<Collider>().bounds.size.y / 2, groundLayers);
            //Debug.Log("isGrounded: " + isGrounded)

            //If there's something beneath you that you can jump from and you push the JUMP key (w), you jump
            if (Input.GetButtonDown(JUMP) == true && isGrounded)
            {
                //Debug.Log("W key pressed.");
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

			if (Input.GetButton (INVINCIBLE) == true && flibbityfish > 0.3f) {
				inv = !inv;
				gameObject.GetComponentInParent<PlayerScript>().health = 3;
                flibbityfish = 0.0f;
			}
        }
        
        //If you've fallen below -25 or your health is 0, you die
        //When the player resets, there's this weird thing where position is -21 for a bit????
        if (health <= 0 || GetComponent<Transform>().position.y <= -25f)
        {
            Debug.Log("bop bop bop to the top");
            kill();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        SwordScript sword = GetComponentInChildren<SwordScript>();
        //Under 0.5 for gameTicks2 = player is temporarily invincible
        if (col.gameObject.tag == "Enemy" && gameTicks2 > 0.5 && controllable && !sword.drawn && !inv)
        {
            //Debug.Log("The only man I love is my daaaaaaaaaaad");
            gameObject.GetComponentInParent<PlayerScript>().health--;
            healthDisplayer.GetComponent<HealthBarDisplay>().UpdateText();
            gameTicks2 = 0.0f;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Charge" && controllable)
        {
            ChargeScript chargeScript = col.GetComponent<ChargeScript>();
            if (chargeScript.isCollected == false)
            {
                chargeScript.isCollected = true;
                Destroy(col.gameObject);
                charges++;
                chargeBarDisplay.GetComponent<ChargeBarDisplay>().UpdateText();
            }
        }

        if (col.gameObject.tag == "Bucket Trigger")
        {
            bucket.GetComponent<BucketScript>().activate();
        }

        if (col.gameObject.tag == "Portal")
        {
            PortalScript portal = col.gameObject.GetComponent<PortalScript>();
            gameObject.GetComponent<Transform>().position = new Vector3(portal.targetX, portal.targetY, portal.targetZ);
        }

        if (col.gameObject.tag == "Checkpoint")
        {
            Checkpoint ch = col.gameObject.GetComponent<Checkpoint>();

            if (ch.activated() == false)
            {
                ValueHolder.checkpointNumber++;
                Debug.Log("CHECKPOINT " + ValueHolder.checkpointNumber);
            }
            ch.activate();

            chargeBarDisplay.GetComponent<ChargeBarDisplay>().UpdateText();

        }

        if (col.gameObject.tag == "Rum" && controllable)
        {
            Destroy(col.gameObject);
			if (health < 3) {
				gameObject.GetComponentInParent<PlayerScript>().health++;
				healthDisplayer.GetComponent<HealthBarDisplay>().UpdateText();
			}
        }
    }
		
    public void setHealth(int thisHealth)
    {
        health = thisHealth;
        //Debug.Log("bam bam");
    }

    //Literally the most satisfying function in this entire project.
    public void kill()
    {
        //ValueHolder.currentTime = time;
        coroutine = LightChange();
        StartCoroutine(coroutine);
        controllable = false;
    }

    private IEnumerator LightChange()
    {
        int numberOfIterations = 10;
        //Five times...
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
                            //...drop the intensity.
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


    public void resetCollisionTime()
    {
        gameTicks2 = 0.0f;
    }

    public void toggleControllable()
    {
        controllable = !controllable;
    }

    public bool getControllable()
    {
        return controllable;
    }

	public bool getInv() {
		return inv;
	}

    public Vector3 returnSpawnLocation()
    {
        return rSpawnLocation;
    }
    //moving the past self
  

    public void setPosition(Vector3 vector)
    {
        var marker = TrackMovement.MARKER;
		//this is always true?
        if (marker != null && Mathf.Abs((vector - marker).magnitude) < float.Epsilon)
            //&& !controllable && gameObject.tag == "Parent for Player(Clone)")
        {
            Debug.Log("oopsies");
            if (gameObject.tag == "Parent for Player(Clone)") {
                Debug.Log("WORRRRRRDDSSSS fail");
                //kill();
            //Destroy(GameObject.Find("Parent for Player (Clone)"));
            }
        }
        transform.position = vector;
    }
    public void loadGame()
    {

    }
}
