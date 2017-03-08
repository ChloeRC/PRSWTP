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
        bool controllable = gameObject.GetComponent<PlayerScript>().getControllable();

        if (controllable && col.gameObject.tag == "Player")
        {
            Debug.Log("boop");
            kill();
        }

    }

    //Literally the most satisfying function in this entire project.
    public void kill()
    {
        Destroy(this.gameObject);
    }
}
