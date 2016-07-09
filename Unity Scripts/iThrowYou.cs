using UnityEngine;
using System.Collections;

public class iThrowYou : MonoBehaviour 
{
	public bool thrown;
	public float power;
	public AudioClip impact;
	public AudioSource stereo;

	iPickYouUp pickUpScript;
	public GameObject theThrown;
	bool handsAreFull;

	void Awake()
	{
		stereo = GameObject.FindWithTag("SoundFX").GetComponent<AudioSource>();
		pickUpScript = GetComponent<iPickYouUp>();
		thrown = false;
	}

	void FixedUpdate()
	{
		if(thrown) 
		{
			
		}
	}

	void Update()
	{
		handsAreFull = pickUpScript.handsAreFull;
		theThrown = pickUpScript.theHeld;

		//if holding an object and Space is pressed, toss it
		if (handsAreFull && Input.GetKeyDown(KeyCode.Space)) 
		{
			toss();
			thrown = true;
		}
	}

	void toss()
	{
		//apply force, let iPickYouUp know it's not being held
		pickUpScript.handsAreFull = false;
		pickUpScript.theHeld.gameObject.GetComponent<Rigidbody>().useGravity = true;
		theThrown.GetComponent<Rigidbody>().AddForce(transform.forward * power); 
	}
}
