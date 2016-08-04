using UnityEngine;
using System.Collections;

public class axeSwing : MonoBehaviour {
	public bool holdingAxe;
	public bool oneRun;

	GameObject player;
	GameObject axeBlade;
	GameObject axeHandle;
	GameObject theDoor;
	public float mouseSpeed;
	float yRotation; 

	void Awake()
	{
		player = GameObject.FindWithTag ("Player");
		axeBlade = GameObject.FindWithTag ("AxeBlade");
		axeHandle = GameObject.FindWithTag ("AxeHandle");
		theDoor = GameObject.FindWithTag ("Door");
		mouseSpeed = GameObject.FindWithTag("MainCamera").GetComponent<CameraController_Mouse> ().mouseSpeed;
	}

	void Start()
	{
		oneRun = false;
		holdingAxe = false;
		axeBlade.GetComponent<Collider> ().enabled = false;
		axeHandle.GetComponent<Collider> ().enabled = false;
	}

	void FixedUpdate()
	{
		if (holdingAxe) 
		{
			yRotation += Input.GetAxis ("Mouse X") * mouseSpeed * Time.deltaTime;
			transform.rotation = Quaternion.Euler (90f, yRotation, 0f);
		}
	}

	void Update()
	{
		if (player.GetComponent<broadPickUp> ().theHeld == this.gameObject)
			holdingAxe = true;
		else
			holdingAxe = false;

		if (oneRun != holdingAxe) 
		{
			swapColliders ();
		}
	}

	void swapColliders()
	{
		oneRun = !oneRun;
		axeBlade.GetComponent<Collider> ().enabled = !axeBlade.GetComponent<Collider> ().enabled;
		axeHandle.GetComponent<Collider> ().enabled = !axeHandle.GetComponent<Collider> ().enabled;
		GetComponent<Collider> ().enabled = !GetComponent<Collider> ().enabled;
	}
		
}
