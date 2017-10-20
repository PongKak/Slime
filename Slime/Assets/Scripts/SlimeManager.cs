using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour {

    public GameObject slime;

    GameObject[] slimePool;

    public int numOfSlime = 100;

	// Use this for initialization
	void Start () {

        slimePool = new GameObject[numOfSlime];

        for(int i=0; i< numOfSlime; i++)
        {
            Vector3 position = new Vector3((float)Random.Range(-100, 100), 0.0f, (float)Random.Range(-100, 100));
            slimePool[i] = Instantiate(slime, position, Quaternion.identity);
            slimePool[i].transform.parent = transform;            
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
