﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private Rigidbody rb;
    private float gameTicks;
    public float horizSpeed;
    public float jumpSpeed;
    public float gravity;
    public LayerMask groundLayers;
    public GameObject bullet;

    //False is right, true is left
    private bool direction;
    private static readonly bool DIR_RIGHT = false;
    private static readonly bool DIR_LEFT = true;

    private int health;

    //If hasShot is 0, you can shoot. Otherwise, you can't.
    private int hasShot;

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
        rb = GetComponent<Rigidbody>();
        gameTicks = 0.0F;
        rb.freezeRotation = true;

        direction = DIR_RIGHT;
        charges = 0;
        health = 3;
        controllable = true;
        hasShot = 0;
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
                direction = DIR_RIGHT;
            }
            //If you push the button which is mapped to LEFT (a), you go left
            if (Input.GetButton(LEFT) == true)
            {
                //Debug.Log("A key pressed.");
                transform.Translate(Vector2.left * horizSpeed * Time.deltaTime);
                direction = DIR_LEFT;
            }

            //SUICIDE (x)
            if (Input.GetButton(SUICIDE) == true)
            {
                kill();
            }

            if (hasShot != 0)
            {
                hasShot++;
                hasShot %= 50;
            }

            //SHOOT (L shift)
            if (Input.GetButton(SHOOT) == true && hasShot == 0)
            {
                hasShot = 1;

                //This value will be added to the position on the Y axis so the bullet starts to the side of the player
                float toAdd = 0;

                //The amount that the bullet will be rotated on the Z axis (so it's facing the correct direction)
                float rotation = 0;
                if (direction == DIR_LEFT)
                {
                    toAdd = -.5f;
                    rotation = 90;
                }
                else //direction is right
                {
                    toAdd = .5f;
                    rotation = -90;
                }
                Vector3 pos = new Vector3(transform.position.x + toAdd, transform.position.y, transform.position.z);
                Instantiate(bullet, pos, Quaternion.Euler(0, 0, rotation));
            }

            //Tell if there is anything in a sphere shape below the player
            RaycastHit hitInfo;
            isGrounded = Physics.SphereCast(rb.position, 0.1f, Vector3.down, out hitInfo, GetComponent<Collider>().bounds.size.y / 2, groundLayers);

            //If there's something beneath you that you can jump from and you push the JUMP key (w), you jump
            if (Input.GetButtonDown(JUMP) == true && isGrounded)
            {
                //Debug.Log("W key pressed.");
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }
        }

        if (controllable)
        {
            //Apply gravity relative to the player's mass
            rb.AddForce(Vector2.down * gravity * rb.mass);
        }

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

    public void setPosition(Vector3 vector)
    {
        var marker = TrackMovement.MARKER;
        if (marker != null && Mathf.Abs((vector - marker).magnitude) < float.Epsilon)
        {
            this.kill();
        }
        transform.position = vector;
    }
}
