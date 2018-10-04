using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChairPrefab : MonoBehaviour
{
    private Camera mainCam;

	// Use this for initialization
	void Start () {
        mainCam = Object.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
        mainCam.transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        mainCam.transform.eulerAngles= new Vector3(0, 0, 0);
        mainCam.fieldOfView = 90;
    }
}
