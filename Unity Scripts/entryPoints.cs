using UnityEngine;
using System.Collections;

public class entryPoints : MonoBehaviour 
{
	bool userInputEnabled;
	Camera endCam;
	Camera mainCam;
	AudioSource doorStereo;
	GameObject theDoor;

	void Awake()
	{
		endCam = GameObject.FindWithTag ("EndCamera").GetComponent<Camera>();
		mainCam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera>();
		theDoor = GameObject.FindWithTag("Door");
		doorStereo = theDoor.GetComponent<AudioSource> ();
	}

	void Update() 
	{
		userInputEnabled = theDoor.GetComponent<doorUnlock> ().userInputEnabled;

		if (userInputEnabled) 
		{
			
		}
	}
}
