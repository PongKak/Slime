using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public CharacterController characterController;
    public Transform cameraTransform;
    Animator animator;
    // Use this for initialization
	void Start () {

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}

    public float moveSpeed = 3.0f;
    public float sensitivity = 100.0f;
    float yVelocity = -20.0f;
    float rotationX;


	// Update is called once per frame
	void Update () {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        rotationX += x * sensitivity * Time.deltaTime;

        rotationX %= 360.0f;

        if(z == 0)
        {
            animator.SetBool("move", false);
        }
        else
        {
            animator.SetBool("move", true);
        }



        Vector3 moveDirection = new Vector3(0, yVelocity, z);
        moveDirection = transform.TransformDirection(moveDirection);
        //moveDirection.y = 0;
        moveDirection *= moveSpeed * Time.deltaTime;
        
        


        characterController.Move(moveDirection);
        transform.eulerAngles = new Vector3(0.0f, rotationX, 0.0f);

	}
}
