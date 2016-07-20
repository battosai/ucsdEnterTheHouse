using UnityEngine;
using System.Collections;

public class iThrowYou : MonoBehaviour 
{
	public float power;
	public GameObject theThrown;
	public AudioClip hollowThump;
	public AudioClip woodThump;
	public AudioClip mattressThump;
	public AudioClip plasticThump;

	GameObject axe;
	broadPickUp pickUpScript;
	bool handsAreFull;

	void Awake()
	{
		pickUpScript = GetComponent<broadPickUp>();
		axe = GameObject.FindWithTag ("Axe");
	}

	void Update()
	{
		handsAreFull = pickUpScript.handsAreFull;

		//if holding an object and Space is pressed, toss it
		if (handsAreFull && Input.GetKeyDown(KeyCode.Space)) 
		{
			theThrown = pickUpScript.theHeld;
			toss();
		}
	}

	void toss()
	{
		if (theThrown == axe)
		{
			axe.GetComponent<axeSwing> ().oneRun = true;
		}

		//apply force, let iPickYouUp know it's not being held
		pickUpScript.handsAreFull = false;
		pickUpScript.theHeld.gameObject.GetComponent<Rigidbody>().useGravity = true;
		theThrown.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.Impulse); 
	}
}
