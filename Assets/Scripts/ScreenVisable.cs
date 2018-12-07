using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ScreenVisable : MonoBehaviour {

    //Renderer m_Renderer;
    public Camera cam;
    VideoPlayer vPlayer;
    static public int totalTime = 0;
    static public int screenTime = 0;
    readonly private int screenLayerMask = 1 << 10;
    static public float dist;
    public Text timeText;

    // Use this for initialization
    void Start () {
        //m_Renderer = GetComponent<Renderer>();
        vPlayer = GetComponent<VideoPlayer>();
        SetTimeText();

        vPlayer.loopPointReached += EndReached;
    }
	
	// Update is called once per frame
	void Update () {
        totalTime++;
        //if (m_Renderer.isVisible)
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 10, Color.green, 10f);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward * 10, Mathf.Infinity, screenLayerMask))
        {
            screenTime++;
        }
        SetTimeText();
        dist = Vector3.Distance(cam.transform.position, transform.position);
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        print("Video Is Over");
        SceneManager.LoadScene("Scenes/QuizInterface", LoadSceneMode.Single);
    }

    void SetTimeText() {
        timeText.text = "Screen Time: " + screenTime + System.Environment.NewLine + "Total Time:" + totalTime + System.Environment.NewLine + "Screen Distance:" + dist;
    }
}
