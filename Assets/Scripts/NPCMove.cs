using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour {

    GameObject target;
    public float speed;
    private bool seated;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!seated){
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
