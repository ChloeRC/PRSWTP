using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private Rigidbody rb;
    private float gameTicks;
    public float speed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        gameTicks = 0.0F;
	}
	
	// Update is called once per frame
	void Update () {
        gameTicks += Time.deltaTime;

        if (Input.GetButton("D"))
        {
            rb.velocity = new Vector2(speed, 0);
        }
	}
}
