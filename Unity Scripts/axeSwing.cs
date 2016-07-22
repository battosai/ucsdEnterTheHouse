using UnityEngine;
using System.Collections;

public class axeSwing : MonoBehaviour {
	public bool holdingAxe;
	public bool oneRun;

	GameObject player;
	GameObject axeBlade;
	GameObject axeHandle;
	GameObject theDoor;

	void Awake()
	{
		player = GameObject.FindWithTag ("Player");
		axeBlade = GameObject.FindWithTag ("AxeBlade");
		axeHandle = GameObject.FindWithTag ("AxeHandle");
		theDoor = GameObject.FindWithTag ("Door");
	}

	void Start()
	{
		oneRun = false;
		holdingAxe = false;
		axeBlade.GetComponent<Collider> ().enabled = false;
		axeHandle.GetComponent<Collider> ().enabled = false;
	}

	void Update()
	{
		if (player.GetComponent<broadPickUp> ().theHeld == this.gameObject)
			holdingAxe = true;
		else
			holdingAxe = false;

		if (oneRun) 
		{
			swapColliders ();
			oneRun = false;
		}
	}

	void swapColliders()
	{
		axeBlade.GetComponent<Collider> ().enabled = !axeBlade.GetComponent<Collider> ().enabled;
		axeHandle.GetComponent<Collider> ().enabled = !axeHandle.GetComponent<Collider> ().enabled;
		GetComponent<Collider> ().enabled = !GetComponent<Collider> ().enabled;
	}
		
}
