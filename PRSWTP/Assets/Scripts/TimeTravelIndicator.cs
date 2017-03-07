using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to indicate when you've gone back in time
//Will draw a full-screen rectangle which will fade as time goes by
//Unity UI fade

public class TimeTravelIndicator : MonoBehaviour {

    public CanvasGroup myCG;
    private bool flash = false;
    private GUIStyle currentStyle = null;

    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        //changes alpha (transparency) level
        if (flash)
        {
            myCG.alpha = myCG.alpha - Time.deltaTime;
            if (myCG.alpha <= 0)
            {
                myCG.alpha -= 0;
                flash = false;
            }
        }
    }

    void OnGUI()
    {
        if (flash)
        {
            InitStyles();
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", currentStyle);
        }

    }

	public void setFlash () {
		flash = true;	
	}

    private void InitStyles()
    {

            currentStyle = new GUIStyle(GUI.skin.box);
            currentStyle.normal.background = MakeTex(2, 2, new Color(1.0f, 1.0f, 1.0f, myCG.alpha));

    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
