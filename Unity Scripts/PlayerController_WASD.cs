using UnityEngine;
using System.Collections;

public class PlayerController_WASD : MonoBehaviour 
{
	public float mouseSpeed;
	public float moveSpeed;
	
	void FixedUpdate() //called just before any physics operations; physics code
	{
		bool inputEnabled = GameObject.FindWithTag("Door").GetComponent<doorUnlock>().userInputEnabled;
		
		//record when keys are pressed
        bool moveFwd = Input.GetKey(KeyCode.W);
        bool moveBwd = Input.GetKey(KeyCode.S);
		bool moveLeft = Input.GetKey(KeyCode.A);
		bool moveRight = Input.GetKey(KeyCode.D);
		
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
}
