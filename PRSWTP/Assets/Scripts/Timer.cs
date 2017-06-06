using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public double secs;
    public double min;
    private string timeDisp;
    private bool pause;

	void Start ()
    {
        pause = false;
        secs = 0;
        min = 0;
    }

    void Update () {
        pause = GetComponentInParent<PlayerScript>().getPause();
        int stemp = (int)secs;
        int mtemp = (int)min;
       if (!pause)
       {
            if (secs < 10)
            {
                secs += Time.deltaTime;
                timeDisp = mtemp.ToString() + ":0" + stemp.ToString();

            }

            else if (secs > 10 && secs < 60)
            {
                secs += Time.deltaTime;
                timeDisp = mtemp.ToString() + ":" + stemp.ToString();
            }

            else
            {
                secs = 0;
                min++;
                timeDisp = mtemp.ToString() + ":" + stemp.ToString();
            }
        }
        transform.Find("Text").GetComponent<TextMesh>().text = timeDisp;
    }

    public void reset()
    {
        secs = 0;
        min = 2;
    }
}
