using UnityEngine;
using System.Collections;

public class impactSounds : MonoBehaviour 
{
	/*
	 * 1 - Palettes
	 * 2 - Orange Barrel
	 * 3 - Cinderblock
	 * 4 - Reflector
	 * 5 - Mattress
	 * 6 - Tire (same as orange barrel?)
	 * 7 - Airduct Vent (same as cinderblock?)
     */
	public int objectID;

	bool makeSounds;
	bool continuousRun;
	bool thrown;
	GameObject player;
	GameObject theThrown;
	AudioSource impactStereo;

	void Awake()
	{
		player = GameObject.FindWithTag("Player");
		impactStereo = player.GetComponent<iThrowYou>().stereo;
		makeSounds = false;
		thrown = false;
		continuousRun = false;
	}

	void OnTriggerEnter(Collider other)
	{
		makeSounds = true;
	}

	void OnTriggerExit(Collider other)
	{
		makeSounds = false;
		continuousRun = false;
	}

	// Update is called once per frame
	void Update() 
	{
		theThrown = player.GetComponent<iThrowYou>().theThrown;
		thrown = player.GetComponent<iThrowYou>().thrown;

		if (thrown && makeSounds && !continuousRun) 
		{
			print ("I MAKE SOUNDS");
			continuousRun = true;
		}
	}
}
