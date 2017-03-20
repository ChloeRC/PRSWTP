using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public int health;
    private int startHealth;

    private Rigidbody rb;
    private Transform ts;
    private float gameTicks;
    public float horizSpeed;
    //True is heading right, false is heading left
    private bool direction = true;

    public float rightEdge;
    public float leftEdge;

    //False for everything but boss
    public bool isBoss;

    public Camera cam;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        ts = GetComponent<Transform>();
        gameTicks = 0.0f;
        rb.freezeRotation = true;

        transform.GetChild(0).GetComponent<TextMesh>().text = "Health: " + health;

        startHealth = health;
    }
	
    void Update () {
        gameTicks += Time.deltaTime;
        //Go right if you are not at the right edge yet, and you are headed right
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

        if (isBoss && health < startHealth && gameTicks > .5)
        {
            gameTicks = 0;
            healthPlusOne();
        }

        if (health == 0)
        {
            kill();
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            healthMinusOne();
        }

		if (col.gameObject.tag == "Player") 
		{
			Debug.Log ("woof");
		}
    }


    public void kill()
    {
        Destroy(this.gameObject);
    }

    public void healthMinusOne()
    {
        health--;
        transform.GetChild(0).GetComponent<TextMesh>().text = "Health: " + health;
    }

    public void healthPlusOne()
    {
        health++;
        transform.GetChild(0).GetComponent<TextMesh>().text = "Health: " + health;
    }
}
