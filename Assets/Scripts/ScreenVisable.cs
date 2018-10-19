using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenVisable : MonoBehaviour {

    Renderer m_Renderer;
    private int totalTime = 0;
    private int screenTime = 0;
    public Text timeText;

    // Use this for initialization
    void Start () {
        m_Renderer = GetComponent<Renderer>();
        SetTimeText();
    }
	
	// Update is called once per frame
	void Update () {
        totalTime++;
        if (m_Renderer.isVisible)
        {
            screenTime++;
        }
        SetTimeText();
    }

    void SetTimeText() {
        timeText.text = "Screen Time: " + screenTime + System.Environment.NewLine + "Total Time:" + totalTime;
    }
}
