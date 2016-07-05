using UnityEngine;
using System.Collections;

public class iThrowYou : MonoBehaviour 
{
	public float power;

	iPickYouUp pickUpScript;
	GameObject theHeld;
	bool handsAreFull;

	void Awake()
	{
		pickUpScript = GetComponent<iPickYouUp>();
	}

	void Update()
	{
		handsAreFull = pickUpScript.handsAreFull;
		theHeld = pickUpScript.theHeld;

		//if holding an object and Space is pressed, toss it
		if (handsAreFull) 
		{
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				toss();
			}
		}
	}

	void toss()
	{
		pickUpScript.handsAreFull = false;
		pickUpScript.theHeld.gameObject.GetComponent<Rigidbody>().useGravity = true;
		theHeld.GetComponent<Rigidbody>().AddForce (transform.forward * power); 
		pickUpScript.theHeld = null;
	}
}
