using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float speed;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //Rotation = 90 (ish)
        float z = this.transform.rotation.z;
        if (91 > z && z > 89)
        {
            rb.AddForce(Vector3.left * speed);
        }
        //Rotation = -90 (ish)
        else if (-91 < z && z < -89)
        {
            rb.AddForce(Vector3.right * speed);
        }
    }
}
