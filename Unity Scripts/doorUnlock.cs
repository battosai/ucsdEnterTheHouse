using UnityEngine;
using System.Collections;

public class doorUnlock : MonoBehaviour 
{	
	public AudioClip unlockSound;
	AudioSource audio;
	bool canUnlock;

	// Use this for initialization
	void Start()
	{
		audio = GetComponent<AudioSource>();
		canUnlock = false;
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
		if(canUnlock && Input.GetKeyDown(KeyCode.Space))
		{
				audio.PlayOneShot(unlockSound, 1);
		}
	}
}
