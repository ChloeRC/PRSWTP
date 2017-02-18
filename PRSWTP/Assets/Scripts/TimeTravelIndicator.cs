using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to indicate when you've gone back in time
//Will draw a full-screen rectangle which will fade as time goes by
public class TimeTravelIndicator : MonoBehaviour {
    int i;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void getI(int i)
    {
        this.i = i;
    }

    void OnGUI()
    {

//        while (int i > 0) {

//        }

    /* Tinkering
        Color colPreviousGUIColor = GUI.color;

        GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), txForeground);

        GUI.color = colPreviousGUIColor;
    */ 
    }
}
