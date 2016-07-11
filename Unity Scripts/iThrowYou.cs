using UnityEngine;
using System.Collections;

public class iThrowYou : MonoBehaviour 
{
	public bool thrown;
	public float power;
	public GameObject theThrown;
	public AudioClip hollowThump;
	public AudioClip woodThump;
	public AudioClip mattressThump;
	public AudioClip plasticThump;

	AudioSource stereo;
	iPickYouUp pickUpScript;
	bool handsAreFull;

	void Awake()
	{
		stereo = GameObject.FindWithTag("SoundFX").GetComponent<AudioSource>();
		pickUpScript = GetComponent<iPickYouUp>();
		thrown = false;
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

		if (thrown) 
		{
			speedCheck();
		}
	}

	void toss()
	{
		//apply force, let iPickYouUp know it's not being held
		pickUpScript.handsAreFull = false;
		pickUpScript.theHeld.gameObject.GetComponent<Rigidbody>().useGravity = true;
		theThrown.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.Impulse); 
		thrown = true;
	}

	void speedCheck()
	{
		Vector3 speed = theThrown.gameObject.GetComponent<Rigidbody>().velocity;

		if (speed.x == 0 && speed.y == 0 && speed.z == 0) 
		{
			print ("i'm not moving");
			thrown = false;
		}
	}
}
