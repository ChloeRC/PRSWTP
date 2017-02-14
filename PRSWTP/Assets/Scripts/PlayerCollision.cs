using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("HELLO!");
        if (col.gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<PlayerScript>().health--;
            Debug.Log("hello");
        }

        bool controllable = gameObject.GetComponent<PlayerScript>().getControllable();

        if (controllable && col.gameObject.tag == "Player")
        {
            kill();
        }
    }

    //Literally the most satisfying function in this entire project.
    public void kill()
    {
        Debug.Log("Killed: " + gameObject.tag);
        Destroy(this.gameObject);
    }
}
