using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    private Rigidbody rb;
    private float gameTicks;
    public float horizSpeed;
    public float jumpSpeed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        gameTicks = 0.0F;
	}
	
	// Update is called once per frame
	void Update () {
        gameTicks += Time.deltaTime;

        if (Input.GetButton(KeyCode.D.ToString()) == true)
        {
            Debug.Log("D key pressed.");
            transform.Translate(Vector2.right * horizSpeed * Time.deltaTime);
        }
        else if (Input.GetButton(KeyCode.A.ToString()) == true)
        {
            Debug.Log("A key pressed.");
            transform.Translate(Vector2.left * horizSpeed * Time.deltaTime);
        }
        else if (Input.GetButton(KeyCode.W.ToString()) == true)
        {
            Debug.Log("W key pressed.");
            transform.Translate(Vector2.up * jumpSpeed * Time.deltaTime);
        }
	}
}
