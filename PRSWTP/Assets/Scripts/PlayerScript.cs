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
        speed = .5f;
	}
	
	// Update is called once per frame
	void Update () {
        gameTicks += Time.deltaTime;

        if (Input.GetButton(KeyCode.D.ToString()) == true)
        {
            Debug.Log("D key pressed.");
            //Vector2 velocity = new Vector3(speed, 0);
            transform.Translate(Vector2.right * Time.deltaTime);

        }
	}
}
