using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnvironment : MonoBehaviour {

    public GameObject environment;
    public int numOfobject = 20;
    GameObject[] objectPool;

	// Use this for initialization
	void Start () {

        objectPool = new GameObject[numOfobject];
        for (int i = 0; i < numOfobject; i++)
        {
            Vector3 position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            GameObject temp = Instantiate(environment,position,Quaternion.identity) as GameObject;
            temp.transform.Rotate(new Vector3(0.0f, Random.Range(1,5) * 90.0f, 0.0f));
            temp.transform.parent = transform;
            objectPool[i] = temp;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
