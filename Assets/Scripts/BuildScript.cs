using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildScript : MonoBehaviour {

    //Make sure to attach these Buttons in the Inspector
    public Button chairButton, wallButton, cancelButton, deleteButton, startButton, spawnButton;
    public Slider spawnSlider, tabletSlider;
    public GameObject start, end;
    public GameObject wallPrefab;
    GameObject wall;
    public Camera cam;
    private string mode = "None";
    private bool creating = false;
    public GameObject chair;
    public GameObject spawner;
    public Text modeText;
    public GameObject buildMenu, lectureMenu;
    public GameObject scripts;
    public static bool customBuilt = false;
    

    void Start()
    {
        customBuilt = true;
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        chairButton.onClick.AddListener(chairMode);
        wallButton.onClick.AddListener(wallMode);
        deleteButton.onClick.AddListener(deleteMode);
        //m_YourSecondButton.onClick.AddListener(delegate { TaskWithParameters("Hello"); });
        //m_YourThirdButton.onClick.AddListener(() => ButtonClicked(42));
        cancelButton.onClick.AddListener(cancelMode);
        startButton.onClick.AddListener(startLecture);
        spawnButton.onClick.AddListener(spawnerMode);
        SetModeText();
    }

    void Update()
    {
        spawnButton.GetComponentInChildren<Text>().text = "NPC Spawner: " + spawnSlider.value.ToString() + "%";
        tabletSlider.GetComponentInChildren<Text>().text = tabletSlider.value.ToString() + "% Tablet Users";
        if (mode == "chair")
        {
            if (Input.GetMouseButtonDown(0) && getWorldPoint() != Vector3.zero) {
                Instantiate(chair, getWorldPoint(), Quaternion.identity);
            }
        }
        else if (mode == "wall") {
            getWallInput();
        }
        else if (mode == "spawner") {
            if (Input.GetMouseButtonDown(0) && getWorldPoint() != Vector3.zero)
            {
                Instantiate(spawner, getWorldPoint(), Quaternion.identity);
            }
        }
        else if (mode == "delete") {
            if (Input.GetMouseButtonDown(0) && getWorldPoint() != Vector3.zero)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Chair" || hit.collider.tag == "Wall" || hit.collider.tag == "Spawner")
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    void chairMode()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the chair button!");
        mode = "chair";
        cancelButton.interactable = true;
        SetModeText();
    }

    void spawnerMode() {
        //Output this to console when Button1 or Button3 is clicked
        mode = "spawner";
        cancelButton.interactable = true;
        SetModeText();
    }

    void wallMode()
    {
        //Output this to console when Button1 or Button3 is clicked
        mode = "wall";
        cancelButton.interactable = true;
        SetModeText();
    }

    void deleteMode()
    {
        //Output this to console when Button1 or Button3 is clicked
        mode = "delete";
        cancelButton.interactable = true;
        SetModeText();
    }

    void TaskWithParameters(string message)
    {
        //Output this to console when the Button2 is clicked
        Debug.Log(message);
    }

    void cancelMode()
    {
        mode = "None";
        cancelButton.interactable = false;
        SetModeText();
    }

    void startLecture() {
        if (GameObject.FindGameObjectWithTag("Chair") == null) {
            mode = "None";
            modeText.text = "Chair Required to Start";
            cancelButton.interactable = false;
            return;
        }
        mode = "lecture";
        buildMenu.SetActive(false);
        lectureMenu.SetActive(true);
        cam.GetComponent<CameraMove>().enabled = true;
        scripts.GetComponent<SpawnerScript>().enabled = true;
    }

    void getWallInput() {
        if (Input.GetMouseButtonDown(0))
        {
            if (getWorldPoint() != Vector3.zero) { 
                creating = true;
                start.transform.position = getWorldPoint();
                wall = (GameObject)Instantiate(wallPrefab, start.transform.position, Quaternion.identity);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            creating = false;
            end.transform.position = getWorldPoint();
            if (getWorldPoint() == Vector3.zero)
            {
                creating = false;
                Destroy(wall);
            }
            wall = null;
        }
        else {
            if (creating) {
                end.transform.position = getWorldPoint();

                start.transform.LookAt(end.transform.position);
                end.transform.LookAt(start.transform.position);
                float distance = Vector3.Distance(start.transform.position, end.transform.position);
                wall.transform.position = start.transform.position + distance / 2 * start.transform.forward;
                wall.transform.rotation = start.transform.rotation;
                wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, distance);
            }
        }
    }

    Vector3 getWorldPoint() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Debug.Log(hit.point);
            return hit.point;
        }
        return Vector3.zero;
    }

    void SetModeText()
    {
        modeText.text = "Current Mode: " + mode;
    }

    void writeSpawnPercent() {
        spawnButton.GetComponentInChildren<Text>().text = "NPC Spawner: " + spawnSlider.value.ToString()+ "%";
    }
}
