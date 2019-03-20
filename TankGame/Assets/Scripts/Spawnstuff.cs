using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnstuff : MonoBehaviour {
    public Transform object1; // first obj to spawn
    public Transform object2; // 2nd obj to spawn
    public int numbertospawn = 50; // numbers of obj to spawn
    public float minX = -25, maxX = 50, minY = -25, maxY = 25; // limits the spawns to inside game area

    // Use this for initialization
    void Start () {
        // loops spawn around set amount of time
        //for (int i = 0; i < numbertospawn; i++)
        do
        {
            // randomize the two objs
            if (Random.Range(0, 2) == 0)
                Instantiate(object1, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), transform.rotation);
            else
                Instantiate(object2, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), transform.rotation);
        } while (GameObject.FindGameObjectsWithTag(object1.tag).Length<numbertospawn);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
