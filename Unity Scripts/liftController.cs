using UnityEngine;
using System.Collections;

public class liftController : MonoBehaviour 
{
	public GameObject theLifted;
	public bool usingLift;
	public bool liftingObject;

	bool restrained;
	float xRotation, yRotation;
	float mouseSpeed;
	GameObject player;
	GameObject mainCam;
	GameObject liftBody;

	void Awake()
	{
		mainCam = GameObject.FindWithTag ("MainCamera");
		player = GameObject.FindWithTag ("Player");
		liftBody = GameObject.FindWithTag ("LiftBody");
	}

	void Start()
	{
		mouseSpeed = mainCam.GetComponent<CameraController_Mouse> ().mouseSpeed;
		restrained = false;
		usingLift = false;
		liftingObject = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<pickMeUp> () != null) 
		{
			liftingObject = true;
			theLifted = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == theLifted)
			liftingObject = false;
	}


	//lift should rotate with player along the world Y-Axis
	void FixedUpdate()
	{
		if (usingLift) 
		{
			yRotation += Input.GetAxis ("Mouse X") * mouseSpeed * Time.deltaTime;
			transform.rotation = Quaternion.Euler (-90f, yRotation, 0);
		}
	}

	void Update()
	{
		if (player.GetComponent<broadPickUp> ().theHeld == this.gameObject)
			usingLift = true;
		else 
		{
			restrained = false;
			usingLift = false;
		}

		if (usingLift) 
		{
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionY;
			
		}
			else
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
	}
}