using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSwordScript : MonoBehaviour {
    private CloneScript clone;

    private float gameTicks;
    private float ouchies;
    private float drawTime;

    private Transform ts;

    public bool pause;

    public Vector3 rightRot;
    public Vector3 leftRot;

    public bool drawn;

    public int damage;

    // Use this for initialization
    public void Start()
    {
        clone = GetComponentInParent<CloneScript>();
        gameTicks = 0.0f;
        ouchies = 0.0f; //keeps track of time between ouching the enemy
        drawTime = 0.3f; //alter this to modify how long the sword stays up
        ts = GetComponent<Transform>();
        pause = false;

        if (drawn)
        {
            swordUp(rightRot);
        }
        else
        {
            swordDown(rightRot);
        }

        rightRot = new Vector3(0.0f, 0.0f, 0.0f);
        leftRot = new Vector3(0.0f, 180.0f, 0.0f);
    }

    // Update is called once per frame
    public void Update()
    {
        
        ouchies += Time.deltaTime;
        if (drawn)
        {
            gameTicks += Time.deltaTime;
        }

        //fix this line vvvvv
        //pause = gameObject.GetComponentInParent<PlayerScript>().getPause();
    }

    public void toggleSword(Vector3 direction)
    {
        drawn = !drawn;

        if (drawn)
        {
            swordUp(direction);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            //gameObject.GetComponent<swordsound>().Kaching(); 
        }
    }

    public void swordUp(Vector3 direction)
    {
        //sword out facing left
        if (direction == leftRot)
        {
            Debug.Log("sword is facing left");
            GetComponent<AudioSource>().Play();
            ts.localPosition = new Vector3(-1, 0.15f, 0);
            Quaternion newRot = Quaternion.Euler(-10, 270, 90);
            ts.localRotation = new Quaternion(newRot.x, newRot.y, newRot.z, newRot.w);
            ts.localScale = new Vector3(0.055f, 0.1f, 0.1f);
            if (gameTicks > drawTime)
            {
                swordDown(direction);
                gameTicks = 0.0f;
                drawn = !drawn;
            }
        }
        else if (direction == rightRot)
        {
            //sword out facing right
            GetComponent<AudioSource>().Play();
            ts.localPosition = new Vector3(1, 0.15f, 0);
            Quaternion newRot = Quaternion.Euler(-10, 90, 90);
            ts.localRotation = new Quaternion(newRot.x, newRot.y, newRot.z, newRot.w);
            ts.localScale = new Vector3(0.055f, 0.1f, 0.1f);
            if (gameTicks > drawTime)
            {
                swordDown(direction);
                gameTicks = 0.0f;
                drawn = !drawn;
            }
        }
        else
        {
            Debug.Log("WARNING: THE PLAYER'S DIRECTION IS NOT AN ACCEPTED DIRECTION, DIRECTION IS: " + direction);
        }
    }

    public void swordDown(Vector3 direction)
    {
        if (direction == leftRot)
        {
            ts.localPosition = new Vector3(0, 0.15f, 0.55f);
            Quaternion newRot = Quaternion.Euler(78, 90, -90);
            ts.localRotation = new Quaternion(newRot.x, newRot.y, newRot.z, newRot.w);
            ts.localScale = new Vector3(0.09f, 0.1f, 0.08f);
        }
        else if (direction == rightRot)
        {
            ts.localPosition = new Vector3(0, 0.15f, -0.55f);
            Quaternion newRot = Quaternion.Euler(78, 90, -90);
            ts.localRotation = new Quaternion(newRot.x, newRot.y, newRot.z, newRot.w);
            ts.localScale = new Vector3(0.04f, 0.1f, 0.08f);
        }
        else
        {
            //Debug.Log("WARNING: THE PLAYER'S DIRECTION IS NOT AN ACCEPTED DIRECTION, DIRECTION IS: " + direction);
        }
    }

    //this actually controls all the close-up player/enemy collisions because haha
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && drawn && ouchies > 0.1f && !pause)
        {
            EnemyAI enemyAI = col.gameObject.GetComponent<EnemyAI>();
            BossAI bossAI = col.gameObject.GetComponent<BossAI>();
            if (enemyAI != null) { enemyAI.adjustHealthBy(-damage); }
            else if (bossAI != null) { bossAI.adjustHealthBy(-damage); }

            ouchies = 0.0f;
        }
    }
}
