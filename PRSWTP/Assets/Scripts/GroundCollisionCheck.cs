using UnityEngine;
using System.Collections;

public class GroundCollisionCheck : MonoBehaviour {

    public bool collided;

	// Use this for initialization
	void Start () {
        collided = false;
	}

    void OnCollisionEnter (Collision collision)
    {
        if (collision.collider.tag == "ground")
        {
            collided = true;
        }
    }
}
