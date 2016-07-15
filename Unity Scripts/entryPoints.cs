using UnityEngine;
using System.Collections;

public class entryPoints : MonoBehaviour 
{
	public AudioClip filler;

	bool userInputEnabled;
	bool handsAreFull;
	bool ladderIsReady;
	doorUnlock doorUnlockScript;
	Camera endCam;
	Camera mainCam;
	AudioSource fxStereo;
	GameObject ladderZone1, ladderZone2, ladderZone3, ladderZone4;
	GameObject theDoor;
	GameObject dummyLadder;

	void Awake()
	{
		endCam = GameObject.FindWithTag ("EndCamera").GetComponent<Camera>();
		mainCam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera>();
		theDoor = GameObject.FindWithTag("Door");
		doorUnlockScript = theDoor.GetComponent<doorUnlock> ();
		dummyLadder = GameObject.FindWithTag ("dummyLadder");
		ladderZone1 = GameObject.FindWithTag ("LadderSnap1");
		ladderZone2 = GameObject.FindWithTag ("LadderSnap2");
		ladderZone3 = GameObject.FindWithTag ("LadderSnap3");
		ladderZone4 = GameObject.FindWithTag ("LadderSnap4");
		fxStereo = GameObject.FindWithTag ("SoundFX").GetComponent<AudioSource>();
	}

	void Update() 
	{
		handsAreFull = GetComponent<broadPickUp> ().handsAreFull;
		userInputEnabled = doorUnlockScript.userInputEnabled;
		ladderIsReady = ladderZone1.GetComponent<LadderSnap>().snap | ladderZone2.GetComponent<LadderSnap>().snap | ladderZone3.GetComponent<LadderSnap>().snap | ladderZone4.GetComponent<LadderSnap>().snap;

		if (userInputEnabled) 
		{
			if (Input.GetKeyDown (KeyCode.Space) && !handsAreFull) 
			{
				attemptEntry ();
			}
		}
	}

	void attemptEntry()
	{
		int centerx = Screen.width/2;
		int centery = Screen.height/2;

		Ray aim = mainCam.ScreenPointToRay(new Vector3(centerx, centery));
		RaycastHit hit;

		if (Physics.Raycast (aim, out hit, 4)) 
		{
			if (hit.transform.gameObject == dummyLadder && ladderIsReady)
			{
				doorUnlockScript.endScene ();
				fxStereo.PlayOneShot(filler, 1);
				doorUnlockScript.Invoke("endGame", 6);
			}
		}
	}
}
