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
	 * 7 - Ladder
     */
	public int objectID;

	GameObject player;
	AudioSource impactStereo;
	AudioClip woodThump;
	AudioClip hollowThump;
	AudioClip mattressThump;
	AudioClip plasticThump;
	AudioClip metalClang;
	AudioClip brick;

	void Awake()
	{
		player = GameObject.FindWithTag("Player");
		impactStereo = GameObject.FindWithTag("SoundFX").GetComponent<AudioSource>();
		hollowThump = player.GetComponent<iThrowYou>().hollowThump;
		woodThump = player.GetComponent<iThrowYou>().woodThump;
		mattressThump = player.GetComponent<iThrowYou>().mattressThump;
		plasticThump = player.GetComponent<iThrowYou>().plasticThump;
		metalClang = player.GetComponent<iThrowYou> ().metalClang;
		brick = player.GetComponent<iThrowYou> ().brick;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject == player)
			return;
		
		switch (objectID) 
		{
		case 1:
			impactStereo.PlayOneShot (woodThump, 1);
			break;
		case 2:
			impactStereo.PlayOneShot (hollowThump, 1);
			break;
		case 3:
			impactStereo.PlayOneShot (brick, 1);
			break;
		case 4:
			impactStereo.PlayOneShot (plasticThump, 1);
			break;
		case 5:
			impactStereo.PlayOneShot (mattressThump, 1);
			break;
		case 6:
			impactStereo.PlayOneShot (hollowThump, 1);
			break;
		case 7:
			impactStereo.PlayOneShot (metalClang, 1);
			break;
		}
	}

	// Update is called once per frame
	void Update() 
	{
	}
}
