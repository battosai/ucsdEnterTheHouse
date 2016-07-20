using UnityEngine;
using System.Collections;

public class axeSwing : MonoBehaviour {
	public bool holdingAxe;
	public bool oneRun;

	GameObject axeBlade;
	GameObject axeHandle;

	void Awake()
	{
		axeBlade = GameObject.FindWithTag ("AxeBlade");
		axeHandle = GameObject.FindWithTag ("AxeHandle");
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
		if (holdingAxe) 
		{
			reposition ();
		} 
		else 
		{
		}

		if (oneRun) 
		{
			swapColliders ();
			oneRun = false;
		}
	}

	void reposition()
	{
	}

	void swapColliders()
	{
		axeBlade.GetComponent<Collider> ().enabled = !axeBlade.GetComponent<Collider> ().enabled;
		axeHandle.GetComponent<Collider> ().enabled = !axeHandle.GetComponent<Collider> ().enabled;
		GetComponent<Collider> ().enabled = !GetComponent<Collider> ().enabled;
	}
}
