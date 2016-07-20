using UnityEngine;
using System.Collections;

public class windowBreak : MonoBehaviour 
{
	public int windowID;
	public bool windowReady;

	int windowHP;
	int thrownObjectID;
	GameObject player;
	GameObject windowBoard1, windowBoard2, windowBoard3, windowBoard4, windowBoard5, windowBoard6, windowBoard7, windowBoard8, windowBoard9, windowBoard10;
	iThrowYou playerThrowScript;

	void Awake()
	{
		windowReady = false;
		windowHP = 2; //depending on how many boards are on the windows...should be 2
		player = GameObject.FindWithTag ("Player");
		playerThrowScript = player.GetComponent<iThrowYou> ();
		windowBoard1 = GameObject.FindWithTag ("WindowBoard1");
		windowBoard2 = GameObject.FindWithTag ("WindowBoard2");
		windowBoard3 = GameObject.FindWithTag ("WindowBoard3");
		windowBoard4 = GameObject.FindWithTag ("WindowBoard4");
		windowBoard5 = GameObject.FindWithTag ("WindowBoard5");
		windowBoard6 = GameObject.FindWithTag ("WindowBoard6");
		windowBoard7 = GameObject.FindWithTag ("WindowBoard7");
		windowBoard8 = GameObject.FindWithTag ("WindowBoard8");
		windowBoard9 = GameObject.FindWithTag ("WindowBoard9");
		windowBoard10 = GameObject.FindWithTag ("WindowBoard10");
	}

	void Update()
	{
		thrownObjectID = playerThrowScript.theThrown.GetComponent<impactSounds>().objectID;
	}

	void OnCollisionEnter(Collision other)
	{
		//use theThrown from iThrowYou and objectID to identify if it's an appropriate object
		if (other.collider.gameObject == playerThrowScript.theThrown && thrownObjectID == 3 && windowHP > 0) 
		{
			windowHP--;
			breakBoard ();
		}
	}

	void breakBoard()
	{
		switch (windowID) {
		case 1:
			if (windowHP == 1) {
				windowBoard2.GetComponent<Renderer> ().enabled = false;
			} 
			else if (windowHP == 0) {
				windowBoard1.GetComponent<Renderer> ().enabled = false;
			}
			break;
		case 2:
			if (windowHP == 1) {
				windowBoard4.GetComponent<Renderer> ().enabled = false;
			}
			else if (windowHP == 0) {
				windowBoard3.GetComponent<Renderer> ().enabled = false;
			}
			break;
		case 3:
			if (windowHP == 1) {
				windowBoard6.GetComponent<Renderer> ().enabled = false;
			}
			else if (windowHP == 0) {
				windowBoard5.GetComponent<Renderer> ().enabled = false;
			}
			break;
		case 4:
			if (windowHP == 1) {
				windowBoard8.GetComponent<Renderer> ().enabled = false;
			}
			else if (windowHP == 0) {
				windowBoard7.GetComponent<Renderer> ().enabled = false;
			}
			break;
		case 5:
			if (windowHP == 1) {
				windowBoard10.GetComponent<Renderer> ().enabled = false;
			}
			else if (windowHP == 0) {
				windowBoard9.GetComponent<Renderer> ().enabled = false;
			}
			break;
		}

		if (windowHP == 0)
			windowReady = true;
	}
}
