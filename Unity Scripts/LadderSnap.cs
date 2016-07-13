using UnityEngine;
using System.Collections;

public class LadderSnap : MonoBehaviour 
{
	public int snapID;

	bool snap;
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
		if (other.gameObject == theLadder) 
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
		}
	}

	// Update is called once per frame
	void Update () 
	{
		theLadderPos = theLadder.transform.position;
		handsAreFull = GameObject.FindWithTag ("Player").GetComponent<broadPickUp> ().handsAreFull;

		if (snap) 
		{
			switch (snapID) 
			{
			case 1:
				dummyLadder.transform.position = new Vector3 (dummyLadderPos.x, dummyLadderPos.y, theLadderPos.z);
				break;
			}
		}
	}

	/* SNAP IDs
	 * 1: x = (7.12, -10degrees), y = (0.05, 90degrees), z = 0degrees
	 * 2:
	 * 3:
	*/
	void swapLadders()
	{
		foreach (Renderer LadderChildRenderer in theLadderRenderer)
		{
			LadderChildRenderer.enabled = false;
		}
		//theLadderCollider.isTrigger = true;
		theLadderCollider.enabled = false;
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
			break;
		case 3:
			break;
		}

		dummyLadder.transform.position = dummyLadderPos;
		dummyLadder.transform.rotation = dummyLadderRot;
	}
}
