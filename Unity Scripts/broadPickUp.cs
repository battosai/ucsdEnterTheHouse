using UnityEngine;
using System.Collections;

public class broadPickUp : MonoBehaviour 
{
	public float distance;
	public float speed;
	public bool handsAreFull;
	public AudioClip paperPickUp;
	public GameObject theHeld;

	Texture found1, found2, found3;
	Texture clue1, clue2, clue3;
	GameObject cameraman;
	GameObject firstClue, secondClue, thirdClue;

	AudioSource soundEffects;

	// Use this for initialization
	void Awake() 
	{
		clue1 = GetComponent<PlayerController> ().clue1;
		clue2 = GetComponent<PlayerController> ().clue2;
		clue3 = GetComponent<PlayerController> ().clue3;
		cameraman = GameObject.FindWithTag("MainCamera");
		firstClue = GameObject.FindWithTag("clue1");
		secondClue = GameObject.FindWithTag("clue2");
		thirdClue = GameObject.FindWithTag("clue3");
		soundEffects = GameObject.FindWithTag("SoundFX").GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update() 
	{
		found1 = GetComponent<PlayerController>().found1;
		found2 = GetComponent<PlayerController>().found2;
		found3 = GetComponent<PlayerController>().found3;

		if(handsAreFull)
		{
			hold();

			//if E is pressed while holding an object, drop it
			if(Input.GetKeyDown(KeyCode.E))
				drop();
		}
		else
		{
			//if E is pressed while empty-handed, pick it up
			if(Input.GetKeyDown(KeyCode.E))
				pickup();
		}
	}

	void hold()
	{
		//hold object at constant distance 
		theHeld.transform.position = Vector3.Lerp (theHeld.transform.position, cameraman.transform.position + cameraman.transform.forward * distance, speed * Time.deltaTime);
	}

	void drop()
	{
		//turn gravity back on
		//point to empty object
		handsAreFull = false;
		theHeld.gameObject.GetComponent<Rigidbody>().useGravity = true;
		theHeld = null;
	}

	void pickup()
	{
		int centerx = Screen.width/2;
		int centery = Screen.height/2;
		pickMeUp holdme;

		//see what's being aimed at within distance
		Ray aim = cameraman.GetComponent<Camera>().ScreenPointToRay(new Vector3(centerx, centery));
		RaycastHit hit;

		if(Physics.Raycast(aim, out hit, 4))
		{
			holdme = hit.collider.GetComponent<pickMeUp>();

			//if first clue
			if (holdme.gameObject != null) 
			{
				if (holdme.gameObject == firstClue) 
				{
					clueOrder (clue1);
					Destroy (firstClue);
					firstClue = null;
					GetComponent<PlayerController>().clueNum++;
					soundEffects.clip = paperPickUp;
					soundEffects.Play();
				}
				//if second clue
				else if (holdme.gameObject == secondClue) 
				{
					clueOrder (clue2);
					Destroy (secondClue);
					secondClue = null;
					GetComponent<PlayerController>().clueNum++;
					soundEffects.clip = paperPickUp;
					soundEffects.Play();
				}
				//if third clue
				else if (holdme.gameObject == thirdClue) 
				{
					clueOrder (clue3);
					Destroy (thirdClue);
					thirdClue = null;
					GetComponent<PlayerController>().clueNum++;
					soundEffects.clip = paperPickUp;
					soundEffects.Play();
				}
				//if any object that isn't essential
				else 
				{
					//set distance to distance when picked up is
					distance = Vector3.Distance (hit.collider.transform.position, transform.position);
					handsAreFull = true;
					theHeld = holdme.gameObject;
					holdme.gameObject.GetComponent<Rigidbody> ().useGravity = false;
				}
			}
		}
	}

	void clueOrder(Texture clue)
	{
		if (found1 == null) 
		{
			assignClue (clue, 1);
		} 
		else if (found2 == null) 
		{
			assignClue (clue, 2);
		} 
		else if(found3 == null)
		{
			assignClue (clue, 3);
		}
	}

	void assignClue(Texture clue, int found)
	{
		switch (found) 
		{
		case 1:
			GetComponent<PlayerController> ().found1 = clue;
			break;
		case 2:
			GetComponent<PlayerController> ().found2 = clue;
			break;
		case 3:
			GetComponent<PlayerController> ().found3 = clue;
			break;
		}
	}
}
