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

	GameObject player;
	GameObject theThrown;
	AudioSource impactStereo;
	AudioClip woodThump;
	AudioClip hollowThump;

	void Awake()
	{
		player = GameObject.FindWithTag("Player");
		impactStereo = GameObject.FindWithTag("SoundFX").GetComponent<AudioSource>();
		theThrown = player.GetComponent<iThrowYou>().theThrown;
		hollowThump = player.GetComponent<iThrowYou>().hollowThump;
		woodThump = player.GetComponent<iThrowYou>().woodThump;
	}

	void OnCollisionEnter(Collision other)
	{
		switch (objectID) 
		{
		case 1:
			impactStereo.PlayOneShot (woodThump, 1);
			break;
		case 2:
			impactStereo.PlayOneShot (hollowThump, 1);
			break;
		case 3:
			break;
		case 4:
			break;
		case 5:
			break;
		case 6:
			impactStereo.PlayOneShot (hollowThump, 1);
			break;
		case 7:
			impactStereo.PlayOneShot (hollowThump, 1);
			break;
		}
	}

	// Update is called once per frame
	void Update() 
	{
	}
}
