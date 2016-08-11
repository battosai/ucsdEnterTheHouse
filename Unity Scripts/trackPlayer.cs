using UnityEngine;
using System.Collections;
using LSL;

/*		For narrow scenario: mark when pick up key items, look at different pages,
 * 		For broad scenario:  have player press key when they have an idea
 */ 

public class trackPlayer : MonoBehaviour {
	public int scenario;
	public AudioClip ding;

	float marker;

	AudioSource stereo;
	iPickYouUp narrowPickUpScript;
	bool contRun1, contRun2, contRun3, contRun4;

	liblsl.StreamInfo inf;
	liblsl.StreamOutlet outl;
	string[] strings;

	void Awake()
	{
		stereo = GameObject.FindWithTag("SoundFX").GetComponent<AudioSource>();
		narrowPickUpScript = GameObject.FindWithTag ("Player").GetComponent<iPickYouUp>();
	}

	void Start () {
		inf = new liblsl.StreamInfo("Test1", "Markers", 1, 0, liblsl.channel_format_t.cf_string, "giu4569");
		outl = new liblsl.StreamOutlet(inf);
		Application.runInBackground = true;
		strings = new string[] {"Test"};
		contRun1 = false;
		contRun2 = false;
		contRun3 = false;
		contRun4 = false;
		marker = 1.1f;
	}

	void Update () 
	{
		strings [0] = marker.ToString();

		//narrow
		if (scenario == 1)
			narrowCheck ();
		//broad
		else if (scenario == 0)
			broadCheck ();
	}

	void broadCheck()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) 
		{
			stereo.PlayOneShot (ding, 1);
			outl.push_sample (strings);
			marker++;
		}
	}

	void narrowCheck()
	{
		if (narrowPickUpScript.hasClue1 && !contRun1) {
			contRun1 = true;
			outl.push_sample (strings);
			marker++;
		}
		if (narrowPickUpScript.hasClue2 && !contRun2) {
			contRun2 = true;
			outl.push_sample (strings);
			marker++;
		}
		if (narrowPickUpScript.hasClue3 && !contRun3) {
			contRun3 = true;
			outl.push_sample (strings);
			marker++;
		}
		if (narrowPickUpScript.hasKey && !contRun4) {
			contRun4 = true;
			outl.push_sample (strings);
			marker++;
		}
	}
}
