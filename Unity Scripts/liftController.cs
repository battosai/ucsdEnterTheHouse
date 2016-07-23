using UnityEngine;
using System.Collections;

public class liftController : MonoBehaviour 
{
	public GameObject theLifted;
	public bool usingLift;
	public bool liftingObject;
	public bool lockObject;

	bool restrained;
	float yRotation, yRotation2;
	float mouseSpeed;
	GameObject player;
	GameObject mainCam;
	GameObject liftBody;
	GameObject objectZone;

	void Awake()
	{
		mainCam = GameObject.FindWithTag ("MainCamera");
		player = GameObject.FindWithTag ("Player");
		liftBody = GameObject.FindWithTag ("LiftBody");
		objectZone = GameObject.FindWithTag ("ObjectZone");
	}

	void Start()
	{
		mouseSpeed = mainCam.GetComponent<CameraController_Mouse> ().mouseSpeed;
		restrained = false;
		usingLift = false;
		liftingObject = false;
		lockObject = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if ((other.gameObject.GetComponent<pickMeUp> () != null || other.gameObject.GetComponent<liftMeUp>() != null) && theLifted == null) 
		{
			liftingObject = true;
			theLifted = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == theLifted)
		{
			liftingObject = false;
			lockObject = false;
			theLifted = null;
		}
	}
		
	void FixedUpdate()
	{

		//lift should rotate with player along the world y-axis
		if (usingLift) 
		{
			yRotation += Input.GetAxis ("Mouse X") * mouseSpeed * Time.deltaTime;
			transform.rotation = Quaternion.Euler (-90f, yRotation, 0);

			//rotate object being lifted as well
			if (lockObject) 
			{
				yRotation2 += Input.GetAxis ("Mouse X") * mouseSpeed * Time.deltaTime;
				theLifted.transform.position = objectZone.transform.position;
				theLifted.transform.rotation = Quaternion.Euler (0, yRotation2, 0);

				if (theLifted.GetComponent<liftMeUp> () != null)
					theLifted.transform.rotation = Quaternion.Euler (-90, yRotation2 - 360, 0);
			}
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
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionY;
		else
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

		//as long as player isn't walking backwards, lift will always hold object
		if (liftingObject) 
		{
			lockObject = true;

			if(Input.GetKey(KeyCode.S))
				lockObject = false;
		}
	}
}