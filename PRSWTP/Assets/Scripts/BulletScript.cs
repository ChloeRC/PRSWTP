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
        if (time > .2)
        {
            kill();
        }
    }

    void kill()
    {
        Destroy(this.gameObject);
    }
}
