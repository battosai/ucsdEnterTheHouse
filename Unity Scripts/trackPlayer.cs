using UnityEngine;
using System.Collections;
using LSL;

/*		For narrow scenario: mark when pick up key items, look at different pages,
 * 		For broad scenario:  have player press key when they have an idea
 */ 

public class trackPlayer : MonoBehaviour {
	public int scenario;

	//GameObject theDoor;
	//doorUnlock doorUnlockScript;
	//AudioSource fxStereo;
	//AudioClip basementDoorSound;

	liblsl.StreamInfo inf;
	liblsl.StreamOutlet outl;
	string[] strings;

	void Awake()
	{
		//theDoor = GameObject.FindWithTag ("Door");
		//doorUnlockScript = theDoor.GetComponent<doorUnlock> ();
		//fxStereo = GameObject.FindWithTag ("SoundFX").GetComponent<AudioSource>();
		//basementDoorSound = GetComponent<entryPoints> ().basementDoorSound;
	}

	// Use this for initialization
	void Start () {
		inf = new liblsl.StreamInfo("Test1", "Markers", 1, 0, liblsl.channel_format_t.cf_string, "giu4569");
		outl = new liblsl.StreamOutlet(inf);
		Application.runInBackground = true;
		strings = new string[] { "Test", "ABC", "123" };
	}

	// Update is called once per frame
	void Update () 
	{
		//broad scenario check
		outl.push_sample(strings);
		if (scenario == 0)
			broadCheck ();

		//System.IO.File.WriteAllText("C:\\Users\\Brian\\Desktop\\lsltest.txt", "im running");
	}

	void broadCheck()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift)) 
		{
			outl.push_sample (strings);

			//doorUnlockScript.endScene ();
			//fxStereo.PlayOneShot (basementDoorSound, 1);
			//doorUnlockScript.Invoke ("endGame", 3);
		}
	}

	//public void lslfnc()
	//{
	//	outl.push_sample (strings);
	//}
}
