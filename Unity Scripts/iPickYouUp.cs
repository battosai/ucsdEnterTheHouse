using UnityEngine;
using System.Collections;

public class iPickYouUp : MonoBehaviour 
{
	public float distance;
	public float speed;
	public bool hasKey, hasClue1, hasClue2, hasClue3;
	GameObject cameraman;
	GameObject theHeld;
	GameObject theKey;
	GameObject firstClue, secondClue, thirdClue;
	bool handsAreFull;
	
	// Use this for initialization
	void Awake() 
	{
		cameraman = GameObject.FindWithTag("MainCamera");
		theKey = GameObject.FindWithTag("Key");
		theKey.GetComponent<Renderer>().enabled = false;
		theKey.GetComponent<Collider>().enabled = false;
		theKey.GetComponent<Rigidbody>().useGravity = false;
		
		firstClue = GameObject.FindWithTag("clue1");
		
		secondClue = GameObject.FindWithTag("clue2");
		secondClue.GetComponent<Renderer>().enabled = false;
		secondClue.GetComponent<Collider>().enabled = false;
		
		thirdClue = GameObject.FindWithTag("clue3");
		thirdClue.GetComponent<Renderer>().enabled = false;
		thirdClue.GetComponent<Collider>().enabled = false;
		
		hasKey = false;
		hasClue1 = false;
		hasClue2 = false;
		hasClue3 = false;
	}
	
	// Update is called once per frame
	void Update() 
	{
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
		//theHeld.transform.position = cameraman.transform.position + cameraman.transform.forward * distance;
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
			if(holdme.gameObject == firstClue)
			{
				Destroy(firstClue);
				firstClue = null;
				hasClue1 = true;
				secondClue.GetComponent<Renderer>().enabled = true;
				secondClue.GetComponent<Collider>().enabled = true;
			}
			//if second clue
			else if(holdme.gameObject == secondClue)
			{
				Destroy(secondClue);
				secondClue = null;
				hasClue2 = true;
				thirdClue.GetComponent<Renderer>().enabled = true;
				thirdClue.GetComponent<Collider>().enabled = true;
			}
			//if third clue
			else if(holdme.gameObject == thirdClue)
			{
				Destroy(thirdClue);
				thirdClue = null;
				hasClue3 = true;
				theKey.GetComponent<Renderer>().enabled = true;
				theKey.GetComponent<Collider>().enabled = true;
				theKey.GetComponent<Rigidbody>().useGravity = true;
			}
			//if aiming at the key, destroy it and raise the key flag
			else if(holdme.gameObject == theKey)
			{
				Destroy(theKey);
				theKey = null;
				hasKey = true;
			}
			//if any object that isn't essential
			else if(holdme != null)
			{
				//set distance to distance when picked up is
				distance = Vector3.Distance(hit.collider.transform.position, transform.position);
				handsAreFull = true;
				theHeld = holdme.gameObject;
				holdme.gameObject.GetComponent<Rigidbody>().useGravity = false;
			}
		}
	}
}