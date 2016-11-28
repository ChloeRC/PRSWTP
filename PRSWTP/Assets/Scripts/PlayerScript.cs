using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private Rigidbody rb;
    private float gameTicks;
    public float horizSpeed;
    public float jumpSpeed;
    public float gravity;

    public float boxCollisionSize;

    private static readonly string RIGHT = "right";
    private static readonly string LEFT = "left";
    private static readonly string JUMP = "jump";

    private bool isGrounded = false;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        gameTicks = 0.0F;
        rb.freezeRotation = true;
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

        isGrounded = Physics.BoxCast(rb.position, new Vector3(boxCollisionSize, boxCollisionSize, boxCollisionSize), Vector3.down);

        if (Input.GetButtonDown(JUMP) == true && isGrounded)
        {
            //Debug.Log("W key pressed.");
            rb.velocity = Vector2.up * jumpSpeed * Time.deltaTime;
        }

        rb.AddForce(Vector2.down * gravity * rb.mass);
	}
}
