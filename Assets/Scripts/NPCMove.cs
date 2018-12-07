using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour {

    GameObject target;
    public float speed;
    private Renderer rendererU;
    private bool seated;
    private AudioSource cough;
    public Transform Tablet;
    public int coughWait;
    public bool tabletUser = false;

	// Use this for initialization
	void Start () {
        coughWait = Random.Range(100000, 1000000);
        cough = GetComponent<AudioSource>();
        rendererU = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        coughWait--;
        if (!seated)
        {
            target = FindClosestChair();
            float step = speed * Time.deltaTime;

            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            if (target.transform.position == transform.position)
            {
                target.GetComponent<ChairPrefab>().taken = true;
                seated = true;
            }
        }
        else if (tabletUser){
            Instantiate(Tablet, new Vector3(this.transform.position.x, 0.5f, this.transform.position.z + 0.2f), transform.rotation);
            tabletUser = false;
        }
        if (coughWait == 0) {
            rendererU.material.color = new Color(0, 4.45f, 4.45f);
            cough.Play();
            coughWait = Random.Range(10000, 1000000);
            rendererU.material.color = new Color(1, 1, 1);
        }
    }

    public GameObject FindClosestChair()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Chair");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            if (go.GetComponent<ChairPrefab>().taken == true) {
                continue;
            }
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
