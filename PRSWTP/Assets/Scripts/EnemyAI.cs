using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    private Rigidbody rb;
    private float gameTicks;
    public float horizSpeed;
    private int test = 0;
    private int test2 = 1;
    public int distance;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        gameTicks = 0.0f;
        rb.freezeRotation = true;
	}
	
    void Update () {
        gameTicks += Time.deltaTime;
        if (test < distance && test2 == 1)
        {
            transform.Translate(Vector2.right * horizSpeed * Time.deltaTime);
            test++;
        }
        else
        {
            transform.Translate(Vector2.left * horizSpeed * Time.deltaTime);
            if (test2 == 1)
            {
                test2 = 0;
            }
            if (test > 0)
            {
                test--;
            }
            else
            {
                test2 = 1;
            }
        }
	}
}
