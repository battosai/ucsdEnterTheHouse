using UnityEngine;
using System.Collections;

public class iThrowYou : MonoBehaviour 
{
	public float power;

	iPickYouUp pickUpScript;
	GameObject theHeld;
	bool handsAreFull;
	bool thrown;

	void Awake()
	{
		pickUpScript = GetComponent<iPickYouUp>();
		thrown = false;
	}

	void Update()
	{
		handsAreFull = pickUpScript.handsAreFull;
		theHeld = pickUpScript.theHeld;

		//if holding an object and Space is pressed, toss it
		if (handsAreFull && Input.GetKeyDown(KeyCode.Space)) 
		{
			toss();
			thrown = true;
		}

		if (thrown) 
		{
			impactCheck();
		}

	}

	void impactCheck();

	void toss()
	{
		//apply force, let iPickYouUp know it's not being held
		pickUpScript.handsAreFull = false;
		pickUpScript.theHeld.gameObject.GetComponent<Rigidbody>().useGravity = true;
		theHeld.GetComponent<Rigidbody>().AddForce (transform.forward * power); 

		//do this once impact noise has been made, move this line later
		pickUpScript.theHeld = null;
	}
}
