using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to indicate when you've gone back in time
//Will draw a full-screen rectangle which will fade as time goes by
//Unity UI fade

public class TimeTravelIndicator : MonoBehaviour {

    public CanvasGroup myCG;
    private bool flash = false;
    public Texture texture;

    void Start() {

        string texture2 = "Assets/Resources/Textures/WhiteTexture.png";
        Texture2D texture = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(texture2, typeof(Texture2D));
        //texture = (Texture2D)Resources.Load("Assets/Textures/WhiteTexture");
    }

    // Update is called once per frame
    // For fading??
    void Update()
    {

        if (flash)
        {
            Graphics.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
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

    void OnGUI()
    {
        //broken :(
        if (flash)
        {
            //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture, ScaleMode.ScaleToFit);
            //GUI.color = Color.white;
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

            GUI.Label(new Rect(50, 50, 50, 50), "bam bam time travel");
        }

    }

	public void setFlash () {
		flash = true;		
	}
}
