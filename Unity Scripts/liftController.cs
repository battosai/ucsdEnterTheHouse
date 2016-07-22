using UnityEngine;
using System.Collections;

public class liftController : MonoBehaviour 
{
	public GameObject theLifted;
	public bool usingLift;
	public bool liftingObject;

	GameObject player;
	GameObject liftBody;

	void Awake()
	{
		player = GameObject.FindWithTag ("Player");
		liftBody = GameObject.FindWithTag ("LiftBody");
	}

	void Start()
	{
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

	void Update()
	{
		if (player.GetComponent<broadPickUp> ().theHeld == this.gameObject)
			usingLift = true;
		else
			usingLift = false;
	}
}
