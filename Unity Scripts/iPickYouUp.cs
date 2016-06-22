using UnityEngine;
using System.Collections;

public class iPickYouUp : MonoBehaviour 
{
	GameObject cameraman;
	GameObject theHeld;
	bool handsAreFull;
	public float distance;
	public float speed;
	
	// Use this for initialization
	void Start() 
	{
		cameraman = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update() 
	{
		if(handsAreFull)
		{
			hold();
			dropCheck();
		}
		else
		{
			pickup();
		}
	}
	
	void hold()
	{
		//hold object at constant distance 
		theHeld.transform.position = Vector3.Lerp (theHeld.transform.position, cameraman.transform.position + cameraman.transform.forward * distance, speed * Time.deltaTime);
		//theHeld.transform.position = cameraman.transform.position + cameraman.transform.forward * distance;
	}
	
	void dropCheck()
	{
		//if E is pressed while holding an object, drop it
		if(Input.GetKeyDown(KeyCode.E) && handsAreFull)
		{
			drop();
		}
	}
	
	void drop()
	{
		//turn forces back on
		//point to empty object
		handsAreFull = false;
		theHeld.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		theHeld = null;
	}
	
	void pickup()
	{
		//if E is pressed
		if(Input.GetKeyDown(KeyCode.E))
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
				if(holdme != null)
				{
					handsAreFull = true;
					theHeld = holdme.gameObject;
					holdme.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				}
			}
		}
	}
}