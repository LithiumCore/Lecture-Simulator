using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerScript : MonoBehaviour {

    GameObject[] spawnList;
    GameObject[] chairList;
    float percent;
    int numToSpawn;
    public Slider spawnSlider;
    public Slider tabletSlider;
    public GameObject NPC;

	// Use this for initialization
	void Start () {
        spawnList = GameObject.FindGameObjectsWithTag("Spawner");
        chairList = GameObject.FindGameObjectsWithTag("Chair");
        percent = spawnSlider.value/100;
        numToSpawn = (int) (chairList.Length * percent);
        Debug.Log(numToSpawn);
        while (numToSpawn > 0) {
            int xRand = Random.Range(-5, 5);
            int zRand = Random.Range(-5, 5);
            GameObject selectedSpawner = spawnList[numToSpawn % spawnList.Length];
            GameObject npcSpawn = Instantiate(NPC, new Vector3(selectedSpawner.transform.position.x + xRand, 0, selectedSpawner.transform.position.z + zRand), Quaternion.identity);
            if (Random.Range(0, 100) <= tabletSlider.value) {
                npcSpawn.GetComponent<NPCMove>().tabletUser = true;
            }
            else {
                npcSpawn.GetComponent<NPCMove>().tabletUser = false;
            }
            numToSpawn--;
        }
        foreach (GameObject spawner in spawnList) {
            spawner.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int getChairCount() {
        return chairList.Length;
    }
}
