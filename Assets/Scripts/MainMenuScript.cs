using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuScript : MonoBehaviour {

    public Button buildButton, defaultButton, uploadVideoButton, resetButton;
    public static string uploadedVideo, uploadedSound, uploadedQuestions;

    // Use this for initialization
    void Start () {
        buildButton.onClick.AddListener(MoveToBuildMode);
        defaultButton.onClick.AddListener(MoveToDefaultMode);

        uploadVideoButton.onClick.AddListener(UploadVideo);
        resetButton.onClick.AddListener(ResetToDefault);

        ResetToDefault();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void MoveToBuildMode() {
        SceneManager.LoadScene("Scenes/BuildScene", LoadSceneMode.Single);
    }

    void MoveToDefaultMode() {
        SceneManager.LoadScene("Scenes/SampleScene", LoadSceneMode.Single);
    }

    void UploadVideo() {
        uploadedVideo = UnityEditor.EditorUtility.OpenFilePanel("Upload Custom Video", "", "mp4");
        //uploadedQuestions = EditorUtility.OpenFilePanel("Upload Question Pack", "", "txt");
    }

    void ResetToDefault() {
        uploadedVideo = "";
        uploadedQuestions = "";
    }
}
