using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to indicate when you've gone back in time
//Will draw a full-screen rectangle which will fade as time goes by
public class TimeTravelIndicator : MonoBehaviour {

	public CanvasGroup myCG;
	private bool flash = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void setFlash () {
		flash = true;
		if (flash) {
			GUI.color = Color.white;
			GUI.Box(new Rect(0,0,Screen.width,Screen.height),"");
			flash = false;
		}
	}
		/* if (flash)
		{
			myCG.alpha = myCG.alpha - 1;
			//flash = false;
			Debug.Log ("ok");
			if (myCG.alpha <= 0)
			{
				//This stuff is not running
				myCG.alpha = 1;
				flash = false;
				Debug.Log ("Flash: " + flash);
			}
		}
	}

	public void setFlash ()
	{
		flash = true;
	} */
		
}
