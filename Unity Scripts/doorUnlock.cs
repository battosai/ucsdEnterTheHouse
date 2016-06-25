using UnityEngine;
using System.Collections;

public class doorUnlock : MonoBehaviour 
{	
	public AudioClip unlockSound;
	public bool userInputEnabled;
	Camera mainCam;
	Camera endCam;
	AudioSource stereo;
	bool canUnlock;

	// Use this for initialization
	void Awake()
	{
		mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		endCam = GameObject.FindWithTag("EndCamera").GetComponent<Camera>();
		endCam.GetComponent<AudioListener>().enabled = false;
		endCam.enabled = false;
		stereo = GetComponent<AudioSource>();
		canUnlock = false;
		userInputEnabled = true;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Key")
			canUnlock = true;
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.transform.tag == "Key")
			canUnlock = false;
	}
	
	// Update is called once per frame
	void Update() 
	{
		//if in trigger and input is given
		if(canUnlock && Input.GetKeyDown(KeyCode.Space) && userInputEnabled)
		{
				userInputEnabled = false;
				stereo.PlayOneShot(unlockSound, 1);
				mainCam.enabled = false;
				endCam.enabled = true;
				
				//MIGHT NEED TO MOVE IF IT SIMULTANEOUSLY DOES THIS ALL...
				//needs to quit after playing the sound in a black screen
				Application.Quit();
		}
	}
}
