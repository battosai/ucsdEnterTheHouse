using UnityEngine;
using System.Collections;

public class CameraController_AD : MonoBehaviour {
	
	public GameObject player;
	public float rotateSpeed;
	private Vector3 offset;
	
	void Start()
	{
		//maintain this offset for camera following player
		offset = transform.position - player.transform.position;
	}
	
	void FixedUpdate()
	{
		//record when keys are pressed
		bool rotRight = Input.GetKey(KeyCode.D);
		bool rotLeft = Input.GetKey(KeyCode.A);
		
		//rotate when A or D is pressed
		if(rotRight)
			transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
		if(rotLeft)
			transform.Rotate(-Vector3.up * Time.deltaTime * rotateSpeed);
	}
	
	void LateUpdate() //runs every frame like Update, but guaranteed to run after all updates have been processed (make sure player has moved for each frame)
	{
		transform.position = player.transform.position + offset;
	}
}
