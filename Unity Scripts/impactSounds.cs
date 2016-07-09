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
	AudioSource impactStereo;

	void Awake()
	{
		impactStereo = GameObject.FindWithTag("Player").GetComponent<iThrowYou>().stereo;
	}

	void OnTriggerEnter(Collider other)
	{
	}

	void OnTriggerExit(Collider other)
	{
		
	}

	// Update is called once per frame
	void Update() 
	{
		
	}
}
