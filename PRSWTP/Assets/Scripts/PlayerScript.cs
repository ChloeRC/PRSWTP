using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private Rigidbody rb;
    private float gameTicks;
    public float horizSpeed;
    public float jumpSpeed;
    public float gravity;

    private static readonly string RIGHT = "right";
    private static readonly string LEFT = "left";
    private static readonly string JUMP = "jump";

    private bool isJumping;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        gameTicks = 0.0F;
        rb.freezeRotation = true;
        isJumping = false;
	}
	
	// Update is called once per frame
	void Update () {
        gameTicks += Time.deltaTime;

        if (Input.GetButton(RIGHT) == true)
        {
            //Debug.Log("D key pressed.");
            transform.Translate(Vector2.right * horizSpeed * Time.deltaTime);
        }
        if (Input.GetButton(LEFT) == true)
        {
            //Debug.Log("A key pressed.");
            transform.Translate(Vector2.left * horizSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown(JUMP) == true && !isJumping)
        {
            //Debug.Log("W key pressed.");
            isJumping = true;
            rb.velocity = Vector2.up * jumpSpeed * Time.deltaTime;
        }
        if (Input.GetButtonUp(JUMP) == true)
        {
            isJumping = false;
        }

        rb.AddForce(Vector2.down * gravity * rb.mass);

        if (isJumping)
        {

        }
        
	}
}
