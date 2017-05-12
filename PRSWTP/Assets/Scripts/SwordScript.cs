using UnityEngine;
using System.Collections;
[RequireComponent (typeof(AudioSource))]
public class SwordScript : MonoBehaviour {

    public bool drawn;
    //True for sword-out
    //False for sword-down

    public int damage;

    private float gameTicks;
    private float ouchies;
    private float drawTime;

    private Transform ts;

    private bool pause;

    public GameObject healthDisplayer;
    public GameObject PlayerScript;
    

    // Use this for initialization
    void Start () {
        gameTicks = 0.0f;
        ouchies = 0.0f; //keeps track of time between ouching the enemy
        drawTime = 0.3f; //alter this to modify how long the sword stays up
        ts = GetComponent<Transform>();
        pause = false;

        if (drawn) {
            swordUp(false);
        }
        else {
            swordDown(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        ouchies += Time.deltaTime;
        if (drawn)
        {
            gameTicks += Time.deltaTime;
        }

		//fix this line vvvvv
        pause = gameObject.GetComponentInParent<PlayerScript>().getPause();
	}

	public void toggleSword(bool direction)
    {
        drawn = !drawn;

        if (drawn) { 
			swordUp(direction);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            //gameObject.GetComponent<swordsound>().Kaching(); 
		}
    }

	public void swordUp (bool direction)
    {
		//sword out facing left
		if (direction) {
            GetComponent<AudioSource>().Play();
            ts.localPosition = new Vector3 (-1, 0.15f, 0);
			Quaternion newRot = Quaternion.Euler (-10, 270, 90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
			ts.localScale = new Vector3 (0.055f, 0.1f, 0.1f);
            if (gameTicks > drawTime)
            {
                swordDown(direction);
                gameTicks = 0.0f;
                drawn = !drawn;
            }
		} else {
			//sword out facing right
			ts.localPosition = new Vector3 (1, 0.15f, 0);
			Quaternion newRot = Quaternion.Euler (-10, 90, 90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
			ts.localScale = new Vector3 (0.055f, 0.1f, 0.1f);
            if (gameTicks > drawTime)
            {
                swordDown(direction);
                gameTicks = 0.0f;
                drawn = !drawn;
            }
        }
    }

	public void swordDown(bool direction)
    {
        if (direction) {
			ts.localPosition = new Vector3 (0, 0.15f, 0.55f);
			Quaternion newRot = Quaternion.Euler (78, 90, -90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
            ts.localScale = new Vector3 (0.09f, 0.1f, 0.08f);
		} else {
			ts.localPosition = new Vector3 (0, 0.15f, -0.55f);
			Quaternion newRot = Quaternion.Euler (78, 90, -90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
            ts.localScale = new Vector3(0.04f, 0.1f, 0.08f);
        }
    }

    //this actually controls all the close-up player/enemy collisions because haha
    void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.tag == "Enemy" && drawn && ouchies > 0.1f && !pause)
        {
            EnemyAI enemyAI = col.gameObject.GetComponent<EnemyAI>();
            BossAI bossAI = col.gameObject.GetComponent<BossAI>();
            if (enemyAI != null) { enemyAI.adjustHealthBy(-damage); }
            else if (bossAI != null) { bossAI.adjustHealthBy(-damage); }

            ouchies = 0.0f;
		}
        //to lose health you also need to be your current self
        else if (col.gameObject.tag == "Enemy" && !drawn  && gameObject.GetComponentInParent<PlayerScript>().getCollisionTime() > 0.5f
			&& !gameObject.GetComponentInParent<PlayerScript>().getInv() && !pause)
        {
            gameObject.GetComponentInParent<PlayerScript>().health--;
            healthDisplayer.GetComponent<HealthBarDisplay>().UpdateText();
            gameObject.GetComponentInParent<PlayerScript>().resetCollisionTime();
        }
    }
}
