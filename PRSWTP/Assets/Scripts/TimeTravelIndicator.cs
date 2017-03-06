using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to indicate when you've gone back in time
//Will draw a full-screen rectangle which will fade as time goes by
//Unity UI fade

public class TimeTravelIndicator : MonoBehaviour {

    public CanvasGroup myCG;
    private bool flash = false;
    private Texture texture;

    void Start() {
        texture = (Texture2D)Resources.Load("Assets/Textures/WhiteTexture.png");
    }

    // Update is called once per frame
    // For fading??
    void Update()
    {

        if (flash)
        {

            Debug.Log(myCG.alpha);
            myCG.alpha = myCG.alpha - Time.deltaTime;
            if (myCG.alpha <= 0)
            {
                myCG.alpha -= 0;
                flash = false;
                Debug.Log("Flash 2: " + flash);
            }
        }
    }

    private void OnGUI()
    {
        if (flash)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture, ScaleMode.ScaleToFit);
        }

    }

    public void setFlash()
    {
        flash = true;
        myCG.alpha = 1;

    }

    public void test()
    {
        Debug.Log("ok");
    }

    //ugly - temporary
    void onGui()
    {
       if (flash) {
            flash = false;
        }
    }

		
}
