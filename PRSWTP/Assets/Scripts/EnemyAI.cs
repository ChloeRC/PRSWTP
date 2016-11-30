using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    private Rigidbody rb;
    private Transform ts;
    private float gameTicks;
    public float horizSpeed;
    private int test = 0;
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
        if (this.ts.position.x < rightEdge && direction)
        {
            transform.Translate(Vector2.right * horizSpeed * Time.deltaTime);
            test++;
        }
        else
        {
            transform.Translate(Vector2.left * horizSpeed * Time.deltaTime);
            if (direction)
            {
                direction = false;
            }
            if (this.ts.position.x > leftEdge)
            {
                test--;
            }
            else
            {
                direction = true;
            }
        }
	}
}
