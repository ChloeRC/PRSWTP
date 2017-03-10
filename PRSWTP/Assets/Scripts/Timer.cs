using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
  /*  public GameObject player;
    public GameObject timer;
    private double currentTime;
    private double timeTravel;
    */
    public double secs;
    public double min;
    private string timeDisp;

	void Start () {
     /*   currentTime = player.GetComponent<PlayerScript>().timer;
        TrackMovement trackm = player.GetComponent<TrackMovement>();
        */
        secs = 0;
        min = 0;
        Debug.Log(timeDisp);
        
	}

    void Update () {
        /*   PlayerScript PlayerScript = GetComponent<PlayerScript>();
           TimeTravelIndicator TimeTravelIndicator = GetComponent<TimeTravelIndicator>();
           TrackMovement TrackMovement = GetComponent<TrackMovement>();
           */
        int stemp = (int)secs;
        int mtemp = (int)min;

        if (secs < 10)
        {
            secs += Time.deltaTime;
            timeDisp = mtemp.ToString() + ":0" + stemp.ToString();

        }

        else if (secs > 10 && secs < 60) {
             secs += Time.deltaTime;
             timeDisp = mtemp.ToString() + ":" + stemp.ToString();
        }
        
        else {
            secs = 0;
            min++;
            timeDisp = mtemp.ToString() + ":" + stemp.ToString();
        }

        transform.Find("Text").GetComponent<TextMesh>().text = timeDisp;
    }
}
