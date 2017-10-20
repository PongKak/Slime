using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public Player player;
    public Transform target;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
        target = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
        
	}

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + target.TransformDirection(0, 5, -10);//target.TransformPoint(0, 5, -10); 이걸 하면 종속적
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        transform.LookAt(target);
    }
}
