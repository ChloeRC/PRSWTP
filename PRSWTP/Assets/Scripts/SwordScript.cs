using UnityEngine;
using System.Collections;
using System;
[RequireComponent (typeof(AudioSource))]

public class SwordScript : CloneSwordScript {

    public GameObject healthDisplayer;

    new void Start()
    {
        base.Start();
        base.rightRot = new Vector3(0, 90, 0);
        base.leftRot = new Vector3(0, 270, 0);
    }
    
    // Update is called once per frame
	new void Update () {
        base.Update();
	}

	public new void toggleSword(Vector3 direction)
    {
        base.toggleSword(direction);
    }

	public new void swordUp (Vector3 direction)
    {
        base.swordUp(direction);
    }

	public new void swordDown(Vector3 direction)
    {
        base.swordDown(direction);
    }

    //this actually controls all the close-up player/enemy collisions because haha
    new void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        //to lose health you also need to be your current self
        if (col.gameObject.tag == "Enemy" && !drawn  && gameObject.GetComponentInParent<PlayerScript>().getCollisionTime() > 0.5f
			&& !gameObject.GetComponentInParent<PlayerScript>().getInv() && !pause)
        {
            gameObject.GetComponentInParent<PlayerScript>().health--;
            healthDisplayer.GetComponent<HealthBarDisplay>().UpdateText();
            gameObject.GetComponentInParent<PlayerScript>().resetCollisionTime();
        }
    }
}
