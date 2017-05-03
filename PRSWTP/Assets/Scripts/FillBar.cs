using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBar : MonoBehaviour {

    public float curr;
    public float full;
	public float currWidth;
	public float currPos;
    public float originalWidth;

    public bool useDivide;

    public GameObject text;

    public string originalText;

    // Use this for initialization
    public void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (useDivide) { text.GetComponent<TextMesh>().text = originalText + curr + " / " + full; }
        else { text.GetComponent<TextMesh>().text = originalText; }

        //Debug.Log("width: " + originalWidth);

        float currWidth = originalWidth * ((float)curr / (float)full);
        gameObject.transform.localScale = new Vector3(currWidth, 0.27f, 0.1f);

        float currPos = -(originalWidth / 2f) + (.5f) * currWidth;
        gameObject.transform.localPosition = new Vector3(currPos, 0, -0.1f);
    }
}
