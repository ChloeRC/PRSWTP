using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private Rigidbody rb;
    private float gameTicks;
    public float horizSpeed;
    public float jumpSpeed;
    public float gravity;
    public LayerMask groundLayers;
    public float groundDetect;

    public int health;

    public GameObject bullet;

    public float boxCollisionSize;


    private bool controllable;

    private static readonly string RIGHT = "right";
    private static readonly string LEFT = "left";
    private static readonly string JUMP = "jump";
    private static readonly string SUICIDE = "suicide";
    private static readonly string SHOOT = "shoot";

    private bool isGrounded = false;

    private int charges;

    // Use this for initialization
    void Start () {
        Debug.Log("Health: " + health);
        rb = GetComponent<Rigidbody>();
        gameTicks = 0.0F;
        rb.freezeRotation = true;
        charges = 0;
        controllable = true;
	}
	
	// Update is called once per frame
    void Update () {
        gameTicks += Time.deltaTime;

        if (controllable)
        {
            //If you push the button which is mapped to RIGHT (d), you go right
            if (Input.GetButton(RIGHT) == true)
            {
                //Debug.Log("D key pressed.");
                transform.Translate(Vector2.right * horizSpeed * Time.deltaTime);
            }
            //If you push the button which is mapped to LEFT (a), you go left
            if (Input.GetButton(LEFT) == true)
            {
                //Debug.Log("A key pressed.");
                transform.Translate(Vector2.left * horizSpeed * Time.deltaTime);
            }

            //SUICIDE (x)
            if (Input.GetButton(SUICIDE) == true)
            {
                kill();
            }

            //SHOOT (L shift)
            if (Input.GetButton(SHOOT) == true && gameTicks % 5 == 0)
            {
                Instantiate(bullet, transform.position, transform.rotation);
            }

            //Tell if there is anything in a sphere shape below the player
            RaycastHit hitInfo;
            isGrounded = Physics.SphereCast(rb.position, 0.1f, Vector3.down, out hitInfo, GetComponent<Collider>().bounds.size.y / 2, groundLayers);

            //If there's something beneath you that you can jump from and you push the JUMP key (w), you jump
            if (Input.GetButtonDown(JUMP) == true && isGrounded)
            {
                //Debug.Log("W key pressed.");
                rb.velocity = Vector2.up * jumpSpeed * Time.deltaTime;
            }
        }

        //Apply gravity relative to the player's mass
        rb.AddForce(Vector2.down * gravity * rb.mass);

        //If you've fallen below -20 or your health is 0, you die
        if (health <= 0 || GetComponent<Transform>().position.y <= -20)
        {
            kill();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Charge")
        {
            Destroy(col.gameObject);
            charges++;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            health--;
        }
    }

    //Literally the most satisfying function in this entire project.
    void kill()
    {
        Destroy(this.gameObject);
    }

    public int getCharges()
    {
        return charges;
    }

    public void resetCharges()
    {
        charges = 0;
    }

    public void toggleControllable()
    {
        controllable = !controllable;
    }
}
