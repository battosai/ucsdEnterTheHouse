using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class doorUnlock : MonoBehaviour 
{	
	public AudioClip unlockSound;
	public AudioClip lockedOut;
	public bool userInputEnabled;
	GameObject theDoor;
	GameObject player;
	Camera mainCam;
	Camera endCam;
	AudioSource stereo;
	bool canUnlock;
	bool handsAreFull;

	// Use this for initialization
	void Awake()
	{
		mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		endCam = GameObject.FindWithTag("EndCamera").GetComponent<Camera>();
		theDoor = GameObject.FindWithTag("Door");
		player = GameObject.FindWithTag("Player");
		endCam.GetComponent<AudioListener>().enabled = false;
		endCam.enabled = false;
		stereo = GetComponent<AudioSource>();
		canUnlock = false;
		userInputEnabled = true;
	}
	
	void openDoor()
	{
		//NEW METHOD USING RAY
		int centerx = Screen.width/2;
		int centery = Screen.height/2;
		
		Ray aim = mainCam.ScreenPointToRay(new Vector3(centerx, centery));
		RaycastHit hit;
		
		if(Physics.Raycast(aim, out hit, 4))
		{
			//check if object hit is the door and that player has the key
			if(hit.transform.gameObject == theDoor && canUnlock)
			{
				endScene ();
				stereo.PlayOneShot(unlockSound, 1);
				Invoke("endGame", 6);
			}
			//if the player is aiming at the door but does not have the key
			else if(hit.transform.gameObject == theDoor && !canUnlock)
			{
				stereo.PlayOneShot(lockedOut, 1);
			}
		}
	}

	public void endScene()
	{
		player.GetComponent<PlayerController>().stereo.Pause();
		player.GetComponent<PlayerController>().currPage.enabled = false;
		GameObject.FindWithTag("Controls").GetComponent<Text>().enabled = false;
		userInputEnabled = false;
		mainCam.enabled = false;
		endCam.enabled = true;
	}

	public void endGame()
	{
		Application.Quit();
	}
	
	// Update is called once per frame
	void Update() 
	{


		if (player.GetComponent<iPickYouUp> () != null) {
			handsAreFull = player.GetComponent<iPickYouUp> ().handsAreFull;
			canUnlock = player.GetComponent<iPickYouUp> ().hasKey;
		} 
		else 
		{
			handsAreFull = player.GetComponent<broadPickUp> ().handsAreFull;
		}
		
		if(Input.GetKeyDown(KeyCode.Space) && userInputEnabled && !handsAreFull)
		{
			openDoor();
		}
	}
}
