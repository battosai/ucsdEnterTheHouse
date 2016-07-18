using UnityEngine;
using System.Collections;

public class entryPoints : MonoBehaviour 
{
	public AudioClip filler;

	bool[] windowReady;
	bool userInputEnabled;
	bool handsAreFull;
	bool ladderIsReady;
	doorUnlock doorUnlockScript;
	Camera endCam;
	Camera mainCam;
	AudioSource fxStereo;
	GameObject windowZone1, windowZone2, windowZone3, windowZone4, windowZone5;
	GameObject ladderZone1, ladderZone2, ladderZone3, ladderZone4;
	GameObject theDoor;
	GameObject dummyLadder;

	void Awake()
	{
		windowReady = new bool[6];
		endCam = GameObject.FindWithTag ("EndCamera").GetComponent<Camera>();
		mainCam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera>();
		theDoor = GameObject.FindWithTag("Door");
		doorUnlockScript = theDoor.GetComponent<doorUnlock> ();
		dummyLadder = GameObject.FindWithTag ("dummyLadder");
		ladderZone1 = GameObject.FindWithTag ("LadderSnap1");
		ladderZone2 = GameObject.FindWithTag ("LadderSnap2");
		ladderZone3 = GameObject.FindWithTag ("LadderSnap3");
		ladderZone4 = GameObject.FindWithTag ("LadderSnap4");
		windowZone1 = GameObject.FindWithTag ("WindowZone1");
		windowZone2 = GameObject.FindWithTag ("WindowZone2");
		windowZone3 = GameObject.FindWithTag ("WindowZone3");
		windowZone4 = GameObject.FindWithTag ("WindowZone4");
		windowZone5 = GameObject.FindWithTag ("WindowZone5");
		fxStereo = GameObject.FindWithTag ("SoundFX").GetComponent<AudioSource>();
	}

	void Update() 
	{
		windowReady [1] = windowZone1.GetComponent<windowBreak> ().windowReady;
		windowReady [2] = windowZone2.GetComponent<windowBreak> ().windowReady;
		windowReady [3] = windowZone3.GetComponent<windowBreak> ().windowReady;
		windowReady [4] = windowZone4.GetComponent<windowBreak> ().windowReady;
		windowReady [5] = windowZone5.GetComponent<windowBreak> ().windowReady;
		windowReady [0] = windowReady [1] | windowReady [2] | windowReady [3] | windowReady [4] | windowReady [5];
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
			//ladder
			if (hit.transform.gameObject == dummyLadder && ladderIsReady) {
				doorUnlockScript.endScene ();
				fxStereo.PlayOneShot (filler, 1);
				doorUnlockScript.Invoke ("endGame", 6);
			}
			//windows
			else if (hit.transform.gameObject.GetComponent<windowBreak> () != null && windowReady[0]) {
				if (testWindows (hit.transform.gameObject.GetComponent<windowBreak>()) ) {
					doorUnlockScript.endScene ();
					fxStereo.PlayOneShot (filler, 1);
					doorUnlockScript.Invoke ("endGame", 6);
				}
			}
		}
	}

	bool testWindows(windowBreak windowScript)
	{
		int windowID = windowScript.windowID;
		return windowReady [windowID];
	}
}
