using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBar : MonoBehaviour {

    public int curr;
    public int full;
	public float currWidth;
	public float currPos;
    public float originalWidth;

    public GameObject text;

    public string originalText;

    // Use this for initialization
    public void Start()
    {
        originalWidth = gameObject.transform.localScale.x;

        UpdateText();
    }

    public void UpdateText()
    {
       
		text.GetComponent<TextMesh>().text = originalText + curr + " / " + full;

        float currWidth = originalWidth * ((float)curr / (float)full);
        gameObject.transform.localScale = new Vector3(currWidth, 0.27f, 0.1f);

        float currPos = -(3.3f / 2f) + (.5f) * currWidth;
        gameObject.transform.localPosition = new Vector3(currPos, 0, -0.1f);
    }
}
