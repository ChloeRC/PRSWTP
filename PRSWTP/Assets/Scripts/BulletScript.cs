using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float speed;

    private Rigidbody rb;
    private double time;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        //despawn timer
        if (time > .4)
        {
            kill();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            kill();
        }
        if (col.gameObject.layer == 8) //ground layer
        {
            kill();
        }
        if (col.gameObject.tag == "SecretWall")
        {
            Destroy(col.gameObject);
            kill();
        }
    }

    void kill()
    {
        Destroy(this.gameObject);
    }
}
