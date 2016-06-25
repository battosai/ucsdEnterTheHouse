﻿using UnityEngine;
using System.Collections;

public class PlayerController_WASD : MonoBehaviour 
{
	public AudioClip footsteps;
	public float mouseSpeed;
	public float moveSpeed;
	
	AudioSource stereo;
	bool moveFwd;
	bool moveBwd;
	bool moveLeft;
	bool moveRight;
	bool footstepsOn;
	
	void Awake()
	{
		stereo = GetComponent<AudioSource>();
		footstepsOn = false;
	}
	
	void FixedUpdate() //called just before any physics operations; physics code
	{
		bool inputEnabled = GameObject.FindWithTag("Door").GetComponent<doorUnlock>().userInputEnabled;
		
		//record when keys are pressed
        moveFwd = Input.GetKey(KeyCode.W);
        moveBwd = Input.GetKey(KeyCode.S);
		moveLeft = Input.GetKey(KeyCode.A);
		moveRight = Input.GetKey(KeyCode.D);
		
		//if endgame is reached, user input will be disabled from doorUnlock script
		if(inputEnabled)
		{
			//only move when W, A, S, or D is pressed
			if(moveFwd)
				transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;
			else if(moveBwd)
				transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime * -1;
			if(moveLeft)
				transform.position = transform.position - (transform.right * moveSpeed * Time.deltaTime);
			else if(moveRight)
				transform.position = transform.position + (transform.right * moveSpeed * Time.deltaTime);
			
			//rotate player along with camera via mouse to allow strafing
			float horizontal = mouseSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
			transform.Rotate(0, horizontal, 0, Space.World);
		}
	}
	
	void Update()
	{	
		//if sound isn't playing and we're walking, play sound
		if((moveFwd || moveBwd || moveLeft || moveRight) && !footstepsOn)
		{
			footstepsOn = true;
			stereo.clip = footsteps;
			stereo.Play();
		}
		//pause if sound is playing and we've stopped walking
		else if(!moveFwd && !moveBwd && !moveLeft && !moveRight && footstepsOn)
		{
			stereo.Pause();
			footstepsOn = false;
		}
	}
}
