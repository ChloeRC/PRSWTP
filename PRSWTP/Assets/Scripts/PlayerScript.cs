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

    public float boxCollisionSize;

    private static readonly string RIGHT = "right";
    private static readonly string LEFT = "left";
    private static readonly string JUMP = "jump";

    private bool isGrounded = false;

    private int charges;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        gameTicks = 0.0F;
        rb.freezeRotation = true;
        charges = 0;
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

        RaycastHit hitInfo;
        isGrounded = Physics.SphereCast(rb.position, 0.1f, Vector3.down, out hitInfo, GetComponent<Collider>().bounds.size.y/2, groundLayers);

        if (Input.GetButtonDown(JUMP) == true && isGrounded)
        {
            //Debug.Log("W key pressed.");
            rb.velocity = Vector2.up * jumpSpeed * Time.deltaTime;
        }

        rb.AddForce(Vector2.down * gravity * rb.mass);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Charge")
        {
            Destroy(col.gameObject);
            charges++;
        }
    }
}
