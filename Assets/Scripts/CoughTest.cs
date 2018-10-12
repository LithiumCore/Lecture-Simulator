using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoughTest : MonoBehaviour {
    private AudioSource cough;
    private Renderer rend;
    // Use this for initialization
    void Start () {
        cough = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", Color.white);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp("c"))
        {
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.red);
            print("cough");
            cough.Play();
            rend.material.SetColor("_Color", Color.white);
        }
    }
}
