using UnityEngine;
using System.Collections;

public class LadderSnap : MonoBehaviour 
{
	public int snapID;
	public bool snap;

	bool handsAreFull;
	GameObject theHeld;

	GameObject theLadder;
	Vector3 theLadderPos;
	Quaternion theLadderRot;
	Renderer[] theLadderRenderer;
	Collider theLadderCollider;

	GameObject dummyLadder;
	Vector3 dummyLadderPos;
	Quaternion dummyLadderRot;
	Renderer[] dummyLadderRenderer;
	Collider dummyLadderCollider;

	void Awake () 
	{
		snap = false;
		handsAreFull = GameObject.FindWithTag ("Player").GetComponent<broadPickUp> ().handsAreFull;
		theHeld = GameObject.FindWithTag ("Player").GetComponent<broadPickUp> ().theHeld;

		theLadder = GameObject.FindWithTag ("Ladder");
		theLadderPos = theLadder.transform.position;
		theLadderRot = theLadder.transform.rotation;
		theLadderRenderer = theLadder.GetComponentsInChildren<Renderer> ();
		theLadderCollider = theLadder.GetComponent<Collider> ();

		dummyLadder = GameObject.FindWithTag ("dummyLadder");
		dummyLadderPos = dummyLadder.transform.position;
		dummyLadderRot = dummyLadder.transform.rotation;
		dummyLadderRenderer = dummyLadder.GetComponentsInChildren<Renderer> ();
		dummyLadderCollider = dummyLadder.GetComponent<Collider> ();
		foreach (Renderer dummyChildRenderer in dummyLadderRenderer) 
		{
			dummyChildRenderer.enabled = false;
		}
		dummyLadderCollider.enabled = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == theLadder && theHeld == theLadder && handsAreFull) 
		{
			snap = true;
			swapLadders ();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == theLadder) 
		{
			snap = false;
			restoreLadders ();
		}
	}

	void FixedUpdate () 
	{
		theLadderPos = theLadder.transform.position;
		handsAreFull = GameObject.FindWithTag ("Player").GetComponent<broadPickUp> ().handsAreFull;
		theHeld = GameObject.FindWithTag ("Player").GetComponent<broadPickUp> ().theHeld;

		if (snap) 
		{
			switch (snapID) 
			{
			case 1:
				dummyLadder.transform.position = new Vector3 (dummyLadderPos.x, dummyLadderPos.y, theLadderPos.z);
				break;
			case 2:
				dummyLadder.transform.position = new Vector3 (theLadderPos.x, dummyLadderPos.y, dummyLadderPos.z);
				break;
			case 3:
				dummyLadder.transform.position = new Vector3 (theLadderPos.x, dummyLadderPos.y, dummyLadderPos.z);
				break;
			case 4:
				dummyLadder.transform.position = new Vector3 (dummyLadderPos.x, dummyLadderPos.y, theLadderPos.z);
				break;
			}
		}
	}

	/* SNAP IDs
	 * 1: x = (7.12, -10degrees), y = (0.05, 90degrees), z = 0degrees
	 * 2: x = (-10degrees), y = (0.05, 0degrees), z = (-31.95, 0degrees)
	 * 3: x = (10degrees), y = (0, 0degrees), z = (-42.5, 0degrees)
	 * 4: x = (-0.17, 10degrees), y = (0.05, 90degrees), z = 0degrees
	*/
	void swapLadders()
	{
		foreach (Renderer LadderChildRenderer in theLadderRenderer)
		{
			LadderChildRenderer.enabled = false;
		}
		theLadderCollider.isTrigger = true;
		theLadder.GetComponent<Rigidbody> ().isKinematic = true;

		foreach (Renderer dummyChildRenderer in dummyLadderRenderer) 
		{
			dummyChildRenderer.enabled = true;
		}
		dummyLadderCollider.enabled = true;

		switch(snapID)
		{
		case 1:
			dummyLadderPos = new Vector3 (7.12f, 0.05f, theLadderPos.z);
			dummyLadderRot = Quaternion.Euler(-10, 90, 0);
			dummyLadder.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
																 RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX |
																 RigidbodyConstraints.FreezePositionY;
			break;
		case 2:
			dummyLadderPos = new Vector3 (theLadderPos.x, 0.05f, -31.95f);
			dummyLadderRot = Quaternion.Euler (-10, 0, 0);
			dummyLadder.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
																 RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY |
																 RigidbodyConstraints.FreezePositionZ;
			break;
		case 3:
			dummyLadderPos = new Vector3 (theLadderPos.x, 0f, -42.5f);
			dummyLadderRot = Quaternion.Euler (10, 0, 0);
			dummyLadder.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
																 RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY |
																 RigidbodyConstraints.FreezePositionZ;
			break;
		case 4:
			dummyLadderPos = new Vector3 (-0.17f, 0.05f, theLadderPos.z);
			dummyLadderRot = Quaternion.Euler (10, 90, 0);
			dummyLadder.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
															 	 RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX |
																 RigidbodyConstraints.FreezePositionY;
			break;
		}

		dummyLadder.transform.position = dummyLadderPos;
		dummyLadder.transform.rotation = dummyLadderRot;
	}

	void restoreLadders()
	{
		foreach (Renderer dummyChildRenderer in dummyLadderRenderer) 
		{
			dummyChildRenderer.enabled = false;
		}
		dummyLadderCollider.enabled = false;

		foreach (Renderer LadderChildRenderer in theLadderRenderer) 
		{
			LadderChildRenderer.enabled = true;
		}	
		theLadderCollider.isTrigger = false;
		theLadder.GetComponent<Rigidbody> ().isKinematic = false;

		switch (snapID) 
		{
		case 1:
			theLadderRot = Quaternion.Euler(-10, 90, 0);
			break;
		case 2:
			theLadderRot = Quaternion.Euler (-10, 0, 0);
			break;
		case 3:
			theLadderRot = Quaternion.Euler (10, 0, 0);
			break;
		case 4:
			theLadderRot = Quaternion.Euler (10, 90, 0);
			break;
		}

		theLadder.transform.rotation = dummyLadderRot;
	}
}
