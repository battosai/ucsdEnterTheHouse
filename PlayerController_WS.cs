using UnityEngine;
using System.Collections;

public class PlayerController_WS : MonoBehaviour 
{
	private Rigidbody rb;
	public float moveSpeed;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate() //called just before any physics operations; physics code
	{
		//record when keys are pressed
        bool moveFwd = Input.GetKey(KeyCode.W);
        bool moveBwd = Input.GetKey(KeyCode.S);
		
		//only move when W or S is pressed
		if(moveFwd)
			transform.position = transform.position + Camera.main.transform.forward * moveSpeed * Time.deltaTime;
		else if(moveBwd)
			transform.position = transform.position + Camera.main.transform.forward * moveSpeed * Time.deltaTime * -1;
		else
			rb.velocity = new Vector3(0, 0, 0);
	}
}
