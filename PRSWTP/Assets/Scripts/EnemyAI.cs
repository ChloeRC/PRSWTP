using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class EnemyAI : MonoBehaviour {
    public int health;
    public int bulletDamage;

    private Rigidbody rb;
    private Transform ts;
    private float gameTicks;
    public float horizSpeed;
    //True is heading right, false is heading left
    private bool direction = true;
    private bool pause;

    public float rightEdge;
    public float leftEdge;

    public GameObject player;

    public Camera cam;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        ts = GetComponent<Transform>();
        gameTicks = 0.0f;
        rb.freezeRotation = true;

        transform.GetChild(0).GetComponent<TextMesh>().text = "Health: " + health;
    }
	
    void Update () {
        gameTicks += Time.deltaTime;
        pause = player.GetComponent<PlayerScript>().getPause();
        //Debug.Log(pause);

        //Go right if you are not at the right edge yet, and you are headed right
        if (!pause)
        {

            if (this.ts.position.x < rightEdge && direction)
            {
                transform.Translate(Vector2.right * horizSpeed * Time.deltaTime);
            }
            //If you're at the right edge, or you're headed left, go left.
            else
            {
                transform.Translate(Vector2.left * horizSpeed * Time.deltaTime);
                //You're headed left until you hit the edge, 
                //then direction becomes true again and you go back right.
                if (direction)
                {
                    direction = false;
                }
                if (this.ts.position.x < leftEdge)
                {
                    direction = true;
                }
            }

            if (health <= 0)
            {
                kill();  
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            adjustHealthBy(-bulletDamage);
        }

		if (col.gameObject.tag == "Player") 
		{
			Debug.Log ("woof");
		}
    }


    public void kill()
    {
        gameTicks = 0.0f;
        GetComponent<AudioSource>().Play();
        if (gameTicks >= 3.0f)
        {    
            Debug.Log("")
            Destroy(this.gameObject);
        }
        
    }

    public void adjustHealthBy(int toAdd)
    {
        //Debug.Log("adding " + toAdd);
        health += toAdd;
        transform.GetChild(0).GetComponent<TextMesh>().text = "Health: " + health;
    }
}
