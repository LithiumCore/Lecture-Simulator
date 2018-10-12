using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour {

    public Transform NPC;
    public Transform Chair;
	// Use this for initialization
	void Start () {
        for (int x = -20; x < -10; x = x + 3)
        {
            for (int z = -10; z < -8; z = z +3)
            {
                Instantiate(NPC, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
        for (int x = -2; x <= 2; x = x + 1)
        {
            for (float z = -10; z < 0; z = z + 1.25f)
            {
                Instantiate(Chair, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
