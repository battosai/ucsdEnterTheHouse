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

	broadPickUp pickUpScript;

	bool handsAreFull;

	void Awake()
	{
		pickUpScript = GetComponent<broadPickUp>();
	}

	void Update()
	{
		handsAreFull = pickUpScript.handsAreFull;
		theThrown = pickUpScript.theHeld;

		//if holding an object and Space is pressed, toss it
		if (handsAreFull && Input.GetKeyDown(KeyCode.Space)) 
		{
			toss();
		}
	}

	void toss()
	{
		//apply force, let iPickYouUp know it's not being held
		pickUpScript.handsAreFull = false;
		pickUpScript.theHeld.gameObject.GetComponent<Rigidbody>().useGravity = true;
		theThrown.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.Impulse); 
	}
}
