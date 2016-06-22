using UnityEngine;
using System.Collections;

public class doorUnlock : MonoBehaviour 
{	
	bool canUnlock;

	// Use this for initialization
	void Start()
	{
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Key")
			canUnlock = true;
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.transform.tag == "Key")
			canUnlock = false;
	}
	
	// Update is called once per frame
	void Update() 
	{
		//if in trigger and input is given
		if(canUnlock && Input.GetKeyDown(KeyCode.Space))
		//if(canUnlock && Input.GetMouseButtonDown(0))
			Debug.Log("Door would be unlocked");
		
	}
}
