﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CameraMove : MonoBehaviour {
    public int speed;
    private Camera mycam;
    private bool seatLocked;
    private bool firstPick;
    private SmoothMouseLook mouseLook;
    public GameObject movie;
	// Use this for initialization
	void Start () {
        mycam = GetComponent<Camera>();
        mouseLook = GetComponent<SmoothMouseLook>();
        movie = GameObject.Find("Screen");
        mouseLook.enabled = false;
        firstPick = true;
        if (MainMenuScript.uploadedVideo.Length > 0)
        {
            movie.GetComponent<AudioSource>().clip = null;
            movie.GetComponent<VideoPlayer>().url = MainMenuScript.uploadedVideo;
            movie.GetComponent<VideoPlayer>().audioOutputMode = VideoAudioOutputMode.AudioSource;
            movie.GetComponent<VideoPlayer>().SetTargetAudioSource(0, movie.GetComponent<AudioSource>());
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = mycam.ScreenPointToRay(Input.mousePosition);
            if (Cursor.visible == false) {
                ray = new Ray(mycam.transform.position, mycam.transform.forward);
            }
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("You selected the " + hit.transform.name);
                if (hit.collider.gameObject.tag == "Chair" && !seatLocked) {
                    Cursor.visible = false;
                    //Screen.lockCursor = true;
                    if (firstPick) { 
                        movie.GetComponent<VideoPlayer>().Play();
                        movie.GetComponent<AudioSource>().Play();
                        movie.GetComponent<ScreenVisable>().enabled = true;
                        firstPick = false;
                    }
                    mycam.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y+1, hit.transform.position.z);
                    mycam.transform.eulerAngles = new Vector3(0, 0, 0);
                    mycam.fieldOfView = 90;
                    seatLocked = false;
                    mouseLook.enabled = true;
                }
                // the object identified by hit.transform was clicked
                // do whatever you want
            }           
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
