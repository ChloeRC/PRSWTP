using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public int health;

    private Rigidbody rb;
    private Transform ts;
    private float gameTicks;
    public float horizSpeed;
    //True is heading right, false is heading left
    private bool direction = true;

    public float rightEdge;
    public float leftEdge;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        ts = GetComponent<Transform>();
        gameTicks = 0.0f;
        rb.freezeRotation = true;
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

        if (health == 0)
        {
            kill();
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            health--;
        }

    }


    public void kill()
    {
        Destroy(this.gameObject);
    }
}
