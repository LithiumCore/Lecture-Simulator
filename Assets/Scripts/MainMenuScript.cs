using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public Button buildButton, defaultButton;

    // Use this for initialization
    void Start () {
        buildButton.onClick.AddListener(MoveToBuildMode);
        defaultButton.onClick.AddListener(MoveToDefaultMode);
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
}
