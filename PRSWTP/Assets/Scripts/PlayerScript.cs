using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private Rigidbody rb;
    private float gameTicks; //This is for something else
    private float gameTicks2; //This is for enemy collisions
    public float horizSpeed;
    public float jumpSpeed;
    public float gravity;
    public LayerMask groundLayers;
    public GameObject bullet;
    public GameObject bucket;
    public GameObject player;
    public GameObject chargeBarDisplay;
    public Collider Checkpoint;

    public Vector3[] checkpointLocations;

    //False is right, true is left
    private bool direction;
    public static readonly bool DIR_RIGHT = false;
    public static readonly bool DIR_LEFT = true;

    public int health;

    public int timer;
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

    private bool isGrounded = false;

    private int charges;
    public GameObject healthDisplayer;
    public GameObject gunReloadDisplayer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

        gameTicks = 0.0F;
        gameTicks2 = 0.0f;
        rb.freezeRotation = true;

        direction = DIR_RIGHT;
        charges = 0;
        health = 3;
        timer = 200;
        controllable = true;

        hasShot = 0;
        hasSword = 0;

        Debug.Log("CHECKPOINT " + ValueHolder.checkpointNumber);
        //The checkpoints are indexed from 1, the locations are indexed from 0
        Vector3 newPos = checkpointLocations[ValueHolder.checkpointNumber - 1];
        Debug.Log(newPos.x + ", " + newPos.y + ", " + newPos.z);
        this.transform.position = checkpointLocations[ValueHolder.checkpointNumber - 1];
	}
	
	// Update is called once per frame
    void Update () {
        gameTicks += Time.deltaTime;
        gameTicks2 += Time.deltaTime;

        if (controllable)
        {
            //If you push the button which is mapped to RIGHT (d), you go right
            if (Input.GetButton(RIGHT) == true)
            {
                //Debug.Log("D key pressed.");
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
        }
        
        //If you've fallen below -20 or your health is 0, you die
        if (health <= 0 || GetComponent<Transform>().position.y <= -20)
        {
            kill();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //Under 0.5 for gameTicks2 = player is temporarily invincible
        if (col.gameObject.tag == "Enemy" && gameTicks2 > 0.5 && controllable)
        {
            gameObject.GetComponentInParent<PlayerScript>().health--;
            healthDisplayer.GetComponent<HealthBarDisplay>().UpdateText();
            gameTicks2 = 0.0f;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Charge" && controllable)
        {
            Destroy(col.gameObject);
            charges++;
            chargeBarDisplay.GetComponent<ChargeBarDisplay>().UpdateText();
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
            Checkpoint checkpoint = col.gameObject.GetComponent<Checkpoint>();
            gameObject.GetComponent<Transform>().position = new Vector3();
        }

        if (col.gameObject.tag == "Rum" && controllable)
        {
            Destroy(col.gameObject);
			if (health < 3) {
				gameObject.GetComponentInParent<PlayerScript> ().health++;
				healthDisplayer.GetComponent<HealthBarDisplay> ().UpdateText ();
			}
        }
    }

    //Literally the most satisfying function in this entire project.
    public void kill()
    {
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

    public void setPosition(Vector3 vector)
    {
        var marker = TrackMovement.MARKER;
		//this is always true?
        if (marker != null && Mathf.Abs((vector - marker).magnitude) < float.Epsilon)
        {
            this.kill();
        }
        transform.position = vector;
    }
    public void loadGame()
    {

    }
}
