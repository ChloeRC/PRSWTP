using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is intended to represent all the past selves
 * in order to differentiate current/past selves from each
 * other. More betterer.
 * 
 * It might not work. Sorry :(
 * It definitely doesn't work.s
*/

public class CloneScript : MonoBehaviour {

    public bool isDead;

    private CloneSwordScript sword;
    
    private Vector3 currDirection;

    // Use this for initialization
    void Start () {
        isDead = false;

        sword = GetComponentInChildren<CloneSwordScript>();
    }

    // Update is called once per frame
    void Update () {
        bool drawn = sword.drawn;

        if (drawn)
        {
            sword.swordUp(currDirection);
        }
        else
        {
            sword.swordDown(currDirection);
        }
    }

    public void setRotation()
    {
        //insert math here that calculates differences
        //in x values from setPosition :)
    }

    //taken from PlayerScript - sets position of the clone
    public void setLocation(CloneLocation cloneLocation)
    {
        if (!cloneLocation.Equals(TrackMovement.DESTROY_CLONE))
        {
            transform.position = cloneLocation.getLocation();

            Vector3 newRot = cloneLocation.getRotation();

            transform.rotation = Quaternion.Euler(new Vector3(newRot.x, newRot.y-90, newRot.z));
            currDirection = transform.rotation.eulerAngles;

            if (cloneLocation.getDidSword())
            {
                Debug.Log("STABBY STAB STAB");
                sword.drawn = true;
            }
            else if (cloneLocation.getDidShoot())
            {
                Debug.Log("One two three four five six seven--");
            }
        }
        else
        {
            this.kill();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerScript>().kill();
            Debug.Log("You colllided with your past self!!");
        }
    }

    void useSword()
    {
        sword.drawn = true;
    }

    public void kill()
        {
            Destroy(this.gameObject);
        }
    }
