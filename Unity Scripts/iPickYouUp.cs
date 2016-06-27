using UnityEngine;
using System.Collections;

public class iPickYouUp : MonoBehaviour 
{
	public float distance;
	public float speed;
	public bool hasKey;
	GameObject cameraman;
	GameObject theHeld;
	GameObject theKey;
	bool handsAreFull;
	
	// Use this for initialization
	void Awake() 
	{
		cameraman = GameObject.FindWithTag("MainCamera");
		theKey = GameObject.FindWithTag("Key");
		hasKey = false;
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
			
			//if aiming at the key, destroy it and raise the key flag
			if(holdme.gameObject == theKey)
			{
				Destroy(theKey);
				theKey = null;
				hasKey = true;
				return;
			}
			
			if(holdme != null)
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